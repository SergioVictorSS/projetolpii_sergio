using AgendaTarefas;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace AgendaTarefaPaginas
{
    public partial class FrmEdicaoStatus : PaginaBase<Status>
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarController(new StatusModel(SDBC.Instance));

            if (!IsPostBack)
                CarregarGrid();      

        }
        protected void CarregarGrid()
        {
            grvBusca.DataSource = controller.Listar(GerarStatus());
            grvBusca.DataBind();
        }

        protected void grvBusca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        controller.Alterar(GerarStatus(grvBusca.Rows[int.Parse(e.CommandArgument.ToString())]));
                        Notificar("Registro alterado com sucesso!", TipoNotificacao.Sucesso);
                        break;
                    case "Excluir":
                        ExcluirStatus(int.Parse(e.CommandArgument.ToString()));
                        break;
                }
            }catch(Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao alterar o status!", TipoNotificacao.Erro);
            }


        }
        protected Status GerarStatus(GridViewRow linha)
        {
            return new Status
            {
                STA_Id = int.Parse(grvBusca.DataKeys[linha.RowIndex][0].ToString()),
                STA_Descricao = ((HtmlInputText)linha.FindControl("txtDescricaoEditar")).Value
            };
        }
        protected Status GerarStatus()
        {
            return new Status
            {
                STA_Descricao = txtDescricao.Value,
                STA_Id = GetInt(txtCodigo.Value)
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
            if(string.IsNullOrEmpty(txtDescricao.Value))
            {
                Notificar("Informe uma descrição e/ou código indentificador válido(s)",TipoNotificacao.Alerta);
                return;
            }
            try
            {
                controller.Inserir(GerarStatus());
                LimparCampos();
                CarregarGrid();
                Notificar("Registro inserido com sucesso!", TipoNotificacao.Sucesso);
            }
            catch (Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao inserir o status", TipoNotificacao.Erro);
            }
        }
        protected void LimparCampos()
        {
            txtCodigo.Value = string.Empty;
            txtDescricao.Value = string.Empty;
        }

        private void ExcluirStatus(int numeroLinha)
        {
            try
            {
                controller.Excluir(GerarStatus(grvBusca.Rows[numeroLinha]).STA_Id);
                btPesquisar_Click(null, null);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK"))
                {
                    Notificar("Não é possível excluir o status, pois há uma tarefa associada a ele.", TipoNotificacao.Alerta);
                    return;
                }
                throw;
            }
        }

    }
}