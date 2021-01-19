<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Mapa_Global.aspx.cs" Inherits="App_Mapa_Global" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-1 text-right">
                Hora Salida
                <br />
                <div class="col-xs-12 separador"></div>
                Fecha
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddl_buscarHorario" ClientIDMode="Static" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
                <div class="col-xs-12 separador"></div>
                <asp:TextBox ID="txt_buscarFecha" ClientIDMode="Static" CssClass="form-control input-fecha" runat="server" />
            </div>
            <div class="col-xs-1">
                <div class="col-xs-12 btn-group">
                    <asp:LinkButton ID="btn_buscar" OnClick="btn_refrescar_Click" title="Refrescar rutas" CssClass="btn btn-sm btn-primary" runat="server">
                        <span class="glyphicon glyphicon-search" />
                    </asp:LinkButton>
<%--                    <button type="button" id="btn_nuevo" onclick="$('#btn_rutaNuevo').click();" title="Nueva ruta" class="btn btn-sm btn-success">--%>
                    <button type="button" id="btn_nuevo" onclick="btn_nuevo_Click();" title="Nueva ruta" class="btn btn-sm btn-success">
                        <span class="glyphicon glyphicon-plus" />
                    </button>
                </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-xs-12 btn-group">
                    <button type="button" id="btn_mostrartodos" onclick="btn_todos_Click(true);" title="Mostrar rutas" class="btn btn-sm btn-info">
                        <span class="glyphicon glyphicon-eye-open" />
                    </button>
                    <button type="button" id="btn_ocultartodos" onclick="btn_todos_Click(false);" title="Ocultar rutas" class="btn btn-sm btn-danger">
                        <span class="glyphicon glyphicon-eye-close" />
                    </button>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dv_pills" class="col-xs-9">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="up_contenedor" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="col-xs-8" id="map">
            </div>
            <div id="dv_rutas" class="col-xs-4">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalVehiculo" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Editar detalle ruta
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-xs-4">
                                Número
                                <br />
                                <asp:TextBox ID="txt_editNombre" ClientIDMode="Static" onchange="txt_editNombre_TextChanged(this, this.value);" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Tracto
                                <br />
                                <telerik:RadComboBox ID="ddl_editTracto" ClientIDMode="Static" OnClientSelectedIndexChanged="ddl_editTracto_SelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Tipo Vehículo
                                <br />
                                <telerik:RadComboBox ID="ddl_editTipo" OnSelectedIndexChanged="ddl_vehiculoTipo_SelectedIndexChanged" runat="server" AutoPostBack="true">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Vehículo
                                <br />
                                <telerik:RadComboBox ID="ddl_editTrailer" ClientIDMode="Static" OnClientSelectedIndexChanged="ddl_editTrailer_SelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Conductor
                                <br />
                                <telerik:RadComboBox ID="ddl_editConductor" ClientIDMode="Static" OnClientSelectedIndexChanged="ddl_editConductor_SelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
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
    <div class="modal fade" id="modalColor" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Color Ruta
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-12">
                                Color
                                <asp:TextBox ID="txt_editColor" ClientIDMode="Static" CssClass="form-control color" runat="server" />
                                <div id="colorpicker"></div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">
                                <button type="button" onclick="colorRuta();" class="btn btn-success">
                                    <span class="glyphicon glyphicon-floppy-disk" />
                                </button>
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
                            <button onclick="eliminarRuta();" class="btn btn-success">
                                <span class="glyphicon glyphicon-ok" />
                            </button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="up_hidden" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idRuta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_idPedido" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonRuta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonRutas" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonPedidos" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonOrigenes" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_retorno" ClientIDMode="Static" runat="server" />
            <%--<asp:Button ID="btn_rutaGuardar" ClientIDMode="Static" OnClick="btn_rutaGuardar_Click" CssClass="ocultar" runat="server" />--%>
            <asp:Button ID="btn_rutaNuevo" ClientIDMode="Static" OnClick="btn_rutaNuevo_Click" CssClass="ocultar" runat="server" />
            <asp:Button ID="btn_confEliminarRuta" ClientIDMode="Static" OnClick="btn_confEliminarRuta_Click" CssClass="ocultar" runat="server" />
            <asp:Button ID="btn_detalleAbrir" ClientIDMode="Static" OnClick="btn_detalleAbrir_Click" CssClass="ocultar" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>--%>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=beta"></script>
    <script src="../Scripts/moment.js"></script>
    <script type="text/javascript" src="../Scripts/farbtastic.js"></script>
    <script type="text/javascript" src="../Scripts/lz-string.min.js"></script>
    <script type="text/javascript" src="../Scripts/mapa_global.js"></script>
    <link rel="stylesheet" href="../Scripts/farbtastic.css" type="text/css" />
    <style>
        .label-alt {
            color: black;
            background-color: lightgray;
        }

        .sel-between > td {
            height: 0px;
            border: 0px;
            padding: 0px !important;
        }

        .sel-between.activo > td {
            height: 2px;
            border: 1px;
            padding: 1px !important;
        }

        .sel-between.activo:hover {
            background-color: red;
        }

        .nav-pills > li > a {
            padding: 5px;
            font-size: smaller;
        }

        .nav-pills > li {
            padding-right: 2px;
            border-right: 2px solid lightgray;
            float: none;
        }
        .nav-pills {
            overflow-x: auto;
            display: -webkit-box;
            display: -moz-box;
        }
    </style>
    <script type="text/javascript">
        const retorno_ruta = <%=ConfigurationManager.AppSettings["retorno_ruta"] %>;
        const iconPath = '<%=ConfigurationManager.AppSettings["imgOrigen"] %>';


        function btn_rutaEliminar_Click() {
            $('#<%=lbl_confTitulo.ClientID%>').html('Eliminar Ruta');
            $('#<%=lbl_confMensaje.ClientID%>').html('Se eliminará la ruta seleccionada ¿Desea continuar?');
            $('#modalConf').modal();
        }
    </script>
</asp:Content>

