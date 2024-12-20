function uploadFile() {
    //blockV3("Actualizando posición...");
    $("#progressMark").show();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/ftp_upload.ashx",
        //data: { idModel: $("#selModel").val(), value: value, idUser: $("#txtidUser").val() },
        //async: false,
        //cache:false,
        success: function (data) {
            //r = jQuery.parseJSON(data);
            //removeOverlay();
            //if (r.result === "true") {
            //    //alertS("Se actualizo correctamente la posición de la etiqueta.");
            //}
            //else
            //    alertE(r.MessageError);

            //setMonitor();

            return false;
        },
        error: function () { }
    })
}

function setMonitor() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/setMonitor.ashx",
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#message").html("<div class='alert alert-success alert-dismissible fade show' role='alert'>Servicio de Release activado...: " + r.html
                    + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
                    + "<span aria-hidden='true'>&times;</span>"
                    + "</button>"
                    + "</div>");
                    uploadFile();
                //$("#selModel").chosen();
                //$("#selModel").chosen().change(function () {
                //    //$("#model").text($("#selModel option:selected").text());
                //});
            }
            else
                $("#message").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'>No se pudo conectar al directorio de Release: " + r.html
                                + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
                                + "<span aria-hidden='true'>&times;</span>"
                                + "</button>" 
                                + "</div>");
            //    alert(r.messageError);
            //unblock();
            return false;
        },
        error: function () { }
    });
}