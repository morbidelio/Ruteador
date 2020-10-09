<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Envio_Ruteador.aspx.cs"
    Inherits="App2_envio_ruteador" MasterPageFile="~/Master/MasterPage.master" %>


<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Maestro Envios
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-lg-12 separador"></div>
    <asp:UpdatePanel ID="up_reload" runat="server">
        <ContentTemplate>
        <asp:Panel ID="filtros" runat="server"  Visible="false">
            <div class="col-lg-1">
                Region
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="ddl_buscarRegion" OnSelectedIndexChanged="ddl_buscarRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                Ciudad
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="ddl_buscarCiudad" OnSelectedIndexChanged="ddl_buscarCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                Comuna
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="ddl_buscarComuna" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3">
                <asp:Label ID="lbl_reload" runat="server" />
                <asp:LinkButton ID="btn_recarga" OnClick="btn_recarga_Click" CssClass="ocultar" runat="server" />
            </div>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-1">
                Fecha Desde
            </div>
            <div class="col-lg-1">
                <asp:TextBox ID="txt_buscarDesde" runat="server" CssClass="form-control input-fecha" />
            </div>
            <div class="col-lg-1">
                Fecha Hasta
            </div>
            <div class="col-lg-1">
                <asp:TextBox ID="txt_buscarHasta" runat="server" CssClass="form-control input-fecha" />
            </div>
            <div class="col-lg-1">
                Hora Salida
            </div>
            <div class="col-lg-1">
                <asp:DropDownList ID="ddl_buscarHorario" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                N° Doc
            </div>
            <div class="col-lg-1">
                <asp:TextBox ID="txt_buscarNro" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            </asp:Panel>
            <div class="col-lg-2 col-lg-push-2">
                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" ToolTip="Buscar">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" runat="server" CssClass="btn btn-success" OnClick="btn_nuevo_Click" ToolTip="Nuevo" Visible="false">
      <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
                <asp:LinkButton ID="btnenviar" runat="server" CssClass="btn btn-info" OnClick="btnenviar_Click" ToolTip="Enviar pedidos" Visible="false">
            <span class="glyphicon glyphicon-send" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_recarga" />
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarRegion" />
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarCiudad" />
            <asp:PostBackTrigger ControlID="btnBuscar" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="col-lg-2 col-lg-pull-2 ocultar">
        Seleccionadas:
    <asp:Label ID="lblgds" runat="server" Text="0" CssClass="tituloCh" />
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel UpdateMode="Conditional" runat="server" ID="upd1">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-nopag" runat="server"
                EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                <Columns>
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_enviar" CssClass="btn btn-warning btn-xs" CommandArgument='<%#Eval("id")%>' CommandName="enviar" runat="server">
                                <span class="glyphicon glyphicon-send" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("id")%>' CommandName="ELIMINAR" runat="server">
                                <span class="glyphicon glyphicon-remove" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                             <asp:BoundField DataField="numero" SortExpression="numero" HeaderText="Nr° Envio" />
                             <asp:BoundField DataField="codigo" SortExpression="codigo" HeaderText="Código Envio" />
                              <asp:BoundField DataField="fecha" SortExpression="fecha" HeaderText="fecha" />
                               <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" Visible="false" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Pedido
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
                            <script src="../Scripts/origen.js" type="text/javascript"></script>
                            <div class="col-lg-8">
                                <div class="col-lg-2">
                                    Número
                                <br />
                                    <asp:TextBox ID="txt_editNumero" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-lg-2">
                                    Código
                                <br />
                                    <asp:TextBox ID="txt_editCodigo" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-lg-2">
                                    Fecha
                                <br />
                                    <asp:TextBox ID="txt_editFecha" CssClass="form-control input-fecha" runat="server" />
                                </div>
                                <div class="col-lg-1">
                                    Peso
                                <br />
                                    <asp:TextBox ID="txt_editPeso" CssClass="form-control input-double" runat="server" />
                                </div>
                                <div class="col-lg-1">
                                    Tiempo
                                <br />
                                    <asp:TextBox ID="txt_editTiempo" CssClass="form-control input-number" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    Hora Salida
                                <br />
                                    <asp:RadioButtonList ID="rb_editHorario" CssClass="rbgroup" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                </div>
                                <%--                              <div class="col-lg-2">
                                    <br />
                                    <asp:RadioButton ID="rb_editHoraAm" Text="AM" GroupName="rbg_horaSalida" runat="server" />
                                    <asp:RadioButton ID="rb_editHoraPm" Text="PM" GroupName="rbg_horaSalida" runat="server" />
                                </div>--%>
                                <div class="col-lg-12 separador"></div>
                                <div class="col-lg-4">
                                    Región
                                <br />
                                    <asp:DropDownList ID="ddl_editRegion" OnSelectedIndexChanged="ddl_editRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    Ciudad
                                <br />
                                    <asp:DropDownList ID="ddl_editCiudad" OnSelectedIndexChanged="ddl_editCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    Comuna
                                <br />
                                    <asp:DropDownList ID="ddl_editComuna" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-12 separador"></div>
                                <div class="col-lg-6">
                                    Dirección
                                <br />
                                    <asp:TextBox ID="txt_editDireccion" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <asp:LinkButton ID="btn_editLatLon" CssClass="btn btn-info" OnClick="btn_editLatLon_Click" runat="server">
                                        <span class="glyphicon glyphicon-map-marker" />
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-2">
                                    Latitud
                                <br />
                                    <asp:TextBox ID="txt_editLat" CssClass="form-control" Enabled="false" ClientIDMode="Static" runat="server" />
                                </div>
                                <div class="col-lg-2">
                                    Longitud
                                <br />
                                    <asp:TextBox ID="txt_editLon" CssClass="form-control" Enabled="false" ClientIDMode="Static" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-4" style="height: 65vh" id="map">
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGuardar" OnClick="btn_editGuardar_Click" CssClass="btn btn-primary" runat="server">
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
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center;">
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

        .rbgroup > tbody > tr > td {
            font-size: 14px;
            color: rgb(53, 146, 255);
        }

            .rbgroup > tbody > tr > td > label {
                padding: 0 10px 0 2px;
            }
    </style>
    <script type="text/javascript">
        function mapa() {
            var mapa = document.getElementById('map');
            var lat = ($('#txt_editLat').val() == '') ? undefined : parseFloat($('#txt_editLat').val().replace(',', '.'));
            var lon = ($('#txt_editLon').val() == '') ? undefined : parseFloat($('#txt_editLon').val().replace(',', '.'));
            cargamapa(12, mapa, lat, lon);
            insertarMarcador(lat, lon, 'icon_pedido.png', '');
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
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
        function EndRequestHandler1(sender, args) {
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
                console.log($("#<%= hseleccionado.ClientID %>").val());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    msj('Debe seleccionar al menos una guía para enviar.', 'warn', true);
                    return false;
                }
            });
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
        var tick_recarga;

        $(document).ready(function (e) {
            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 60000);
        });

        function click_recarga() {
            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true) {
                $('#<%= btn_recarga.ClientID %>')[0].click();
            }
        }

    </script>
</asp:Content>
