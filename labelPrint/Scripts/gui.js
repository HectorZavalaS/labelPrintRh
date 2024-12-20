function getVirtDir() {
    var Path = location.host;
    var VirtualDirectory;
    if (Path.indexOf("localhost") >= 0 && Path.indexOf(":") >= 0) {
        VirtualDirectory = "";

    }
    else {
        var pathname = window.location.pathname;
        var VirtualDir = pathname.split('/');
        VirtualDirectory = VirtualDir[1];
        VirtualDirectory = '/' + VirtualDirectory;
    }
    return VirtualDirectory;
}

/******************************************************************************************************/
/****************************************** AGREGAR FLUX LED 1 ****************************************/
/******************************************************************************************************/

function getDlgCreateFlux(idModel) {
    //block();
    //loadHtml();
    //alert("getDlgCreateFlux");
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Create",
        async:false,
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Add Flux");
            $("#se_id_model").val(idModel);
            $('#se_id_model').attr('disabled', 'disabled');
            $("#dlgFooter").hide();
            // unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgEditFlux(idModel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Edit",
        data: { id: idModel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $('#se_id_model').attr('disabled', 'disabled');
            $("#dlgTitle").html("Edit Flux");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsFlux(idFlux) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Details",
        data: { id: idFlux },
        success: function (data) {
            $(".modal-dialog").css("width", "300px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Side");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteFlux(idFlux) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Delete",
        data: { id: idFlux },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Side");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
/******************************************************************************************************/
/****************************************** AGREGAR COLOR LED 1 ****************************************/
/******************************************************************************************************/

function getDlgCreateColor(idModel) {
    //block();
    //loadHtml();
    //alert("getDlgCreateFlux");
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addColorModel/Create",
        async: false,
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Add Flux");
            $("#se_id_model").val(idModel);
            $('#se_id_model').attr('disabled', 'disabled');
            $("#dlgFooter").hide();
            // unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgEditColor(idFlux) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Edit",
        data: { id: idFlux },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Side");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsColor(idFlux) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Details",
        data: { id: idFlux },
        success: function (data) {
            $(".modal-dialog").css("width", "300px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Side");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteColor(idFlux) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/addFluxModel/Delete",
        data: { id: idFlux },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Side");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}