<%@ Page ClassName="blankpage" Language="C#" CodePage="65001" MasterPageFile="masterpage.master"
    AutoEventWireup="true" CodeFile="_RegistroVehiculos.aspx.cs" Inherits="blankpage" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <script src="aspxjs/json-min.js"></script>
    <script src="aspxjs/Utilidades.js"></script>
    <% custompage.ShowMessage(); %>
    <!-- Put your custom html here -->
    <style>
        .ui-widget
        {
            font-size: 2em;
        }
    </style>
    <div id="Captura" class="ewGridContent">
        <label class="Etiqueta">
            Tipo Vehiculo:
            <select id="IdTipoVehiculo" class="Campo">
                <%=ObtenerTiposVehiculos() %>
            </select>
        </label>


        <br />
        <label class="Etiqueta">
            Placa:
            <input type="text" id="Placa" class="Campo" />
        </label>

        <br />
        <label class="Etiqueta">
            Observaciones:
            <br />
            <textarea id="Observacion" class="CampoObservacion"></textarea>
        </label>
    </div>
    <a href="#" id="IngresarVehiculo" class="ewGridLink">Ingresar Vehículo</a>

    <a href="#" id="SacarVehiculo" class="ewGridLink">Sacar Vehículo</a>
    <div id="Resultado" style="display:none">
        <label class="Etiqueta">
            Fecha Ingreso:
            <label id="lFechaIngreso">
            </label>
        </label>

        <br />
        <label class="Etiqueta">
            Observaciones:
            <br />
            <label id="lObservaciones">
            </label>
            
        </label>
    
    </div>

    <div id="ConsultaVehiculo"  style="display:none">
        <label class="Etiqueta">
            Documento:
            <label id="lDocumento">
            </label>
        </label>
        <br />

        <label class="Etiqueta">
            Persona:
            <label id="lPersona">
            </label>
        </label>
        <br />

        <label class="Etiqueta">
            Marca Vehiculo:
            <label id="lMarca">
            </label>
        </label>
        <br />

        <label class="Etiqueta">
            Area:
            <label id="lArea">
            </label>
        </label>
        <br />

        <label class="Etiqueta">
            Cargo:
            <label id="lCargo">
            </label>
        </label>
        <br />
    </div>

    <script>
        $(function () {
            var IdTipoVehiculo;
            var Placa;
            var Observacion;

            function BorrarDatos() {
                $('#Placa').val("").focus();
                $('#Observacion').val("");
            }

            function GuardarRegistro() {
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/GuardarRegistroVehiculo',
                    data: "{idTipoVehiculo:" + IdTipoVehiculo + ", placa:'" + Placa + "', observacion:'" + Observacion + "'}",
                    success: function (datos) {
                        BorrarDatos();

                    }
                });
            }

            $('#SacarVehiculo').on('click', function (e) {

                Placa = $('#Placa').val();

                if (Placa == "") {
                    Mensaje('Debe digitar una placa.');
                    $('#Placa').focus();
                    return;
                }

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/ConsultarIngresoVehiculo',
                    data: "{placa:'" + Placa + "'}",
                    success: function (datosIngreso) {
                        var respuestaIngreso = YAHOO.lang.JSON.parse(datosIngreso.d);

                        if (respuestaIngreso.SeEncontro == "True") {
                            $('#lFechaIngreso').text(respuestaIngreso.FechaIngreso);
                            $('#lObservaciones').text(respuestaIngreso.Observaciones);

                            SiNo('Confirmar',$('#Resultado').html(), function() {

                                    $.ajax({
                                        type: 'POST',
                                        contentType: "application/json; charset=utf-8",
                                        url: 'Validaciones.asmx/GuardarSacarVehiculo',
                                        data: "{placa:'" + Placa + "'}",
                                        success: function (datos) {
                                            Mensaje('Se marco la salida del vehiculo con placa ' + Placa);
                                            BorrarDatos();
                                        }
                                    });
                                });
                        }
                        else {
                            Mensaje("No se econtro la placa " + Placa);
                        }


                    }

                });

            });


            $('#IngresarVehiculo').on('click', function (e) {

                e.preventDefault();

                IdTipoVehiculo = $('#IdTipoVehiculo').val();
                Placa = $('#Placa').val();
                Observacion = $('#Observacion').val();

                // Valida el tipo de vehiculo
                if (IdTipoVehiculo == "0") {
                    Mensaje('Debe seleccionar el tipo de vehiculo.');
                    $('#idTipoVehiculo').focus();
                    return;
                }

                // Valida la placa
                if (Placa == "") {
                    Mensaje('No ha digitado la placa.');
                    $('#Placa').focus();
                    return;
                }

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/ValidarVehiculo',
                    data: "{placa:'" + Placa + "'}",
                    success: function (datos) {
                        var respuesta = YAHOO.lang.JSON.parse(datos.d);

                        if (respuesta.Autorizado == "True") {
                            $('#lDocumento').text(respuesta.Documento);
                            $('#lPersona').text(respuesta.Persona);
                            $('#lArea').text(respuesta.Area);
                            $('#lCargo').text(respuesta.Cargo);
                            $('#lMarca').text(respuesta.Marca);

                            Mensaje('Información Vehículo.', $('#ConsultaVehiculo').html(), function () {
                                if (respuesta.PicoyPlaca == "True") {
                                    SiNo('Alerta', 'El vehiculo tiene pico y placa, desea ingresarlo? ', GuardarRegistro);
                                }
                                else {
                                    GuardarRegistro();
                                }
                            });

                        }
                        else {
                            SiNo('Alerta', 'El vehiculo no esta autorizado, desea ingresarlo', GuardarRegistro);
                        }

                    }
                });
            });

        });
    </script>
</asp:Content>
