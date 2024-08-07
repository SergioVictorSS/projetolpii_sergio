using AgendaTarefas;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AgendaTarefaPaginas
{
    public partial class FrmEdicaoPessoa : PaginaBase<Pessoa>
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            InicializarController(new PessoaModel(SDBC.Instance));

            if (!IsPostBack)
            {
                PreencheDropDownList(lstAvatar, "AVATAR_CONSULTAR_PREENCHIMENTO", 0, true, "Selecione");
                CarregarGrid();
            }

        }
        protected void CarregarGrid()
        {
            grvBusca.DataSource = controller.Listar(GerarPessoa());
            grvBusca.DataBind();
        }

        protected void grvBusca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        AlterarPessoa(int.Parse(e.CommandArgument.ToString()));
                        break;
                    case "Excluir":
                        ExcluirPessoa(int.Parse(e.CommandArgument.ToString()));
                        break;
                }
            }catch(Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao alterar a pessoa!", TipoNotificacao.Erro);
            }


        }
        protected Pessoa GerarPessoa(GridViewRow linha)
        {
            var dataNascimento = ((HtmlInputGenericControl)linha.FindControl("txtDataNascimentoEditar")).Value;
            DateTime data;
            if(!DateTime.TryParse(dataNascimento, out data))
            {
                Notificar("Não é possível removr a data de nascimento da pessoa",TipoNotificacao.Alerta);
                return null;
            }
            return new Pessoa
            {
                PES_Id = int.Parse(grvBusca.DataKeys[linha.RowIndex][0].ToString()),
                PES_DataNascimento = data,
                PES_Nome = ((HtmlInputText)linha.FindControl("txtNomeEditar")).Value
            };
        }

        protected Pessoa GerarPessoa()
        {
            DateTime data;
            DateTime? dataNasc;
            int id;

            if (!DateTime.TryParse(txtDataNascimento.Value, out data))
                dataNasc = null;
            else
                dataNasc = data;

            return new Pessoa
            {
                PES_DataNascimento = dataNasc,
                PES_Nome = txtNome.Value,
                PES_Id = GetInt(txtCodigo.Value),
                PES_AVA_IdAvatar = GetIntNullable(lstAvatar.SelectedValue,true)
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

            DateTime data;
            if (!DateTime.TryParse(txtDataNascimento.Value, out data))
            {
                Notificar("Não é possível inserir pessoa sem data de nascimento", TipoNotificacao.Alerta);
                return;
            }

            try
            {
                controller.Inserir(GerarPessoa());
                LimparCampos();
                CarregarGrid();
                Notificar("Registro inserido com sucesso!", TipoNotificacao.Sucesso);
            }
            catch (Exception ex)
            {
                InserirLog(ex);
                Notificar("Ocorreu um erro interno ao inserir a pessoa", TipoNotificacao.Erro);
            }
        }
        protected void LimparCampos()
        {
            txtDataNascimento.Value = string.Empty;
            txtNome.Value = string.Empty;
            txtCodigo.Value = string.Empty;
        }

        private void AlterarPessoa(int numreroLinha)
        {
            var pessoa = GerarPessoa(grvBusca.Rows[numreroLinha]);
            if (pessoa == null)
                return;
            controller.Alterar(pessoa);
            Notificar("Registro alterado com sucesso!", TipoNotificacao.Sucesso);
        }

        private void ExcluirPessoa(int numeroLinha)
        {
            try
            {
                controller.Excluir(GerarPessoa(grvBusca.Rows[numeroLinha]).PES_Id);
                btPesquisar_Click(null, null);
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("FK"))
                {
                    Notificar("Não é possível excluir a pessoa, pois há uma tarefa associada à ele.",TipoNotificacao.Alerta);
                    return;
                }
                throw;
            }
        }
    }
}