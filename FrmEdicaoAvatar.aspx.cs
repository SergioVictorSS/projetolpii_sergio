using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgendaTarefas;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Management;

namespace AgendaTarefaPaginas
{
    public partial class FrmEdicaoAvatar : PaginaBase<Avatar>
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarController(new AvatarModel(SDBC.Instance));

            if (!IsPostBack)
                CarregarGrid();      

        }
        protected void CarregarGrid()
        {
            grvBusca.DataSource = controller.Listar(new Avatar
            {
                AVA_Nome = txtNome.Value,
                AVA_Id = GetInt(txtCodigo.Value)
            });
            grvBusca.DataBind();
        }

        protected void grvBusca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        controller.Alterar(GerarAvatar(grvBusca.Rows[int.Parse(e.CommandArgument.ToString())]));
                        Notificar("Registro alterado com sucesso!", TipoNotificacao.Sucesso);

                        break;
                    case "Excluir":
                        ExcluirAvatar(int.Parse(e.CommandArgument.ToString()));
                        break;
                }
            }
            catch(Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao alterar o Avatar!", TipoNotificacao.Erro);
            }


        }
        protected Avatar GerarAvatar(GridViewRow linha)
        {
            return new Avatar
            {
                AVA_Id = int.Parse(grvBusca.DataKeys[linha.RowIndex][0].ToString()),
                AVA_Nome = ((HtmlInputText)linha.FindControl("txtNomeEditar")).Value
            };
        }

        protected Avatar GerarAvatar()
        {
            var postedFile = ipuImagem.PostedFile;
            string extensaoArquivo;
            string caminhoServidor;
            if (postedFile == null || String.IsNullOrEmpty(postedFile.FileName))
            {
                Notificar("Necessário escolher uma imagem",PaginaBase<Avatar>.TipoNotificacao.Alerta);
                return null;
            }
            var aux = postedFile.FileName.Split('.');
            extensaoArquivo = aux.LastOrDefault();

            caminhoServidor = Server.MapPath("./Avatares/");

            if (!Directory.Exists(caminhoServidor))
                Directory.CreateDirectory(caminhoServidor);

            var guid = Guid.NewGuid();
            caminhoServidor = $"{caminhoServidor}{guid}.{extensaoArquivo}";

            postedFile.SaveAs(caminhoServidor);

            return new Avatar
            {
                AVA_Nome = txtNome.Value,
                AVA_Imagem = $"/Avatares/{guid}.{extensaoArquivo}"
            };
        }

        protected void btPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid();
            }
            catch(Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao pesquisar", TipoNotificacao.Erro);
            }
        }

        protected void btInserirNova_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNome.Value))
            {
                Notificar("Informe uma descrição e/ou código indentificador válido(s)",TipoNotificacao.Alerta);
                return;
            }
            try
            {
                var avatar = GerarAvatar();
                if (avatar == null)
                    return;                    
                controller.Inserir(avatar);
                LimparCampos();
                CarregarGrid();
                Notificar("Registro inserido com sucesso!", TipoNotificacao.Sucesso);
            }
            catch (Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao inserir o Avatar", TipoNotificacao.Erro);
            }
        }
        protected void LimparCampos()
        {
            txtCodigo.Value = string.Empty;
            txtNome.Value = string.Empty;
        }
        protected void ExcluirAvatar(int numeroLinha)
        {
            try
            {
                controller.Excluir(GerarAvatar(grvBusca.Rows[numeroLinha]).AVA_Id);
                btPesquisar_Click(null, null);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK"))
                {
                    Notificar("O avatar está em uso por alguma pessoa!", TipoNotificacao.Alerta);
                    return;
                }
                throw;
            }
        }
    }
}