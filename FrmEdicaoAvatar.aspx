<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmEdicaoAvatar.aspx.cs" Inherits="AgendaTarefaPaginas.FrmEdicaoAvatar" %>

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
                    <input runat="server" id="txtCodigo" type="number" class="form-control" style="width:150px" maxlength="50"/>
                </div>
                <div class="filtroitem">
                    <label class="input-group-text" id="lblNome" for="txtNome">Nome</label>
                    <input runat="server" id="txtNome" type="text" class="form-control" style="width:400px" maxlength="100"/>

                </div>
                <div class="filtroitem">
                    <label class="input-group-text" id="lblImagem" for="ipuImagem">Imagem</label>
                    <input id="ipuImagem" type="file" runat="server" name="inpImagem"/>  
                </div>

            </div>
        </fieldset>
            <div class="botoes">
                <asp:Button runat="server" Text="Pesquisar" ID="btPesquisar" OnClick="btPesquisar_Click" CssClass="btn"/>
                <asp:Button runat="server" Text="Inserir" ID="btInserirNova" OnClick="btInserirNova_Click" CssClass="btn btn-primary"/>      
            </div>
     
        <div class="grid">
            <asp:GridView  AutoGenerateColumns="false" runat="server" ID="grvBusca" CssClass="table table-bordered table-striped center" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="Não foram encontrados resultados" OnRowCommand="grvBusca_RowCommand" DataKeyNames="AVA_Id">
                <Columns>
                    <asp:TemplateField HeaderText="Código" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <label runat="server" type="text"  id="lblCodigo" style="width:150px"><%# Eval("AVA_Id")%></label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imagem"  HeaderStyle-Width="45%">
                        <ItemTemplate>
                            <img src='<%# Eval("AVA_Imagem")%>' runat="server"  style="height:70px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nome"  HeaderStyle-Width="45%">
                        <ItemTemplate>
                            <input runat="server" type="text" id="txtNomeEditar" value='<%# Eval("AVA_Nome")%>' style="width:300px"/>
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
