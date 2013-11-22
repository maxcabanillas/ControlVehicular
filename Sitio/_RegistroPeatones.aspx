<%@ Page ClassName="blankpage" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="_RegistroPeatones.cs" Inherits="blankpage" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>

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
            Tipo Documento:
            <select id="IdTipoDocumento" class="Campo">
                <%=ObtenerTiposDocumentos() %>
            </select>
        </label>

        <label class="Etiqueta">
            Documento:
            <input type="text" id="Documento" class="Campo" />
        </label>

        <br />
        <label class="Etiqueta">
            Nombre:
            <input type="text" id="Nombre" class="Campo" />
        </label>

        <label class="Etiqueta">
            Apellidos:
            <input type="text" id="Apellidos" class="Campo" />
        </label>

        <br />
        <label class="Etiqueta">
            Area:
            <select id="IdArea" class="Campo">
                <%=ObtenerAreas() %>
            </select>
        </label>

        <br />
        <label class="Etiqueta">
            Persona:
            <select id="IdPersona" class="Campo">
            </select>
        </label>

        <br />
        <label class="Etiqueta">
            Observaciones:
            <br />
            <textarea id="Observacion" class="Campo"></textarea>
        </label>
    </div>
    <a href="#" id="IngresarPeaton" class="ewGridLink">Ingresar Peatón</a>

    <a href="#" id="SacarPeaton" class="ewGridLink">Sacar Peatón</a>
    <div id="Resultado" style="display: none">
        <label class="Etiqueta">
            Nombre:
            <label id="lNombre">
            </label>
        </label>
        <br />

        <label class="Etiqueta">
            Apellidos:
            <label id="lApellidos">
            </label>
        </label>
        <br />

        <label class="Etiqueta">
            Fecha Ingreso:
            <label id="lFechaIngreso">
            </label>
        </label>

        <br />
        <label class="Etiqueta">
            Observaciones:
            <br />
            <label id="lObservacion">
            </label>

        </label>

    </div>
    <script>
        $(function () {

            function BorrarDatos() {
                $('#IdTipoDocumento').val("0");
                $('#Documento').val("");
                $('#Nombre').val("");
                $('#Apellidos').val("");
                $('#IdArea').val("0");
                $('#IdPersona').val("0");
                $('#Observacion').val("");
            }


            $('#IdArea').on('change', function () {
                var IdArea = $(this).val();

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/SeleccionarPersonas',
                    data: "{IdArea:" + IdArea + "}",
                    success: function (datos) {
                        $('#IdPersona').html(datos.d);
                    }
                });
            });

            $('#IngresarPeaton').on('click', function (e) {

                e.preventDefault();

                var IdTipoDocumento = $('#IdTipoDocumento').val();
                var Documento = $('#Documento').val();
                var Nombre = $('#Nombre').val();
                var Apellidos = $('#Apellidos').val();
                var IdArea = $('#IdArea').val();
                var IdPersona = $('#IdPersona').val();
                var Observacion = $('#Observacion').val();

                //Validacion
                if (IdTipoDocumento == "0") {
                    Mensaje('Debe seleccionar el tipo de documento');
                    $('#IdTipoDocumento').focus();

                    return;
                }

                if (Documento == "") {
                    Mensaje('Debe digitar el documento');
                    $('#Documento').focus();

                    return;
                }

                if (Nombre == "") {
                    Mensaje('Debe digitar el nombre');
                    $('#Nombre').focus();

                    return;
                }

                if (Apellidos == "") {
                    Mensaje('Debe digitar los apellidos');
                    $('#Apellidos').focus();

                    return;
                }

                if (IdArea == "0") {
                    Mensaje('Debe seleccionar el Area');
                    $('#IdArea').focus();

                    return;
                }

                if (IdPersona == "0") {
                    Mensaje('Debe seleccionar la persona a visitar');
                    $('#IdPersona').focus();

                    return;
                }

                var Parametros =

                "{IdTipoDocumento :" + IdTipoDocumento +
                ", Documento :'" + Documento +
                "', Nombre : '" + Nombre +
                "', Apellidos : '" + Apellidos +
                "', IdArea :" + IdArea +
                ", IdPersona :" + IdPersona +
                ", Observacion :'" + Observacion + "'}"

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/GuardarPeaton',
                    data: Parametros,
                    success: function (datos) {
                        BorrarDatos();
                    }
                });
            });


            // Busca el documento en el registro de los peatones y trae el nombre y los apellidos
            $('#Documento').on('focusout', function (e) {

                var Documento = $('#Documento').val();

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/ConsultarPersona',
                    data: "{Documento:'" + Documento + "'}",
                    success: function (datos) {
                        var respuesta = YAHOO.lang.JSON.parse(datos.d);

                        if (respuesta.SeEncontro == "True") {
                            $('#Nombre').val(respuesta.Nombre);
                            $('#Apellidos').val(respuesta.Apellidos);
                            $('#IdArea').focus();

                        }
                    }
                });
            });

            // Marca la salida del peaton
            $('#SacarPeaton').on('click', function (e) {

                var Documento = $('#Documento').val();

                if (Documento == "") {
                    Mensaje('Debe digitar un documento.');
                    $('#Documento').focus();
                    return;
                }

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'Validaciones.asmx/ConsultarIngresoPeaton',
                    data: "{Documento:'" + Documento + "'}",
                    success: function (datosIngreso) {
                        var respuestaIngreso = YAHOO.lang.JSON.parse(datosIngreso.d);

                        if (respuestaIngreso.SeEncontro == "True") {
                            $('#lNombre').text(respuestaIngreso.Nombre);
                            $('#lApellidos').text(respuestaIngreso.Apellidos);
                            $('#lFechaIngreso').text(respuestaIngreso.FechaIngreso);
                            $('#lObservacion').text(respuestaIngreso.Observacion);

                            SiNo('Confirmar', $('#Resultado').html(), function () {
                                $.ajax({
                                    type: 'POST',
                                    contentType: "application/json; charset=utf-8",
                                    url: 'Validaciones.asmx/GuardarSacarPeaton',
                                    data: "{Documento:'" + Documento + "'}",
                                    success: function (datos) {
                                        Mensaje('Se marco la salida de la persona con documento ' + Documento);
                                        BorrarDatos();
                                    }
                                });

                            });
                        }
                        else {
                            Mensaje("No se econtro el documento para hacer la salida." + Documento);
                        }
                    }
                });
            });

        });
    </script>

</asp:Content>
