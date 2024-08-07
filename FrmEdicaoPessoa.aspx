<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmEdicaoPessoa.aspx.cs" Inherits="AgendaTarefaPaginas.FrmEdicaoPessoa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="Content/bootstrap.min.css" rel="stylesheet"/>
        <link href="Content/notification.css" rel="stylesheet"/>
        <link href="Content/AgendaTarefas.css" rel="stylesheet"/>
        <script src="Scripts/jquery-3.3.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js" ></script>
        <script src="Scripts/notification.js" ></script>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


        <fieldset class="fildsetPadrao">
            <legend class="legendPadrao">Busca/Inclusão</legend>
            <div class="filtro">

                <div class="filtroitem">
                    <label class="input-group-text" id="lblCodigo" for="txtCodigo">Código</label>
                    <input runat="server" id="txtCodigo" type="text" class="form-control" style="width:150px" maxlength="50"/>
                </div>
                <div class="filtroitem">
                    <label class="input-group-text" id="lblDescricaoPrioridade" for="txtNome">Nome:</label>
                    <input runat="server" id="txtNome" type="text" class="form-control" style="width:400px" maxlength="100"/>

                </div>
                <div class="filtroitem">
                    <label class="input-group-text" id="lblDataEntrega">Data de Nascimento</label>
                    <input runat="server" id="txtDataNascimento" type="date" class="form-control" />         
                </div>
                <div class="filtroitem">
                    <label class="input-group-text" for="lstAvatar">Avatar</label>
                    <asp:DropDownList runat="server" class="custom-select" ID="lstAvatar" CssClass="form-control">
                    </asp:DropDownList>
                </div>

            </div>

        </fieldset>
            <div class="botoes">
                <asp:Button runat="server" Text="Pesquisar" ID="btPesquisar" OnClick="btPesquisar_Click" CssClass="btn"/>
                <asp:Button runat="server" Text="Inserir" ID="btInserirNova" OnClick="btInserirNova_Click" CssClass="btn btn-primary"/>      
            </div>
     
        <div class="grid">
            <asp:GridView  AutoGenerateColumns="false" runat="server" ID="grvBusca" CssClass="table table-bordered table-striped center" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="Não foram encontrados resultados" OnRowCommand="grvBusca_RowCommand" DataKeyNames="PES_Id">
                <Columns>
                    <asp:TemplateField HeaderText="Código" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <label runat="server"  ><%# Eval("PES_Id")%></label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avatar"  HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <img src='<%# Eval("AVA_Imagem")%>' runat="server"   style="height:70px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nome" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <input runat="server" type="text"  id="txtNomeEditar" value='<%# Eval("PES_Nome")%>' style="width:150px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descrição"  HeaderStyle-Width="45%">
                        <ItemTemplate>
                         <input runat="server" id="txtDataNascimentoEditar" type="date" value='<%# ((DateTime)Eval("PES_DataNascimento")).ToString("yyyy-MM-dd") %>' class="form-control" required="required" />         
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button  runat="server" Text="Alterar"  CommandArgument='<%#((GridViewRow)Container).RowIndex  %>' CommandName="Editar" CssClass="btn btn-primary"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Excluir" Text="Excluir" CssClass="btn btn-danger"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
