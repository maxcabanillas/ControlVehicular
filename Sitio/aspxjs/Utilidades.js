//Crea un dialogo para mostrar un mensaje utilizando jQueryUI
function Mensaje(titulo, mensaje, ejecutar) {
    if (mensaje == null) {
        mensaje = titulo;
        titulo = 'Alerta';
    }

    var DialogoMensaje = $('#DialogoMensaje');

    if ($('#DialogoMensaje').html() == null) {

        //Crea el div para el dialogo
        $('body').append('<div id="DialogoMensaje" style="display: none;"> ' + mensaje + '</div>');
    }
    else {
        $('#DialogoMensaje').html(mensaje); 
    }


    // Llama el dialogo
    $('#DialogoMensaje').dialog({
        title: titulo,
        minWidth: 600,
        autoOpen: true,
        modal: true,
        show: "clip",
        hide: "clip",
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                //$('#DialogoMensaje').remove();

                if (ejecutar != null)
                {
                    ejecutar();
                }
            }
        }
    });
}

//Crea un dialogo que valida si o no
function SiNo(titulo, pregunta, eventoSi, eventoNo) {

    // Verifica si existe un div con el nombre y lo retira
    $('#DialogoSiNo').remove();

    // Crea el div para el dialogo
    $('body').append('<div id="DialogoSiNo" style="display: none;"> ' + pregunta + '</div>');

    // Llama el dialogo
    $('#DialogoSiNo').dialog({
        title: titulo,
        minWidth: 600,
        modal: true,
        buttons: [
                        {
                            text: "Si",
                            click: function () {

                                // Llama al evento del si, si esta asigando
                                if (eventoSi != null) {
                                    eventoSi();
                                }

                                $('#DialogoSiNo').dialog('close');

                                // Borra el div 
                                $('#DialogoSiNo').remove();

                            }
                        },
                        {
                            text: "No",
                            click: function () {

                                // Llama al evento del no, si esta asigando
                                if (eventoNo != null)
                                    eventoNo();

                                $('#DialogoSiNo').dialog('destroy');

                                // Borra el div 
                                $('#DialogoSiNo').remove();

                            }
                        }
        ]
    });
}