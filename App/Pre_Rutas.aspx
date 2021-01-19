<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeFile="Pre_Rutas.aspx.cs" Inherits="App_Pre_Rutas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-xs-12 separador"></div>
    <h2>PROPUESTA RUTAS</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel ID="up_reload" runat="server">
        <ContentTemplate>
            <div class="col-xs-9 oculta">
                <div class="col-xs-1">
                    Region
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_buscarRegion" OnSelectedIndexChanged="ddl_buscarRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Ciudad
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_buscarCiudad" OnSelectedIndexChanged="ddl_buscarCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Comuna
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_buscarComuna" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-3">
                <asp:Label ID="lbl_reload" runat="server" />
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-1 text-right">
                Fecha
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscarFecha" runat="server" CssClass="form-control input-fecha" />
            </div>
            <div class="col-xs-1 text-right">
                Hora Salida
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddl_buscarHorario" CssClass="form-control" runat="server" ClientIDMode="Static">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-1 text-right">
                N° Ruta
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscarNro" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            <div class="col-xs-1 text-right">
                N° envío
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscaenvio" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            <div class="col-xs-2 btn-group">
                <asp:LinkButton ID="btn_buscar" runat="server" CssClass="btn btn-primary" OnClick="btn_buscar_Click" ToolTip="Buscar">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" runat="server" CssClass="btn btn-success" OnClick="btn_nuevo_Click" ToolTip="Nuevo">
      <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_enviar" runat="server" CssClass="btn btn-info" OnClick="btn_enviar_Click" ToolTip="Enviar rutas">
            <span class="glyphicon glyphicon-send" /> 
                </asp:LinkButton>
                <asp:LinkButton ID="btn_pdf" runat="server" CssClass="btn btn-info" OnClick="btn_pre_pdf_click" ToolTip="pdf rutas">
            <span class="glyphicon glyphicon-file" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_eliminar_todos" CssClass="btn btn-danger" OnClick="btn_eliminar_todos_click" runat="server">
                <span class="glyphicon glyphicon-remove" />
                </asp:LinkButton>
            </div>
            <div class="col-xs-2 text-right">
                Seleccionadas:
            <asp:Label ID="lblgds" ClientIDMode="Static" runat="server" Text="0" CssClass="tituloCh" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarRegion" />
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarCiudad" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenedor" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" AutoGenerateColumns="false" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-nopag" runat="server"
                EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound" DataKeyNames="ID,ID_ORIGEN">
                <Columns>
                    <asp:TemplateField HeaderText="Todas" ShowHeader="False" ItemStyle-Width="1%">
                        <HeaderTemplate>
                            <input type="checkbox" id="chkTodos" class="chkTodos" onclick="checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" data-id='<%#Eval("ID")%>' class="gridCB chk" onclick="checkIndividual(this);" runat="server" id="check" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_puntos" CssClass="btn btn-xs btn-primary" CommandArgument='<%#Container.DataItemIndex%>' CommandName="PUNTOS" runat="server">
                                <span class="glyphicon glyphicon-map-marker" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_color" CssClass="btn btn-xs" CommandArgument='<%#Eval("ID")%>' CommandName="COLOR" runat="server">
                                <span class="glyphicon glyphicon-tint" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_detalle" CssClass="btn btn-xs btn-info" CommandArgument='<%#Eval("ID")%>' CommandName="DETALLE" runat="server">
                                <span class="glyphicon glyphicon-list" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-xs btn-danger" CommandArgument='<%#Eval("ID")%>' CommandName="ELIMINAR" runat="server">
                                <span class="glyphicon glyphicon-remove" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NUMERO" SortExpression="NUMERO" HeaderText="NUMERO" />
                    <asp:BoundField DataField="FECHA_DESPACHOEXP" SortExpression="FECHA_DESPACHOEXP" HeaderText="FECHA_DESPACHO" />
                    <asp:BoundField DataField="OBSERVACION" SortExpression="OBSERVACION" HeaderText="OBSERVACION" Visible="false" />
                    <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="RUTA" Visible="false" />
                    <asp:BoundField DataField="ORIGEN_NOMBRE" SortExpression="ORIGEN_NOMBRE" HeaderText="CD ORIGEN" />
                    <asp:BoundField DataField="VIAJE_ESTADO" SortExpression="VIAJE_ESTADO" HeaderText="VIAJE_ESTADO" />
                    <asp:BoundField DataField="tipo_vehiculo" SortExpression="tipo_vehiculo" HeaderText="TIPO VEHICULO" />
                    <asp:BoundField DataField="TRAI_PLACA" SortExpression="TRAI_PLACA" HeaderText="VEHICULO" />
                    <asp:BoundField DataField="TRAC_PLACA" SortExpression="TRAC_PLACA" HeaderText="TRACTO" />
                    <asp:BoundField DataField="COND_NOMBRE" SortExpression="COND_NOMBRE" HeaderText="COND" />
                    <asp:BoundField DataField="ENVIO" SortExpression="ENVIO" HeaderText="ENVIO" />
                    <asp:BoundField DataField="HORARIO" SortExpression="HORARIO" HeaderText="HORARIO" />
                    <asp:BoundField DataField="DURACION" SortExpression="DURACION" HeaderText="DURACION" />
                    <asp:BoundField DataField="puntos" SortExpression="puntos" HeaderText="PUNTOS" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Modals" runat="server">
    <div style="display: none">
        <asp:Panel ID="pnlReport" runat="server" Visible="false">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="BarCode 128" Font-Size="8pt"
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="BarCode 128" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reporte\Pre_Rutas.rdlc" EnableExternalImages="True">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
    <div class="modal fade" id="modalPuntos" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <div class="col-xs-2">
                                <h4 class="modal-title">PROPUESTA RUTA
                                </h4>
                            </div>
                            <asp:UpdatePanel UpdateMode="Always" runat="server" ID="act_cambia">
                                <ContentTemplate>
                                    <div class="col-xs-1">
                                        <asp:DropDownList ID="ddl_puntosCambiarPreruta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_puntosCambiarPreruta_SelectedIndexChanged" ForeColor="Blue"></asp:DropDownList>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoTracto" runat="server" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoTrailer" runat="server" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoConductor" runat="server" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoSalida" runat="server" ClientIDMode="Static" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-7" style="height: 65vh" id="map">
                            </div>
                            <div class="col-xs-5">
                                <div class="col-xs-4">
                                    Punto seleccionado
                                </div>
                                <div class="col-xs-5">
                                    <asp:UpdatePanel UpdateMode="Always" runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <telerik:RadComboBox ID="ddl_puntoNombre" OnClientSelectedIndexChanged="ddl_puntoNombre_SelectedIndexChanged" ClientIDMode="Static" AllowCustomText="true" MarkFirstMatch="true" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-xs-2">
                                    <button onclick="limpiarPuntos();return false;" class="btn btn-danger">
                                        <span class='glyphicon glyphicon-erase' />
                                    </button>
                                </div>
                                <div class="col-xs-12 separador"></div>
                                <div id="tbl_puntos"></div>
                                <div class="col-xs-12 separador"></div>
                                <div class="col-xs-3">
                                    <asp:Label ID="txt_cant_punt" ClientIDMode="Static" runat="server" Text="Cant Puntos" Style="float: left"></asp:Label>
                                </div>
                                <div class="col-xs-3 btn-group" role="group">
                                    <button id="btn_guardarRuta" onclick="guardarRuta();" class="btn btn-success">
                                        <span class="glyphicon glyphicon-floppy-disk" />
                                    </button>
                                    <button id="btn_puntosVehiculo" type="button" class="btn btn-info" data-toggle="modal" data-target="#modalVehiculo">
                                        <span class="glyphicon glyphicon-list" />
                                    </button>
                                </div>
                                <div class="col-xs-2">
                                    <input type="radio" id="rb_ruta" name="rb_mostrar" />
                                    <br />
                                    <label for="rb_ruta">Ruta</label>
                                </div>
                                <div class="col-xs-2">
                                    <input type="radio" id="rb_poligono" name="rb_mostrar" />
                                    <br />
                                    <label for="rb_ruta">Polígono</label>
                                </div>
                                <div class="col-xs-2">
                                    <input type="radio" id="rb_ambos" name="rb_mostrar" checked="checked" />
                                    <br />
                                    <label for="rb_ruta">Ambos</label>
                                </div>
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
    <div class="modal fade" id="modalVehiculo" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Asignar vehículos
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                        <div class="col-xs-4">
                                Número
                                <br />
                                <asp:textbox id="txt_editNombre" ClientIDMode="Static" onchange="txt_editNombre_TextChanged(this, this.value);" cssclass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Tracto
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoTracto" OnClientSelectedIndexChanged="ddl_vehiculoTracto_SelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Tipo Vehículo
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoTipo" OnSelectedIndexChanged="ddl_vehiculoTipo_SelectedIndexChanged" runat="server" AutoPostBack="true">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Vehículo
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoTrailer" OnClientSelectedIndexChanged="ddl_vehiculoTrailer_SelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Conductor
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoConductor" OnClientSelectedIndexChanged="ddl_vehiculoConductor_SelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div id="dv_detalle" runat="server">
                                <div class="col-xs-12 separador"></div>
                                <div class="col-xs-12 text-center">
                                    <asp:LinkButton ID="btn_vehiculoGuardar" OnClick="btn_vehiculoGuardar_Click" CssClass="btn btn-success" runat="server">
                                        <span class="glyphicon glyphicon-floppy-disk" />
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn <%=(string.IsNullOrEmpty(hf_idRuta.Value)) ? "btn-success" : "btn-danger"%>" data-dismiss="modal">
                                <span class="glyphicon <%=(string.IsNullOrEmpty(hf_idRuta.Value)) ? "glyphicon-ok" : "glyphicon-remove"%>" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
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

                            <asp:LinkButton ID="btn_confEliminarTodos" CssClass="btn btn-success" OnClick="btn_confEliminartodos_Click" runat="server" Visible="false">
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
    <div class="modal fade" id="modalenviar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lbl_titulo_enviar" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body" style="height: 70px">
                            <asp:Label ID="Label2" runat="server" />
                            <div class="col-xs-12">
                                <asp:CheckBox runat="server" ID="chk_archivar" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_confEnviar" CssClass="btn btn-success" OnClick="btn_confEnviar_Click" runat="server">
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
                                <asp:TextBox ID="txt_editColor" CssClass="form-control color" runat="server" />
                                <div id="colorpicker"></div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_colorGuardar" runat="server" OnClick="btn_colorGuardar_Click" CssClass="btn btn-success">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
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

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_jsonPedidos" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonOrigenes" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonRuta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_idRuta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_idPedido" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_origen" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_circular" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hseleccionado" runat="server" />
            <asp:HiddenField ID="respuesta_direcction" ClientIDMode="Static" runat="server" />
            <asp:Button ID="btn_puntosGuardar" ClientIDMode="Static" OnClick="btn_puntosGuardar_Click" CssClass="ocultar" runat="server" />
            <asp:Button ID="btn_exportarExcel" ClientIDMode="Static" OnClick="btn_exportarExcel_Click" CssClass="ocultar" runat="server" />
            <asp:Button ID="pdf_post" ClientIDMode="Static" OnClick="btn_pdf_click" CssClass="ocultar" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_exportarExcel" />
            <asp:PostBackTrigger ControlID="pdf_post" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="scripts" runat="server">
    <style>
        .sel-between > td {
            height: 0px;
            border: 0px;
            padding: 0px !important;
        }

        .sel-between {
        }

            .sel-between.enabled > td {
                height: 3px;
                border: 1px;
                padding: 1px !important;
            }

            .sel-between.enabled:hover {
                background-color: red;
            }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=beta"></script>
    <script src="../Scripts/ruteador.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/farbtastic.js"></script>
    <link rel="stylesheet" href="../Scripts/farbtastic.css" type="text/css" />
    <script type="text/javascript">
        const retorno_ruta = <%=ConfigurationManager.AppSettings["retorno_ruta"] %>;
        const iconPath = '<%=ConfigurationManager.AppSettings["imgOrigen"] %>';
        // PageLoad
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
        function EndRequestHandler1(sender, args) {
            setTimeout(tabla2, 100);
            $('#colorpicker').farbtastic('#<%=txt_editColor.ClientID%>');
            $('[name=rb_mostrar').click(function () {
                switch ($(this).attr('id')) {
                    case 'rb_ruta':
                        mostrar(true, false);
                        break;
                    case 'rb_poligono':
                        mostrar(false, true);
                        break;
                    case 'rb_ambos':
                        mostrar(true, true);
                        break;
                }
            });
            $('#<%=btn_colorGuardar.ClientID%>').click(function () {
                if ($('#<%=txt_editColor.ClientID%>').val() == '' ||
                    !$('#<%=txt_editColor.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_colorVacio');
                    return false;
                }
            });
            $('#<%=btn_enviar.ClientID%>').click(function () {
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    showAlertClass("enviar", "warn_noSeleccionados");
                    return false;
                }
            });

            $('#<%=btn_pdf.ClientID%>').click(function () {
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    showAlertClass("enviar", "warn_noSeleccionados");
                    return false;
                }
            });

            $('#<%=btn_eliminar_todos.ClientID%>').click(function () {
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    showAlertClass("enviar", "warn_noSeleccionados");
                    return false;
                }
            });
        }

    </script>
</asp:Content>
