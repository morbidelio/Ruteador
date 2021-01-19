<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pedido_Ruteador.aspx.cs"
    Inherits="App2_envio_pedido" MasterPageFile="~/Master/MasterPage.master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-lg-9">
        <h2>Maestro pedidos
        </h2>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-3 text-right" style="padding-top: 20px;">
                <asp:Label ID="lbl_reload" runat="server" />
                <asp:LinkButton ID="btn_recarga" OnClick="btn_recarga_Click" CssClass="ocultar" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_recarga" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel ID="up_reload" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-1 text-right">
                    Región
                </div>
                <div class="col-xs-1">
                    <telerik:RadComboBox ID="ddl_buscarRegion" OnSelectedIndexChanged="ddl_buscarRegion_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Selected="true" Text="Todos..." />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="col-xs-1 text-right">
                    Ciudad
                </div>
                <div class="col-xs-1">
                    <telerik:RadComboBox ID="ddl_buscarCiudad" OnSelectedIndexChanged="ddl_buscarCiudad_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Selected="true" Text="Todos..." />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="col-xs-1 text-right">
                    Comuna
                </div>
                <div class="col-xs-1">
                    <telerik:RadComboBox ID="ddl_buscarComuna" CheckBoxes="true" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Checked="true" Text="Todos..." />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="col-xs-2 col-xs-push-3 btn-group" role="group">
                    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" ToolTip="Buscar">
          <span class="glyphicon glyphicon-search" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btn_nuevo" runat="server" CssClass="btn btn-success" OnClientClick="modalEdit();" OnClick="btn_nuevo_Click" ToolTip="Nuevo">
          <span class="glyphicon glyphicon-plus" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnenviar" runat="server" CssClass="btn btn-info" OnClick="btnenviar_Click" ToolTip="Enviar pedidos">
                <span class="glyphicon glyphicon-send" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btn_Agendar" OnClick="btn_Agendar_Click" runat="server" CssClass="btn btn-primary" ToolTip="Agendar Fecha">
                    <span class="glyphicon glyphicon-calendar" />
                    </asp:LinkButton>
                </div>
            </div>
            <%--<div class="col-xs-12 separador"></div>--%>
            <div class="row">
                <div class="col-xs-1 text-right">
                    Fecha Desde
                </div>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_buscarDesde" runat="server" CssClass="form-control input-fecha" />
                </div>
                <div class="col-xs-1 text-right">
                    Fecha Hasta
                </div>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_buscarHasta" runat="server" CssClass="form-control input-fecha" />
                </div>
                <div class="col-xs-1 text-right">
                    Hora Salida
                </div>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddl_buscarHorario" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1 text-right">
                    N° Doc
                </div>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_buscarNro" runat="server" AutoCompleteType="Search" CssClass="form-control" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarRegion" />
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarCiudad" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="col-xs-2 col-xs-push-10 text-right small">
        Seleccionadas:
        <asp:Label ID="lblgds" runat="server" Text="0" CssClass="tituloCh" />
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-xs-12">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>

                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" CssClass="table table-bordered table-hover tablita" Width="100%" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="Todas" ShowHeader="False" ItemStyle-Width="1%">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkTodos" class="chkTodos" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" data-id='<%#Eval("PERU_ID")%>' class="gridCB chk" onclick="checkIndividual(this);" runat="server" id="check" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" CssClass="btn btn-warning btn-xs" OnClientClick="modalEdit();" CommandArgument='<%#Eval("PERU_ID")%>' CommandName="EDITAR" runat="server">
                                <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("PERU_ID")%>' CommandName="ELIMINAR" runat="server">
                                <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PERU_NUMERO" SortExpression="PERU_NUMERO" HeaderText="Número" />
                        <asp:BoundField DataField="PERU_CODIGO" SortExpression="PERU_CODIGO" HeaderText="Código" />
                        <asp:BoundField DataField="PERU_NOMBRE" SortExpression="PERU_NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="PERU_FECHA" SortExpression="PERU_FECHA" HeaderText="Fecha" />
                        <asp:BoundField DataField="PERU_PESO" SortExpression="PERU_PESO" HeaderText="Peso" />
                        <asp:BoundField DataField="PERU_TIEMPO" SortExpression="PERU_TIEMPO" HeaderText="Tiempo" />
                        <asp:BoundField DataField="REGI_NOMBRE" SortExpression="REGI_NOMBRE" HeaderText="Región" />
                        <asp:BoundField DataField="CIUD_NOMBRE" SortExpression="CIUD_NOMBRE" HeaderText="Ciudad" />
                        <asp:BoundField DataField="COMU_NOMBRE" SortExpression="COMU_NOMBRE" HeaderText="Comuna" />
                        <asp:BoundField DataField="PERU_DIRECCION" SortExpression="PERU_DIRECCION" HeaderText="Dirección" />
                        <asp:BoundField DataField="PERU_LATITUD" SortExpression="PERU_LATITUD" HeaderText="Latitud" />
                        <asp:BoundField DataField="PERU_LONGITUD" SortExpression="PERU_LONGITUD" HeaderText="Longitud" />
                        <asp:BoundField DataField="HORA_COD" SortExpression="HORA_COD" HeaderText="Hora Salida" />
                        <asp:BoundField DataField="envio" SortExpression="envio" HeaderText="Nr° Envio" />
                        <asp:BoundField DataField="propuesta_ruta2" SortExpression="propuesta_ruta2" HeaderText="Nr° Propuesta" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <asp:UpdatePanel ID="up_modalEdit" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Pedido
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-xs-7">
                                        <div class="row">
                                            <div class="col-xs-2">
                                                Número
                                    <br />
                                                <asp:TextBox ID="txt_editNumero" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-xs-2">
                                                Código
                                    <br />
                                                <asp:TextBox ID="txt_editCodigo" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-xs-2">
                                                Fecha
                                    <br />
                                                <asp:TextBox ID="txt_editFecha" CssClass="form-control input-fecha" runat="server" />
                                            </div>
                                            <div class="col-xs-1">
                                                Peso
                                    <br />
                                                <asp:TextBox ID="txt_editPeso" CssClass="form-control input-double" runat="server" />
                                            </div>
                                            <div class="col-xs-1">
                                                Tiempo
                                    <br />
                                                <asp:TextBox ID="txt_editTiempo" CssClass="form-control input-number" runat="server" />
                                            </div>
                                            <div class="col-xs-4">
                                                Hora Salida
                                    <br />
                                                <asp:RadioButtonList ID="rb_editHorario" CssClass="rbgroup" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <%--<div class="col-xs-12 separador"></div>--%>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                Región
                                        <br />
                                                <asp:DropDownList ID="ddl_editRegion" OnSelectedIndexChanged="ddl_editRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-xs-3">
                                                Ciudad
                                        <br />
                                                <asp:DropDownList ID="ddl_editCiudad" OnSelectedIndexChanged="ddl_editCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-xs-3">
                                                Comuna
                                        <br />
                                                <asp:DropDownList ID="ddl_editComuna" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--<div class="col-xs-12 separador"></div>--%>
                                        <div class="row">
                                            <div class="col-xs-6">
                                                Dirección
                                <br />
                                                <asp:TextBox ID="txt_editDireccion" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-xs-1">
                                                <br />
                                                <asp:LinkButton ID="btn_editLatLon" CssClass="btn btn-info" OnClick="btn_editLatLon_Click" runat="server">
                                        <span class="glyphicon glyphicon-map-marker" />
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-xs-2">
                                                Latitud
                                <br />
                                                <asp:TextBox ID="txt_editLat" CssClass="form-control" Enabled="false" ClientIDMode="Static" runat="server" />
                                            </div>
                                            <div class="col-xs-2">
                                                Longitud
                                <br />
                                                <asp:TextBox ID="txt_editLon" CssClass="form-control" Enabled="false" ClientIDMode="Static" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="row">
                                            <div class="col-xs-11">
                                                <asp:GridView ID="gv_detalle" AutoGenerateColumns="False" AllowSorting="true" CssClass="table table-bordered table-hover tablita" Width="100%" runat="server"
                                                    EmptyDataText="No hay productos" OnRowCreated="gv_detalle_RowCreated" OnRowCommand="gv_detalle_RowCommand" DataKeyNames="PEDE_ID">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btn_modificar" OnClientClick="modalDetalle();" CommandArgument='<%#Container.DataItemIndex%>' CommandName="EDITAR" runat="server">
                                <span class="glyphicon glyphicon-edit" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btn_eliminar" CommandArgument='<%#Container.DataItemIndex%>' CommandName="ELIMINAR" runat="server">
                                <span class="glyphicon glyphicon-remove" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NUMERO_GUIA" HeaderText="Nª Guía" />
                                                        <asp:BoundField DataField="CODIGO_PRODUCTO" HeaderText="Código Producto" />
                                                        <asp:BoundField DataField="PEDE_DESC_PRODUCTO" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="NOMBRE_CLIENTE" HeaderText="Nombre Cliente" />
                                                        <asp:BoundField DataField="CODIGO_CLIENTE" HeaderText="Código Cliente" />
                                                        <asp:BoundField DataField="DIRECCION_CLIENTE" HeaderText="Dirección Cliente" />
                                                        <asp:BoundField DataField="COMUNA_CLIENTE" HeaderText="Comuna Cliente" />
                                                        <asp:BoundField DataField="CIUD_NOMBRE_CLIENTE" HeaderText="Ciudad Cliente" />
                                                        <asp:BoundField DataField="REGI_NOMBRE_CLIENTE" HeaderText="Región Cliente" />
                                                        <asp:BoundField DataField="PESO_PEDIDO" HeaderText="Peso" />
                                                        <asp:BoundField DataField="PEDE_CANTIDAD" HeaderText="Cantidad" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="col-xs-2 col-xs-push-5 btn-group" role="group">
                                            <asp:LinkButton ID="btn_editDetalle" OnClientClick="modalDetalle();" OnClick="btn_editDetalle_Click" CssClass="btn btn-primary" runat="server">
                                                <span class="glyphicon glyphicon-plus" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btn_editGuardar" OnClick="btn_editGuardar_Click" CssClass="btn btn-success" runat="server">
                                                <span class="glyphicon glyphicon-floppy-disk" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-xs-5" style="height: 65vh" id="map">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="modalDetalle" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Datos Detalle</h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4>Cliente</h4>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            Código
                                            <br />
                                            <asp:TextBox ID="txt_detalleCodCl" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-4">
                                            Nombre
                                            <br />
                                            <asp:TextBox ID="txt_detalleNomCl" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            Región
                                        <br />
                                            <asp:DropDownList ID="ddl_detalleRegionCl" OnSelectedIndexChanged="ddl_detalleRegionCl_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-3">
                                            Ciudad
                                        <br />
                                            <asp:DropDownList ID="ddl_detalleCiudadCl" OnSelectedIndexChanged="ddl_detalleCiudadCl_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-3">
                                            Comuna
                                        <br />
                                            <asp:DropDownList ID="ddl_detalleComunaCl" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            Dirección
                                        <br />
                                            <asp:TextBox ID="txt_detalleDireccionCl" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4>Producto</h4>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            Nº Guía
                                        <br />
                                            <asp:TextBox ID="txt_detalleNro" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-2">
                                            Código
                                        <br />
                                            <asp:TextBox ID="txt_detalleCod" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-4">
                                            Descripción
                                        <br />
                                            <asp:TextBox ID="txt_detalleDesc" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-2">
                                            Cantidad
                                        <br />
                                            <asp:TextBox ID="txt_detalleCant" CssClass="form-control input-number" runat="server" />
                                        </div>
                                        <div class="col-xs-2">
                                            Peso
                                        <br />
                                            <asp:TextBox ID="txt_detallePeso" CssClass="form-control input-double" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 text-center">
                                <asp:LinkButton ID="btn_detalleGuardar" OnClick="btn_detalleGuardar_Click" CssClass="btn btn-success" runat="server">
                                    <span class="glyphicon glyphicon-ok" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="modalFOTO" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Enviar
                  <asp:Label ID="lbl_cedible" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center;">
                                <asp:LinkButton ID="btncorreo" class="btn btn-primary" OnClick="btncorreo_Click" runat="server">
                    <span class="glyphicon glyphicon-send" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lbl_confTitulo" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lbl_confMensaje" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_confEliminar" CssClass="btn btn-success" OnClick="btn_confEliminar_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalFechaAg" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Modificar Fecha Agendamiento
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-xs-4">
                                Fecha
                  <br />
                                <asp:TextBox ID="txt_fechaAg" CssClass="form-control input-fecha" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Horario 
                  <br />

                                <asp:DropDownList ID="ddl_edithorario" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">No cambiar...</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">

                                <asp:LinkButton ID="btn_AgendarMasivo" runat="server" OnClick="btn_AgendarMasivo_Click" CssClass="btn btn-primary">
                                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_AgendarMasivo" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hseleccionado" runat="server" />
            <asp:HiddenField ID="hf_idEnvio" runat="server" />
            <asp:HiddenField ID="hf_idPeru" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="server">
    <style>
        .seleccionado > td {
            color: green;
        }

        .rbgroup {
            margin-bottom: 0px !important;
        }

            .rbgroup > tbody > tr > td {
                font-size: 14px;
                color: rgb(53, 146, 255);
            }

                .rbgroup > tbody > tr > td > label {
                    padding: 0 10px 0 2px;
                    margin-bottom: 0px;
                }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
    <script src="../Scripts/origen.js" type="text/javascript"></script>
    <script type="text/javascript">
        function mapa() {
            var mapa = document.getElementById('map');
            var lat = ($('#txt_editLat').val() == '') ? undefined : parseFloat($('#txt_editLat').val().replace(',', '.'));
            var lon = ($('#txt_editLon').val() == '') ? undefined : parseFloat($('#txt_editLon').val().replace(',', '.'));
            cargamapa(12, mapa, lat, lon);
            insertarMarcador(lat, lon, 'icon_pedido.png', '');
        }
        function modalEdit() {
            limpiar('modalEdit');
            abrirModal('modalEdit');
        }
        function modalDetalle() {
            limpiar('modalDetalle');
            abrirModal('modalDetalle');
        }
        var ids = [];
        function checkAll(a) {
            $(".gridCB:enabled").prop('checked', $(a).prop('checked'))
            $(".gridCB:enabled").each(function (index, e) {
                var id = $(e).attr("data-id");
                var index = ids.indexOf(id);
                if ($(a).prop('checked')) {
                    if (index == -1) ids.push(id);
                    $(e).parent().parent().addClass("seleccionado");
                }
                else {
                    if (index != -1) ids.splice(index, 1);
                    $(e).parent().parent().removeClass("seleccionado");
                }
            });
            $("#<%=lblgds.ClientID %>").html($(".gridCB:checked").length);
            console.log(ids);
        }
        function checkIndividual(objeto) {
            var cant = parseInt($("#<%= lblgds.ClientID %>").html());
            $('#chkTodos').prop('checked', ($('.gridCB:checked').length == $('.gridCB:enabled').length));
            var id = $(objeto).attr("data-id");
            if ($(objeto).prop('checked')) {
                ids.push(id);
                $(objeto).parent().parent().addClass("seleccionado");
            }
            else {
                ids.splice(ids.indexOf(id), 1);
                $(objeto).parent().parent().removeClass("seleccionado");
            }
            $("#<%=lblgds.ClientID %>").html($(".gridCB:checked").length);
            console.log(ids);
        }
        function drawTableDetalles() {
            tableSPag('<%=gv_detalle.ClientID%>', '40vh');
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoaded);
        function PageLoaded(sender, args) {
            moment.locale('es');
            $('#<%= btnBuscar.ClientID %>').click(function () {
                var desde = moment($('#<%=txt_buscarDesde.ClientID%>').val(), 'DD-MM-YYYY');
                var hasta = moment($('#<%=txt_buscarHasta.ClientID%>').val(), 'DD-MM-YYYY');
                if (desde > hasta) {
                    msj('Fecha inicio debe ser menor.', 'warn', true);
                    return false;
                }
            });
            $('#<%= btnenviar.ClientID%>').click(function () {

                //sumarTotales();
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                // console.log($("#<%= hseleccionado.ClientID %>").val());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    msj('Debe seleccionar al menos un pedido para enviar.', 'warn', true);
                    return false;
                }
            });

            $('#<%= btn_Agendar.ClientID%>').click(function () {

                //sumarTotales();
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                //   console.log($("#<%= hseleccionado.ClientID %>").val());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    msj('Debe seleccionar al menos un pedido para enviar.', 'warn', true);
                    return false;
                }
            });

            if ($('#modalDetalle').hasClass('in')) {
                $('#<%=btn_detalleGuardar.ClientID%>').click(function () {
                    if ($("#<%=txt_detalleCodCl.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleCodCl.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detalleNomCl.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleNomCl.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=ddl_detalleComunaCl.ClientID%>").val() == "" ||
                    $("#<%=ddl_detalleComunaCl.ClientID%>").val() == "0" ||
                    $("#<%=ddl_detalleComunaCl.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detalleDireccionCl.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleDireccionCl.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detalleNro.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleNro.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detalleCod.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleCod.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detalleDesc.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleDesc.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detalleCant.ClientID%>").val() == "" ||
                    $("#<%=txt_detalleCant.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($("#<%=txt_detallePeso.ClientID%>").val() == "" ||
                    $("#<%=txt_detallePeso.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
            });
            }
            if ($('#modalEdit').hasClass('in')) {
                drawTableDetalles();
                $('#<%=btn_editGuardar.ClientID%>').click(function () {
                    if ($("#<%=txt_editNumero.ClientID%>").val() == "" ||
                        $("#<%=txt_editNumero.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_numeroVacio');
                        return false;
                    }
                    if ($("#<%=txt_editCodigo.ClientID%>").val() == "" ||
                        $("#<%=txt_editCodigo.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_codigoVacio');
                        return false;
                    }
                    if ($("#<%=txt_editFecha.ClientID%>").val() == "" ||
                        $("#<%=txt_editFecha.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_fechaVacio');
                        return false;
                    }
                    if ($("#<%=txt_editPeso.ClientID%>").val() == "" ||
                        $("#<%=txt_editPeso.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_pesoVacio');
                        return false;
                    }
                    if ($("#<%=txt_editTiempo.ClientID%>").val() == "" ||
                        $("#<%=txt_editTiempo.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_tiempoVacio');
                        return false;
                    }
                    if ($("#<%=rb_editHorario.ClientID%> input:checked").val() == null) {
                        showAlertClass('guardar', 'warn_horarioVacio');
                        return false;
                    }
                    if ($("#<%=ddl_editRegion.ClientID%>").val() == "" ||
                        $("#<%=ddl_editRegion.ClientID%>").val() == "0" ||
                        $("#<%=ddl_editRegion.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_regionVacio');
                        return false;
                    }
                    if ($("#<%=ddl_editCiudad.ClientID%>").val() == "" ||
                        $("#<%=ddl_editCiudad.ClientID%>").val() == "0" ||
                        $("#<%=ddl_editCiudad.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_ciudadVacio');
                        return false;
                    }
                    if ($("#<%=ddl_editComuna.ClientID%>").val() == "" ||
                        $("#<%=ddl_editComuna.ClientID%>").val() == "0" ||
                        $("#<%=ddl_editComuna.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_comunaVacio');
                        return false;
                    }
                    if ($("#<%=txt_editDireccion.ClientID%>").val() == "" ||
                        $("#<%=txt_editDireccion.ClientID%>").val() == null) {
                        showAlertClass('guardar', 'warn_direccionVacio');
                        return false;
                    }
                    if (($("#<%=txt_editLat.ClientID%>").val() == "" ||
                        $("#<%=txt_editLat.ClientID%>").val() == null) &&
                        ($("#<%=txt_editLon.ClientID%>").val() == "" ||
                            $("#<%=txt_editLon.ClientID%>").val() == null)) {
                        showAlertClass('guardar', 'warn_latLonVacio');
                        return false;
                    }
                });
            }
            tableSPag('<%=gv_listar.ClientID%>');
        }
        var tick_recarga;

        $(document).ready(function (e) {
            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 60000);
        });

        function click_recarga() {
            //debugger;
            if (!$('.modal').hasClass('in')) {
                $('#<%= btn_recarga.ClientID %>')[0].click();
            }
        }




        function EndRequest() {
            var i = 0;
            while (i < ids.length) {
                $("[data-id=" + ids[i] + "]").prop('checked', true);
                i = i + 1;

            }
            $("#<%=lblgds.ClientID %>").html($(".gridCB:checked").length);

        }


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);

    </script>
</asp:Content>
