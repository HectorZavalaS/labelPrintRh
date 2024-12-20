function createSelect(id) {
    $(id).attr('data-live-search', true);
    $(id).attr('data-size', '10');
    $(id).attr('data-live-search-style', 'contains');
    $(id).attr('data-style', 'btn-info');
    $(id).css('width', '100%');
    $(id).selectpicker();
}
function createSelect(id,size) {
    $(id).attr('data-live-search', true);
    $(id).attr('data-size', '10');
    $(id).attr('data-live-search-style', 'contains');
    $(id).attr('data-style', 'btn-info');
    $(id).css('width', size);
    $(id).selectpicker();
}

function updateSelect(id) {
    $(id).addClass('selectpicker');
    $(id).attr('data-live-search', true);
    $(id).attr('data-size', '10');
    $(id).attr('data-live-search-style', 'contains');
    $(id).attr('data-style', 'btn-info');
    $(id).selectpicker('refresh');
}

//function alertS(mensaje) {
//    BootstrapDialog.alert({
//        message: mensaje,
//        type: BootstrapDialog.TYPE_SUCCESS, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
//        closable: true, // <-- Default value is false
//        draggable: true, // <-- Default value is false
//        buttonLabel: 'Aceptar' // <-- Default value is 'OK',

//    });
//}

function alertS(mensaje) {
    var dialog = BootstrapDialog.alert({
        message: mensaje,
        type: BootstrapDialog.TYPE_SUCCESS,
        closable: false,
        draggable: true,
        buttonLabel: 'Close'

    });
    setTimeout(function () {
        dialog.close();
        //$("#fldPCBSerial").val("");
        //$("#fldPCBSerial").focus();
    }, 3000);
}

function alertE(mensaje) {
    BootstrapDialog.alert({
        message: "<div class='FAIL'>" + mensaje + "</div>",
        type: BootstrapDialog.TYPE_DANGER, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is false
        draggable: true, // <-- Default value is false
        buttonLabel: 'Aceptar' // <-- Default value is 'OK',

    });
}

function alertW(mensaje) {
    BootstrapDialog.alert({
        message: mensaje,
        type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is false
        draggable: true, // <-- Default value is false
        buttonLabel: 'Aceptar' // <-- Default value is 'OK',

    });
}
function alertI(mensaje) {
    BootstrapDialog.alert({
        message: mensaje,
        type: BootstrapDialog.TYPE_INFO, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is false
        draggable: true, // <-- Default value is false
        buttonLabel: 'Aceptar' // <-- Default value is 'OK',

    });
}

function getDjInfo(e) {
    //See notes about 'which' and 'key'
    if (e.keyCode == 13) {
        //setDJRem();
        getPrintedLabel($("#selModel").val());
        getPickedBines($("#selModel").val());
        return false;
    }
}

function loadHtml() {
    $("#loginDlg").html("<div style='margin-left:auto;margin-right:auto; width:70%;'><img src='" + getVirtDir() + "/images/busy.gif' style='width: 40px;margin:15px;' /> Espera un momento...</div>");
}
function loadHtmlV2() {
    $("#sectionMessage").html("<div style='margin-left:auto;margin-right:auto; width:70%;'><img src='" + getVirtDir() + "/images/busy.gif' style='width: 40px;margin:15px;' /> Espera un momento...</div>");
}
function loadHtmlDiv() {
    $("#loginDlg").html("<div style='padding: 15px;margin-left:auto;margin-right:auto;'><img src='" + getVirtDir() + "/images/busy.gif' style='width: 40px;' /> Espera un momento...</div>");
}

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
function block() {
    $.blockUI({
        css: {
            baseZ: 150000,
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        },
        message: "<div style='padding: 15px'><img src='" + getVirtDir() + "/images/busy.gif' style='width: 40px;' /> Espera un momento...<br><label id='loadW'></label></div>"
    });
}

function print() {
    $.blockUI({
        css: {
            baseZ: 150000,
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        },
        message: "<div style='padding: 15px'><img src='" + getVirtDir() + "/images/print.gif' style='width: 100px;' />Imprimiendo etiquetas...<br><label id='loadW'></label></div>"
    });
}

function Mark() {
    $.blockUI({
        css: {
            baseZ: 180000,
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        },
        message: "<div style='padding: 15px'><img src='" + getVirtDir() + "/images/laser.gif' style='width: 100px;' /> <br>En espera de la marcadora...<label id='loadW'></label></div>"
    });
}

function unblock() {
    $.unblockUI();
}


function getPreviewLbl(code, nDiv, line1, line2, idModel, idflx, idVol, idColor, dateDj) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/lblWEBService.ashx",
        data: { code: code, line1: line1, line2: line2, idModel: idModel, idflx: idflx, idColor: idColor, idVol: idVol, dateDj: dateDj },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true")
                $("#" + nDiv).html("<center><img src='" + getVirtDir() + "/Labels/" + r.dirprev + "' style='width: 50%;'></center>");
            if (r.result === "false")
                alert(r.MessageError + " ---- GetPreviewLabel \n" + code + ", " + line1 + ", " + line2 + ", " + idModel + ", " + idflx + ", " + idColor + ", " + idVol + ", " + dateDj);
            //unblock();
            return false;
        },
        error: function(){}
    })
}


function genLblTemplate(idModel, idSide, idflx, idVol, idColor, idRev, divName, nSides, dateDj) {
    //block();
    //alert(dateDj);
    if (idSide != null) {
        $.ajax({
            method: "POST",
            url: getVirtDir() + "/Controllers/genLblTemplate.ashx",
            data: { idModel: idModel, idSide: idSide, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev, nSides: nSides, dateDj: dateDj },
            async: false,
            //cache:false,
            success: function (data) {
                r = jQuery.parseJSON(data);
                if (r.result === "true") {
                    //alert(nSides);
                    $("#" + nSides).val(r.lblTemp);
                    $("#" + nSides + "1").val(r.line1);
                    $("#" + nSides + "2").val(r.line2);
                }
                else
                    alert(r.MessageError + "------ GenLabelTemplate");
                //unblock();
                return false;
            },
            error: function () { }
        });
    }
}
function printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idPrinter, printMode) {
    print();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/printLabel.ashx",
        data: {
            num_lbls: num_lbls, idModel: idModel, idSideL: idSideL, idSideR: idSideR, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev,
            idPrinter: idPrinter, idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(), dateDj: $("#txtDateDJ").val(),
            isRem: $("#isRem").val(), djGrp: $("#txtDjGrp").val(), typePrint: "FOLIO", printMode: printMode
        }, 
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //alert(idModel + " - " + idflx + " - " + idColor + " - " + idVol + " - " + $("#idUser").val() + " - " + num_lbls);
                //newPrint(idModel, idflx, idColor, idVol, $("#idUser").val(), num_lbls, $("#txtDjNo").val(), $("#txtAName").val(), r.lastfolio,r.initfolio);
                var pcbQty;

                if ($("#partPrint").is(':checked')) {
                    pcbQty = $("#pcbDjQtyPar").val();
                } else {
                    pcbQty = $("#pcbDjQty").val();
                }
                insertPartial(idModel, pcbQty, r.ID_RESULT);
                //checkLDM(idModel, num_lbls, $("#cmbPrinters").val());
                //checkFourLbls(idModel, idflx, idVol, idColor, num_lbls, $("#cmbPrinters").val());
                //checkPanel(idModel, idflx, idVol, idColor, num_lbls, $("#cmbPrinters").val());
                //generateExcelSimons($("#txtDjNo").val());
            }
            //$("#lblFrame").html("<center><img src='" + getVirtDir() + "/Labels/" + r.dirprev + "' style='width: 70%;'></center>");
            else
                alert(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}
function printLblSide(idModel, idSide, idflx, idVol, idColor, idRev, num_lbls, idPrinter, typePrint) {
    print();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/printLabelSide.ashx",
        data: {
            num_lbls: num_lbls, idModel: idModel, idSide: idSide, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev,
            idPrinter: idPrinter, idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(), dateDj: $("#txtDateDJ").val(),
            isRem: $("#isRem").val(), djGrp: $("#txtDjGrp").val(), typePrint: typePrint//"FOLIO"
        },
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                var pcbQty;

                if ($("#partPrint").is(':checked')) {
                    pcbQty = $("#pcbDjQtyPar").val();
                } else {
                    pcbQty = $("#pcbDjQty").val();
                }
                if (idSide == "1" || idSide == "2")
                    insertPartial(idModel, pcbQty / 2, r.ID_RESULT);
                else
                    insertPartial(idModel, pcbQty, r.ID_RESULT);
                //checkLDM(idModel, num_lbls, $("#cmbPrinters").val());
                //checkFourLbls(idModel, idflx, idVol, idColor, num_lbls, $("#cmbPrinters").val());
                //checkPanel(idModel, idflx, idVol, idColor, num_lbls, $("#cmbPrinters").val());
            }
            else
                alert(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}

function printLblSideV2(idModel, idSide, idflx, idVol, idColor, idRev, num_lbls, idPrinter, typePrint) {
    print();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/printLabelV2.ashx",
        data: {
            num_lbls: num_lbls, idModel: idModel, idSide: idSide, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev,
            idPrinter: idPrinter, idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(), dateDj: $("#txtDateDJ").val(),
            isRem: $("#isRem").val(), djGrp: $("#txtDjGrp").val(), typePrint: typePrint//"FOLIO"
        },
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                var pcbQty;

                if ($("#partPrint").is(':checked')) {
                    pcbQty = $("#pcbDjQtyPar").val();
                } else {
                    pcbQty = $("#pcbDjQty").val();
                }
                if (typePrint != "TEST") {
                    if (idSide == "1" || idSide == "2")
                        insertPartial(idModel, pcbQty / 2, r.ID_RESULT);
                    else
                        insertPartial(idModel, pcbQty, r.ID_RESULT);
                }
                //checkLDM(idModel, num_lbls, $("#cmbPrinters").val());
                //checkFourLbls(idModel, idflx, idVol, idColor, num_lbls, $("#cmbPrinters").val());
                //checkPanel(idModel, idflx, idVol, idColor, num_lbls, $("#cmbPrinters").val());
            }
            else
                alert(r.MessageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}

function insertPartial(idModel, pcbQty, idSpec) {

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertPartial.ashx",
        data: {
             idModel: idModel, idSpec: idSpec, pcbQty: pcbQty, djNo: $("#txtDjNo").val()
        },
        async: false,
        //cache:false,
        success: function (data) {
            //r = jQuery.parseJSON(data);
            //if (r.result === "true") {

            //else
            //    alert(r.messageError);

            return false;
        },
        error: function () { }
    });
}

function printLblTest(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev,idPrinter, printMode) {
    print();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/printLabel.ashx",
        data: {
            num_lbls: 10, idModel: idModel, idSideL: idSideL, idSideR: idSideR, idflx: idflx, idVol: idVol,
            idColor: idColor, idRev: idRev, idPrinter: idPrinter, dateDj: $("#txtDateDJ").val(), isRem: 0,
            djGrp: $("#txtDjGrp").val(), typePrint: "TEST", printMode : printMode
        },
        //async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //alert(idModel + " - " + idflx + " - " + idColor + " - " + idVol + " - " + $("#idUser").val() + " - " + num_lbls);
                //newPrint(idModel, idflx, idColor, idVol, $("#idUser").val(), num_lbls, $("#txtDjNo").val(), $("#txtAName").val());
            }
            //$("#lblFrame").html("<center><img src='" + getVirtDir() + "/Labels/" + r.dirprev + "' style='width: 70%;'></center>");
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getAllModels() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbAllModels.ashx",
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selModel").html(r.html);
                $("#selModel").selectpicker();
                //$("#selModel").chosen();
                //$("#selModel").chosen().change(function () {
                //getBins($("#selModel").val());
                //});
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}
function getAllPrintedDJs() {
    //block();
    displayOverlay("Cargando información de modelos...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbPrintedDJs.ashx",
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                removeOverlay();
                $("#selModel").html(r.html);
                //$("#selModel").chosen();
                setDJRem();
                getPrintedLabel($("#txtDjNo").val());
                $('#tblPrinted').DataTable({
                    "paging": true,
                    "searching": false,
                    "lengthChange": false,
                    "pageLength": 5,
                    "info": true,
                    "autoWidth": true,
                    "ordering": false,
                    'language': { 'url': getVirtDir() + '/Scripts/Spanish.json' }
                });
                getPickedBines($("#txtDjNo").val());
                $('#tblPicked').DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": false,
                    "pageLength": 5,
                    "ordering": false,
                    "info": true,
                    "autoWidth": true,
                    'language': { 'url': getVirtDir() + '/Scripts/Spanish.json' }
                });
                $("#selModel").chosen().change(function () {
                    setDJRem();
                    getPrintedLabel($("#txtDjNo").val());
                    getPickedBines($("#txtDjNo").val());
                });
            }
            else 
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getAllPrintedDJs_Test() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbPrintedDJs.ashx",
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selModel").html(r.html);
                //$("#selModel").chosen();
                $("#selModel").chosen().change(function () {
                    //getBins($("#selModel").val());
                });
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getPrinters() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getZebraPrinters.ashx",
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#cmbPrinters").html(r.html);
                //$("#cmbPrinters").chosen();
                //$("#cmbPrinters").chosen().change(function () {
                //    //getBins($("#selModel").val());
                //});
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getLaserMarks() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getLaserMarks.ashx",
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#cmbPrinters").html(r.html);
                //$("#cmbPrinters").chosen();
                //$("#cmbPrinters").chosen().change(function () {
                //    //getBins($("#selModel").val());
                //});
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getLabels() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbLabels.ashx",
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selLabel").html(r.html);
                $("#selLabel").selectpicker();
                //$("#selLabel").chosen();
                //$("#selLabel").chosen().change(function () {

                //});
            }
            else
                alertE(r.messageError);
            
            return false;
        },
        error: function () { }
    })
}
function getModelsToPrint() {
    //block();
    blockV3("Cargando información de modelos...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbModelsToPrint.ashx",
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {

                $("#selModel").html(r.html);
                //$("#selModel").chosen();
                createSelect("#selModel");
                setDJASSEMBLY();
                
                getBins($("#selModel").val());
                getStep($("#selModel").val());
                checkIfIsPrinted($("#txtDjNo").val(), $("#selModel").val());
                disableBinCombosR();
                //getBinesByBatch($("#txtDjGrp").val(), $("#selModel").val());
                
                //UpdateDjQty();
                //getDjQty();
                
                //getCants($("input:checked").val());
                //$("input").on("click", function () {
                //    getCants($("input:checked").val());
                //});
                $("#selModel").change(function () {
                    blockV3("Cargando " + $("#selModel option:selected").text() + "...");
                    setDJASSEMBLY();
                    if (!hasBeenPrinted($("#txtDjNo").val(), $("#selModel").val())) {
                        getBins($("#selModel").val());
                        getStep($("#selModel").val());
                    }
                    //UpdateDjQty();
                    //getDjQty();
                    removeOverlay();
                    
                    //checkIfIsPrinted($("#txtDjNo").val(), $("#selModel").val());
                    //getBinesByBatch($("#txtDjGrp").val(), $("#selModel").val());
                    
                });
            }
            else
                alertE('No se pudieron obtener los modelos.');
            removeOverlay();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function setDJRem() {
    var myString = $("#selModel option:selected").text();
    var myStringArray = myString.split(',');
    $("#txtDjNo").val(myStringArray[0]);
    $("#txtDJ").html("DJ NO.: " + myStringArray[0]);
    $("#txtDateDJ").val(myStringArray[2].substring(1, 12));
}

function setDJASSEMBLY() {
    var myString = $("#selModel option:selected").text();
    var myStringArray = myString.split(',');
    $("#txtModelDJ").val(myStringArray[0]);
    $("#txtDjNo").val(myStringArray[2]);
    $("#txtDJ").html("DJ NO: " + myStringArray[2] + " - DJ GROUP: " + myStringArray[4]);
    $("#txtAName").val(myStringArray[3]); 
    $("#txtDateDJ").val(myStringArray[1].substring(1, 12));
    $("#txtDjGrp").val(myStringArray[4]);
}

function getReviews() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getReviews.ashx",
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selRev").html(r.html);
                //$("#selRev").css("width", "90px");
                //$("#selRev").chosen().change(function () {
                //});
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}
function getBins(idmodel) {

    getFluxesByIdModel(idmodel);
    getColorsByIdModel(idmodel);
    getVoltsByIdModel(idmodel);
    getRevsByIdModel(idmodel);
    getSidesByIdModel(idmodel);
    getLabelSize(idmodel);
    initCombos();
}




function getCants(range) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getCants.ashx",
        data: { range: range },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selCant").html(r.html);
                $('#selCant').trigger("chosen:updated");
                $("#selCant").chosen().change(function () {

                });
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}

function getModelDesc(idModel) {
    $("#tblReviews").hide();
    $("#tblAsignLabels").hide();
    
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getModelDescription.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#modelDesc_" + idModel).html(r.html);
                $("#modelDesc_" + idModel).val(r.html);
                $("#txtOver").html("Cargando..." + r.html);
            }
            else
                alertE(r.MessageError);
            return false;
        },
        error: function () { }
    });
}

function getStep(idModel) {

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getStep.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#txtTPrint").val(r.step);
                $("#lblTPrint").html(r.string);
            }
            //else
            //    alertE(r.MessageError);
            return false;
        },
        error: function () { }
    });
}
function checkLDM(idModel, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",

        url: getVirtDir() + "/Controllers/hasLDM.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                alert("El modelo contiene LDM, a continuación se imprimiran las etiquetas.\n Presiona aceptar para continuar...");
                getLDMSpec(idModel, num_lbls, idPrinter);
                //printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idPrinter);
            }
            //else
            //    alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function checkFourSides(idModel, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",

        url: getVirtDir() + "/Controllers/hasFourSides.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                alert("El modelo contiene 4 rollos, a continuación se imprimiran los siguientes 3 rollos.\n Presiona aceptar para continuar...");
                getLDMSpec(idModel, num_lbls, idPrinter);
                //printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idPrinter);
            }
            //else
            //    alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function checkPanel(idModel, idflx, idVol, idColor, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/hasPanel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //alert('Id color:' + idColor);
                alert("El modelo contiene panel, a continuación se imprimiran las etiquetas.\n Presiona aceptar para continuar...");
                getModelPanelSpec(idModel, idflx, idVol, idColor, num_lbls, idPrinter);
                //printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idPrinter);
            }
            //else
            //    alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function checkFourLbls(idModel, idflx, idVol, idColor, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/hasFourLbls.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //alert('Id color:' + idColor);
                alert("El modelo es de 4 etiquetas, a continuación se imprimiran las siguientes etiquetas.\n Presiona aceptar para continuar...");
                getModelSpecF(idModel, idflx, idVol, idColor, num_lbls, idPrinter);
                //printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idPrinter);
            }
            //else
            //    alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getLDMSpec(idModel, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getLDMSpec.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                printLbl(r.idModel, r.idSideL, r.idSideR, r.idFlx, r.idVol, r.idColor, r.idRev, num_lbls, idPrinter);
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getModelSpecF(idModel, idflx, idVol, idColor, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getModelSpecF.ashx",
        data: { idModel: idModel, idColor: idColor },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //alert('Id color:' + idColor);
                printLbl(r.idModel, r.idSideL, r.idSideR, idflx, idVol, r.idColor, r.idRev, num_lbls, idPrinter);
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    });
}
function getModelPanelSpec(idModel, idflx, idVol, idColor, num_lbls, idPrinter) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getModelPanelSpec.ashx",
        data: { idModel: idModel, idColor: idColor },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                var pcbQty;

                if ($("#partPrint").is(':checked')) {
                    pcbQty = $("#pcbDjQtyPar").val();
                } else {
                    pcbQty = $("#pcbDjQty").val();
                }
                //alert('Id color:' + idColor);
                //var numLBL = num_lbls / r.numPCB;
                //if (numLBL < 10) numLBL = 10;
                //alert(numLBL);
                printLbl(r.idModel, r.idSideL, r.idSideR, idflx, idVol, r.idColor, r.idRev, pcbQty, idPrinter);
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getModelSpecRem(djNo, idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getIdsDJSpecByNoDJ.ashx",
        data: { djNo: djNo, idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //alert('Id color:' + idColor);
                //printLbl(r.idModel, r.idSideL, r.idSideR, idflx, idVol, r.idColor, r.idRev, num_lbls, idPrinter);
                $("#selFlxR").val(r.flux);
                $('#selFlxR').trigger("chosen:updated");
                $("#selFlxL").val(r.flux);
                $('#selFlxL').trigger("chosen:updated");
                $("#selVBL").val(r.volt);
                $('#selVBL').trigger("chosen:updated");
                $("#selVBR").val(r.volt);
                $('#selVBR').trigger("chosen:updated");
                $("#selCBL").val(r.color);
                $('#selCBL').trigger("chosen:updated");
                $("#selCBR").val(r.color);
                $('#selCBR').trigger("chosen:updated");
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getLabelSize(idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getLabelSize.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $(".width").html(r.height);
                $(".height").html(r.width);
                $(".height2").html(r.width);
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getFluxesByIdModel(idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getFluxesByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selFlxL").html(r.html);
                updateSelect('#selFlxL');
                $("#selFlxR").html(r.html);
                updateSelect('#selFlxR');

                $("#selFlxL").change(function (e) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    $("#selFlxR").val($("#selFlxL").val());
                    updateSelect('#selFlxR');
                    blockV3("Loading flux configuration...");
                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '') 
                        getLblPreview($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val());
                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
                        getLblPreview($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val());
                    removeOverlay();
                });
            }
            else
                alertE(r.messageError);
            //unblock();
            return false;
        },
        error: function () { }
    })
}



function getVoltsByIdModel(idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getVoltsByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selVBL").html(r.html);
                updateSelect('#selVBL');
                $("#selVBR").html(r.html);
                updateSelect('#selVBR');

                $("#selVBL").change(function (e) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    $("#selVBR").val($("#selVBL").val());
                    updateSelect('#selVBR');
                    blockV3("Loading voltage configuration...");
                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '') 
                        getLblPreview($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val());
                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
                        getLblPreview($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val());
                    removeOverlay();
                });
            }
            else
                alertE(r.messageError);
            //unblock();
            return false;
        },
        error: function () { }
    })
}

function getColorsByIdModel(idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getColorsByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selCBL").html(r.html);
                updateSelect('#selCBL');
                $("#selCBR").html(r.html);
                updateSelect('#selCBR');
                //$("#selSide").chosen();
                $("#selCBL").change(function (e) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    $("#selCBR").val($("#selCBL").val());
                    updateSelect('#selCBR');
                    blockV3("Loading color configuration...");
                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '') 
                        getLblPreview($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val());
                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '') 
                        getLblPreview($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val());
                    removeOverlay();
                });
            }
            else {
                //alert("EL modelo no tiene Bin de color configurado.");
                alert("EL modelo no tiene Bin de color configurado.");
                $("#selCBR").html("<option></option>");
                $('#selCBR').trigger("updated");
                $("#selCBL").html("<option></option>");
                $('#selCBL').trigger("updated");
            }
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getRevsByIdModel(idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getRevByIdModel.ashx",
        data: { idModel: idModel },
        //async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selRev").html(r.html);
                updateSelect('#selRev');
                //$("#selSide").chosen();
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function UpdateDjQty() {
    //block();
    $("#txtOver").html("Calculando cantidad del modelo..." );
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/UpdateDjQty.ashx",
        data: { idModel: $("#selModel").val(), djNo: $("#txtDjNo").val(), djGrp: $("#txtDjGrp").val()},
        //async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {

            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function updateZPLOS() {
    //block();
    blockV3("Actualizando zpl...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/updateZPLOS.ashx",
        data: { idZPL: $("#se_id_zpl").val(), ZPL: $("#se_str_zpl_one").val() },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            removeOverlay();
            if (r.result === "true") {
                alertS("Se acualizo correctamente el zpl.");
            }
            else
                alertE(r.MessageError);
            
            return false;
        },
        error: function () { }
    })
}

function updateZPLTS() {
    //block();
    blockV3("Actualizando zpl...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/updateZPLTS.ashx",
        data: { idZPL: $("#se_id_zpl").val(), ZPL: $("#se_str_zpl_two").val() },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            removeOverlay();
            if (r.result === "true") {
                alertS("Se acualizo correctamente el zpl.");
            }
            else
                alertE(r.MessageError);
            return false;
        },
        error: function () { }
    })
}
function getDjQty() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getDjQty.ashx",
        data: { idModel: $("#selModel").val(), djNo: $("#txtDjNo").val()},
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#pcbDjQty").val(r.qty);
                $("#pcbQty").val(r.qtyPCB);
                $("#selCant").val(r.totLbl);
                $('#partPrint').prop('checked', false);
                $('#pcbDjQtyPar').val('');
                $("#pcbQtyPar").val("");
                $("#selCantPar").val("");
                $('#pcbDjQtyPar').prop('disabled', true);

                if (r.hasPanel === "1") {
                    $("#hasPanel").html("<div style='color:green;'>TIENE ETIQUETA DE PANEL</div>");
                    $("#divPanel").css("background-color","greenyellow");
                }
                else {
                    $("#hasPanel").html("<div style='color:red;'>NO TIENE ETIQUETA DE PANEL</div>");
                    $("#divPanel").css("background-color", "lightpink");
                }
                //$("#selSide").chosen();
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getDjQtyPart() {
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getDjQtyPart.ashx",
        data: { idModel: $("#selModel").val(), djNo: $("#txtDjNo").val(), pcbDjQty: $("#pcbDjQtyPar").val() },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#pcbQtyPar").val(r.qtyPCB);
                $("#selCantPar").val(r.totLbl);
            }
            else
                alertE("<div class='alert alert-danger' role='alert'><center><i class='fa fa-times-circle'></i> <strong>" + r.MessageError + "</strong></center></div>");
            //unblock();
            return false;
        },
        error: function () { }
    });
}


function checkPartM(djGroup, idflx, idVol, idColor, qty, pcbDjQty, printMode) {
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/checkPartM.ashx",
        data: { djGroup: djGroup, idColor: idColor, idflx: idflx, idVol: idVol, qty: qty, pcbDjQty: pcbDjQty, idModel: $("#selModel").val() },
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                getViewPrintLbl($("#selModel").val(), $("#selSideL").val(), $("#selSideR").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selCBL").val(), $("#selRev").val(), qty, printMode);
                $('#dlgGeneral').on('show.bs.modal', function () {
                    var myModal = $(this);
                    //myModal.modal('hide');
                    clearTimeout(myModal.data('hideInterval'));
                });
            }
            if (r.result === "false") {
                //alertE("<div class='alert alert-danger' role='alert'><center><i class='fa fa-times-circle'></i> <strong>" + r.Message +"</strong></center></div>");

                $("#loginDlg").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>No hay suficientes PCB's´para imprimir.</strong> <br><br>" +
                    "Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad."
                    + "<br><br><strong> Correo de contacto: it@siix-sem.com.mx</strong>" +
                    "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    "<span aria-hidden='true'>&times;</span>" +
                    "</button>" +
                    "</div > ");

                //$('#dlgGeneral').on('show.bs.modal', function () {
                //    var myModal = $(this);
                //    //myModal.modal('hide');
                //    clearTimeout(myModal.data('hideInterval'));
                //    myModal.data('hideInterval', setTimeout(function () {
                //        myModal.modal('hide');
                //    }, 3));
                //});

                $("#printDJ").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>No hay suficientes PCB's´para imprimir.</strong> <br><br>" +
                    "Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad."
                    + "<br><br><strong> Correo de contacto: it@siix-sem.com.mx</strong>" +
                    "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    "<span aria-hidden='true'>&times;</span>" +
                    "</button>" +
                    "</div > ");

                document.getElementById("overlay_printed").style.display = "none";

                return;
            }
        },
        error: function () { }
    });
}
function getPickedBines(dj_no) {
    //block();
    $("#tblPicked").hide();
    $("#loader").show();
    $("#loadW").html("Obteniendo bines pickeados");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getPickedBins.ashx",
        data: { dj_no: dj_no },
        //async: false,
        cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $('#tblPicked tbody').html(r.tbl);
                //$('#tblPicked').DataTable().ajax.reload();
                $("#tblPicked").show();
                $("#loader").hide();
                $("#loadW").html("");
            }
            else
                alertE(r.MessageError);
            //unblock();
            return false;
        },
        error: function () { }
    })
}
function getPrintedLabel(dj_no) {
    $("#tblPrinted").hide();
    $("#loader1").show();
    $("#loadWE").html("Obteniendo etiqueta impresa");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getLabelInfo.ashx",
        data: { dj_no: dj_no },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $('#tblPrinted tbody').html(r.tbl);
                ///$('#tblPrinted').DataTable().ajax.reload();
                $('#dModel').val(r.model);
                $('#pokayoke').val(r.pokayoke);
            }
            else {
                alertE(r.messageError);
            }

            $("#tblPrinted").show();
            $("#loader1").hide();
            $("#loadWE").html("");
            
            return false;
        },
        error: function () { }
    })
}

function generateExcelSimons(dj_no) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/generateExcelSimos.ashx",
        data: { dj_no: dj_no },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //$("#selRev").html(r.html);
                //$('#selRev').trigger("chosen:updated");
                //$("#selSide").chosen();
            }
            else
                alert(r.messageError);
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getSidesByIdModel(idModel) {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getSidesByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selSideL").html(r.htmlL);
                updateSelect('#selSideL');
                $("#selSideR").html(r.htmlR);
                updateSelect('#selSideR');
                if (r.nSides === "1") {
                    $("#rSide").hide("clip");
                    if ($("#selSideL").val()!='' && $("#selFlxL").val()!='' && $("#selVBL").val()!='' && $("#selRev").val()!='' && $("#selCBL")!='') 
                        getLblPreview($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val());
                    $("#txtLblCodeR").val("");
                    $("#lblFrameR").hide("clip");
                }
                else {
                    $("#rSide").show("clip");
                    $("#lblFrameR").show("clip");
                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '') 
                        getLblPreview($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val());
                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '') 
                        getLblPreview($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val());
                }
            }
            else
                alert(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getLblPreview(idModel, idSide, idFlx, idVol, idRev, divName, txtSide, idColor) {
    genLblTemplate(idModel, idSide, idFlx, idVol, idColor, idRev, divName, txtSide, $("#txtDateDJ").val());
    getPreviewLbl($("#" + txtSide).val(), divName, $("#" + txtSide + "1").val(), $("#" + txtSide + "2").val(), idModel, idFlx, idVol, idColor);
}

function initDlgPrintLbl(idModel, idSideL, idSideR, idflx, idVol ,idColor, idRev, num_lbls) {
    //genLblTemplate(79, 1, 2, 2, 4);

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getPreviewLabels.ashx",
        data: { num_lbls: num_lbls, idModel: idModel, idSideL: idSideL, idSideR: idSideR, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev, dateDj: $("#txtDateDJ").val(), isRem: $("#isRem").val()},
        success: function (data) {
            var r = jQuery.parseJSON(data);
            
            if (r.result === 'true') {
                $('#topPrev').html(r.htmlTop);
                $('#botPrev').html(r.htmlBot);
                $('#tblEtiquetas tbody').html(r.tblTop);
                $('#tblEtiquetas').DataTable({
                    "paging": false,
                    //"order": [[0, "asc"]],
                    "recordsFiltered": 10,
                    "lengthChange": false,
                    "searching": false,
                    "pageLength": 5,
                    "ordering": false,
                    "info": false,
                    "autoWidth": false,
                    'language': { 'url': getVirtDir() + '/Scripts/Spanish.json' }
                });
            }
            return false;
        },
        error: function () { }
    });
}
function getViewPrintLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode) {
    blockV2("");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/infomodel/",
        //async: false,
        success: function (data) {
            //initDlgPrintLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls);
            $(".modal-dialog").css("width", "600px");

            $("#loginDlg").html(data);

            //$("#selModel option:selected").text();
            $("#txtModel").html($("#txtModelDJ").val());
            $("#txtFlxL").html($("#selFlxL option:selected").text());
            $("#txtCBL").html($("#selCBL option:selected").text());
            $("#txtVBL").html($("#selVBL option:selected").text());

            $("#dlgTitle").html("ETIQUETAS GENERADAS SEGUN ESPECIFICACIONES");
            $("#dlgFooter").html("<button class='btn btn-sm btn-success'  id='btnContinue'>" +
                "<span class='glyphicon glyphicon-triangle-right' style='margin-right:7px;'></span>Continuar..." +
                "</button>" +
                "<button type='button' class='btn btn-danger' data-dismiss='modal'><span class='glyphicon glyphicon-remove-circle' style='margin-right:5px;'></span>Cerrar</button>"
            );
            
            $("#dlgFooter").show();
            //unblock();
            removeOverlay();
            $("#btnContinue").click(function () {
                showDlgPrint(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode);
                //showDlgMark(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode);
            });
            return false;
        },
        error: function () { }
    });

}
function showDlgPrint(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode) {
    //blockV2("");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/dlgPrintlbl/",
        async: false,
        success: function (data) {
            initDlgPrintLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls);
            $(".modal-dialog").css("width", "550px");
            $(".modal-dialog").css("max-width", "550px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("ETIQUETAS GENERADAS SEGUN ESPECIFICACIONES");
            $("#dlgFooter").html("<select class='form-control form-control-sm' id='cmbPrinters' style='text-align:left;width:240px; display:inline; margin-left:15px;'></select>" +
                "<div id='labelPos' style='width:300px;text-align:center;'>" +
                "<div id='up'><button type='button' class='btn btn-sm btn-info' id='btnUp'><i class='metismenu-icon pe-7s-up-arrow' style='font-size: 18px;'></i></button></div>" +
                "<div id='lr'><button type='button' class='btn btn-sm btn-info' id='btnLeft' style='margin-right:30px;'><i class='metismenu-icon pe-7s-left-arrow' style='font-size: 18px;'></i></button>" +
                             "<button type='button' class='btn btn-sm btn-info' id='btnRight'><i class='metismenu-icon pe-7s-right-arrow' style='font-size: 18px;'></i></div>" +
                "<div id='down'><button type='button' class='btn btn-sm btn-info' id='btnDown'><i class='metismenu-icon pe-7s-bottom-arrow' style='font-size: 18px;'></i></button></div>" +
                "</div> " +
                "<button type='button' class='btn btn-sm btn-danger' data-dismiss='modal' id='btnClose'> " +
                "<span class='glyphicon glyphicon-remove-circle' style='margin-right:7px;'></span>Cancelar" +
                "</button>" +
                "<button class='btn btn-sm btn-info'  id='btnTestPrint'>" +
                "<span class='glyphicon glyphicon-print' style='margin-right:7px;'></span>Imprimir prueba" +
                "</button>" +
                "<button class='btn btn-sm btn-success'  id='btnPrint'>" +
                "<span class='glyphicon glyphicon-print' style='margin-right:7px;'></span>Imprimir" +
                "</button>");
            getPrinters();
            $("#dlgFooter").show();
            $('#btnPrint').prop('disabled', false);
            unblock();
            $('#btnUp').click(function () {
                    updateLblPositionVer(-5);
            });
            $('#btnLeft').click(function () {
                    updateLblPositionHor(-5);
            });
            $('#btnRight').click(function () {
                    updateLblPositionHor(5);
            });
            $('#btnDown').click(function () {
                    updateLblPositionVer(5);
            });
            $("#btnPrint").click(function () {
                $('#btnPrint').prop('disabled', true);
                //if ($("#txtTPrint").val() === "1") {
                //    printLblSideV2(idModel, idSideL, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO");
                //    if (idSideL === "1") {
                //        alert("Se imprimiran las etiquetas del lado RH");
                //        printLblSideV2(idModel, 2, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO");
                //    }
                //}
                //else
                //    printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), printMode);
                printLblSideV2(idModel, 2, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO");

            });
            $("#btnTestPrint").click(function () {
                
                //if ($("#txtTPrint").val() === "1") {
                //    printLblSideV2(idModel, idSideL, idflx, idVol, idColor, idRev, 10, $("#cmbPrinters").val(),"TEST");
                //    if (idSideL === "1") {
                //        alert("Se imprimiran las etiquetas del lado RH");
                //        printLblSideV2(idModel, 2, idflx, idVol, idColor, idRev, 10, $("#cmbPrinters").val(), "TEST");
                //    }
                //}
                //else
                //    printLblTest(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, $("#cmbPrinters").val(), "1");
                printLblSideV2(idModel, 2, idflx, idVol, idColor, idRev, 10, $("#cmbPrinters").val(), "TEST");

            });
            $("#btnClose").click(function () {
                location.reload(true);
            });
            return false;
        },
        error: function () { }
    });
}




function updateLblPositionHor(value) {

    blockV3("Actualizando posición...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/updateLblPositionHor.ashx",
        data: { idModel: $("#selModel").val(), value: value, idUser: $("#txtidUser").val() },
        //async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            removeOverlay();
            if (r.result === "true") {
                //alertS("Se actualizo correctamente la posición de la etiqueta.");
            }
            else
                alertE(r.MessageError);

            return false;
        },
        error: function () { }
    })
}
function updateLblPositionVer(value) {

    blockV3("Actualizando posición...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/updateLblPositionVer.ashx",
        data: { idModel: $("#selModel").val(), value: value, idUser: $("#txtidUser").val() },
        //async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            removeOverlay();
            if (r.result === "true") {
                //alertS("Se actualizo correctamente la posición de la etiqueta.");
            }
            else
                alertE(r.MessageError);

            return false;
        },
        error: function () { }
    })
}
function checkIfIsPrinted(dj_no, idModel) {
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/isPrinted.ashx",
        data: { dj_no: dj_no, idModel: idModel },
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //$("#printDJ").html("Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad.");
                $("#printDJ").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>La DJ ya se imprimió.</strong> <br><br>" +
                    "Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad."
                    + "<br><br><strong> Correo de contacto: it@siix-sem.com.mx</strong>"+
                    "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>"+
                        "<span aria-hidden='true'>&times;</span>"+
                      "</button>" +
                    "</div > ");

                //alertW("<div class='alert alert-warning' role='alert'><i class='fa fa-warning'></i> <strong>La DJ ya se imprimió.</strong> <br><br>" +
                //    "Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad."
                //    + "<br><br><strong> Correo de contacto: it@siix-sem.com.mx</strong></div > ");
                document.getElementById("overlay_printed").style.display = "none";
                $('#btnImpEt').attr('disabled', 'disabled');
            }
            if (r.result === "false") {
                $("#printDJ").html("");
                document.getElementById("overlay_printed").style.display = "block";
                $('#btnImpEt').prop('disabled', false);
                UpdateDjQty();
                getDjQty();

            }

            return false;
        },
        error: function () { }
    });
}
function hasBeenPrinted(dj_no, idModel) {
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/hasBeenPrinted.ashx",
        data: { dj_no: dj_no, idModel: idModel },
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //$("#printDJ").html("Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad.");
                $("#printDJ").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>La DJ ya se imprimió.</strong> <br><br>" +
                    "Para habilitar nuevamente la impresión necesitas autorización de un supervisor."
                    + "<br><br><strong> Da click en el boton de autorizar para desbloquear la impresión.</strong>" +
                    //"  <button type='button' class='btn btn-primary' data-dismiss='alert'>Autorizar</button>" +
                    "  <button type='button' id='btnAuth' class='btn btn-primary' data-toggle='modal' data-target='#dlgGeneral' onclick = ''>Autorizar</button>" +
                    //"  <button type='button' id='btnAuth' class='btn btn-primary' onclick = ''>Autorizar</button>" +
                    //"  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    //"<span aria-hidden='true'>&times;</span>" +
                    //"</button>" +
                    "</div > ");
                document.getElementById("overlay_printed").style.display = "none";
                $("#printDJ").show();
                $('#btnImpEt').attr('disabled', 'disabled');
                //$('#btnAuth').click(function () {
                //    $('#dlgGeneral').modal('show');
                //});
                
                $.ajax({
                    method: "GET",
                    url: getVirtDir() + "/authDialog/",
                    success: function (data) {
                        //$(".modal-dialog").css("width", "500px");
                        $("#loginDlg").html(data);
                        //$("#dlgTitle").html("Create Model");
                        //$("#dlgFooter").hide();
                        //unblock();
                        return false;
                    },
                    error: function () { }
                });
                return false;
            }
            $("#dlgFooter").hide();
            if (r.result === "false") {
                $("#printDJ").html("");
                document.getElementById("overlay_printed").style.display = "block";
                $('#btnImpEt').prop('disabled', false);
                //UpdateDjQty();
                //getDjQty();
                return true;
            }
            return false;
        },
        error: function () { }
    });
}
function showDlgARP() {
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/djAlreadyPrint/",
        success: function (data) {
            $(".modal-dialog").css("width", "600px");

            $("#loginDlg").html(data);
            $("#dlgTitle").html("AVISO...");
            $("#dlgFooter").html("<button type='button' class='btn btn-sm btn-danger' data-dismiss='modal' id='btnClose'> " +
                "<span class='glyphicon glyphicon-remove-circle' style='margin-right:7px;'></span>Cancelar" +
                "</button>");
            $("#dlgFooter").show();
            return false;
        },
        error: function () { }
    });

}
function getDlgUpdatePN() {
    block();

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/importPartNumber/",
        async: false,
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("IMPORT PART NUMBERS");
            $("#dlgFooter").html("<button type='button' class='btn btn-sm btn-danger' data-dismiss='modal' id='btnClose'>" +
                "<span class='glyphicon glyphicon-remove-circle' style='margin-right:7px;'></span>Cancelar" +
                "</button>");
            $("#dlgFooter").show();
            unblock();
            return false;
        },
        error: function () { }
    });
}    
/******************************************************************************************************/
/********************************************* MODELOS ************************************************/
/******************************************************************************************************/

function getDlgCreateModel() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_models/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Model");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}  

function getDlgEditModel(idModel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_models/Edit",
        data: { id: idModel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Model");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    }); 
}

function getDlgDetailsModel(idModel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_models/Details",
        data: { id: idModel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Model");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteModel(idModel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_models/Delete",
        data: { id: idModel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Model");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/********************************************* PROYECTOS **********************************************/
/******************************************************************************************************/

function getDlgCreateProject() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_projects/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Project");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}  

function getDlgEditProy(idProy) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_projects/Edit",
        data: { id: idProy },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Project");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsProy(idProy) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_projects/Details",
        data: { id: idProy },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Project");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteProy(idProy) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_projects/Delete",
        data: { id: idProy },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Project");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/*********************************************** VOLTAGE **********************************************/
/******************************************************************************************************/

function getDlgCreateVolt() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_voltageb/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Voltage");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
} 
function getDlgEditVolt(idVolt) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_voltageb/Edit",
        data: { id: idVolt },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Voltage");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsVolt(idVolt) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_voltageb/Details",
        data: { id: idVolt },
        success: function (data) {
            $(".modal-dialog").css("width", "300px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Voltage");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteVolt(idVolt) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_voltageb/Delete",
        data: { id: idVolt },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Voltage");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
/******************************************************************************************************/
/************************************************ LADOS ***********************************************/
/******************************************************************************************************/
function getDlgCreateSide() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_sides/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Side");
            $("#dlgFooter").hide();
           // unblock();
            return false;
        },
        error: function () { }
    });
} 
function getDlgEditSide(idSide) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_sides/Edit",
        data: { id: idSide },
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
function getDlgDetailsSide(idSide) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_sides/Details",
        data: { id: idSide },
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

function getDlgDeleteSide(idSide) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_sides/Delete",
        data: { id: idSide },
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
/******************************************* REVISIONES ***********************************************/
/******************************************************************************************************/
function getDlgCreateReview() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_reviews/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Review");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
} 
function getDlgEditRev(idRev) {
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_reviews/Edit",
        data: { id: idRev },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Review");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsRev(idRev) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_reviews/Details",
        data: { id: idRev },
        success: function (data) {
            $(".modal-dialog").css("width", "300px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Review");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteRev(idRev) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_reviews/Delete",
        data: { id: idRev },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Review");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
/******************************************************************************************************/
/*********************************************** FLUX *************************************************/
/******************************************************************************************************/

function getDlgCreateFlux() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_flxb/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Flux");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}  
function getDlgEditFlx(idFLx) {
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_flxb/Edit",
        data: { id: idFLx },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Flux");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsFlx(idFLx) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_flxb/Details",
        data: { id: idFLx },
        success: function (data) {
            $(".modal-dialog").css("width", "300px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Flux");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteFlx(idFLx) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_flxb/Delete",
        data: { id: idFLx },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Flux");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/*********************************************** CODIGO ZPL *******************************************/
/******************************************************************************************************/

function getDlgCreateCZPL() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_zpl/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create ZPL");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgEditCZPL(idZPL,TYPEZPL) {
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/editZPL/Edit",
        data: { id: idZPL },
        success: function (data) {
            //alert(TYPEZPL);

            $(".modal-dialog").css("width", "800px");
            $("#loginDlg").html(data);
            $("#typezpl").val(TYPEZPL);
            $("#dlgTitle").html("Edit ZPL");
            if (TYPEZPL == 0) {
                $("#zplOS").show();
                $("#zplTS").hide();
            }
            if (TYPEZPL == 1) {
                $("#zplOS").hide();
                $("#zplTS").show();
            }
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsCZPL(idZPL) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_zpl/Details",
        data: { id: idZPL },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details ZPL");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteCZPL(idZPL) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_zpl/Delete",
        data: { id: idZPL },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete ZPL");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/*********************************************** ETIQUETAS ********************************************/
/******************************************************************************************************/

function getDlgCreateLabel() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_labels/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Create Label");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgEditLabel(idLabel) {
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_labels/Edit",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Label");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsLabel(idLabel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_labels/Details",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Label");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteLabel(idLabel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_labels/Delete",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Label");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/*************************************** ASIGNACION DE ETIQUETAS **************************************/
/******************************************************************************************************/

function getDlgCreateAsignLabel() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Asign Label to model");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function AsignLabel(idModel, idLabel) {
    var result = "false";
    block();
    //loadHtml();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertAsignLabel.ashx",
        data: { id_model: idModel, id_label: idLabel },
        success: function (data) {
            unblock();
            var res = jQuery.parseJSON(data);

            alert(res.html);
            if (res.result === "false")
                result = "false";
            else {
                $('#dlgGeneral').modal('toggle');
                location.reload(true);
                result = "true";
            }
        },
        error: function () {
            result = "false";
        }
    });
    return result;
}

function getDlgEditLabel(idLabel) {
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Edit",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Asign");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsLabel(idLabel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Details",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Asign");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteLabel(idLabel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Delete",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Asign");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/*************************************** ASIGNACION DE REVISIONES A MODELO ****************************/
/******************************************************************************************************/

function getDlgCreateAsignRev() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_revModel/Create",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Asign Review to model");
            $("#dlgFooter").hide();

            //unblock();
            return false;
        },
        error: function () { }
    });
}

function AsignReview(idModel, idReview) {
    var result = "false";
    block();
    //loadHtml();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertAsignReview.ashx",
        data: { id_model: idModel, id_review: idReview },
        success: function (data) {
            unblock();
            var res = jQuery.parseJSON(data);

            alert(res.html);
            if (res.result === "false")
                result = "false";
            else {
                $('#dlgGeneral').modal('toggle');
                location.reload(true);
                result = "true";
            }
        },
        error: function () {
            result = "false";
        }
    });
    return result;
}

function getDlgEditLabel(idLabel) {
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Edit",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Edit Asign");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
function getDlgDetailsLabel(idLabel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Details",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Details Asign");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}

function getDlgDeleteLabel(idLabel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/se_asignLabels/Delete",
        data: { id: idLabel },
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Delete Asign");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}
/******************************************************************************************************/
/******************************************************************************************************/
/******************************************************************************************************/

function getDlgLogin() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/Account/Login",
        success: function (data) {
            $(".modal-dialog").css("width", "500px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("Login");
            $("#dlgFooter").hide();
            //unblock();
            return false;
        },
        error: function () { }
    });
}  
function getSpecView() {
    //block();

    $.ajax({
        method: "GET",
        url: getVirtDir() + "/modelSpec/",
        success: function (data) {
            $("#printSpec").html(data);
            unblock();
            return false;
        },
        error: function () { }
    });
}  

function getSpecViewEsp() {
    //block();

    $.ajax({
        method: "GET",
        url: getVirtDir() + "/specials/",
        success: function (data) {
            $("#printSpec").html(data);
            unblock();
            return false;
        },
        error: function () { }
    });
}  

function getSpecViewLaserMark() {
    //block();

    $.ajax({
        method: "GET",
        url: getVirtDir() + "/laserMark/",
        success: function (data) {
            $("#printSpec").html(data);
            unblock();
            return false;
        },
        error: function () { }
    });
}  
function disableBinCombosL() {

    $('#selFlxL').attr('disabled', 'disabled');
    $('#selCBL').attr('disabled', 'disabled');
    $('#selVBL').attr('disabled', 'disabled');
    $('#selRev').attr('disabled', 'disabled');

    updateSelect('#selFlxL');
    updateSelect('#selCBL');
    updateSelect('#selVBL');
    updateSelect('#selRev');

}

function disableBinCombosLLed2() {

    $('#selFlxL1').attr('disabled', 'disabled');
    $('#selCBL1').attr('disabled', 'disabled');
    $('#selVBL1').attr('disabled', 'disabled');

    updateSelect('#selFlxL');
    updateSelect('#selCBL');
    updateSelect('#selVBL');
    updateSelect('#selRev');

}
function disableBinCombosR() {

    $('#selFlxR').attr('disabled', 'disabled');
    $('#selCBR').attr('disabled', 'disabled');
    $('#selVBR').attr('disabled', 'disabled');
    $('#selRev').attr('disabled', 'disabled');

    updateSelect('#selFlxR');
    updateSelect('#selCBR');
    updateSelect('#selVBR');
    updateSelect('#selRev');
}
function disableBinCombosRLed2() {

    $('#selFlxR1').attr('disabled', 'disabled');
    $('#selCBR1').attr('disabled', 'disabled');
    $('#selVBR1').attr('disabled', 'disabled');

    updateSelect('#selFlxR1');
    updateSelect('#selCBR1');
    updateSelect('#selVBR1');
}

function enableBinCombosR() {

    $("#selFlxR").removeAttr('disabled');
    $("#selCBR").removeAttr('disabled');
    $("#selVBR").removeAttr('disabled');

    updateSelect('#selFlxR');
    updateSelect('#selCBR');
    updateSelect('#selVBR');
}
function enableBinCombosRLed2() {

    $('#selFlxR1').attr('disabled', 'disabled');
    $('#selCBR1').attr('disabled', 'disabled');
    $('#selVBR1').attr('disabled', 'disabled');

    $("#selFlxR").removeAttr('disabled');
    $("#selFlxR1").removeAttr('disabled');
    //$("#selCBR1").removeAttr('disabled');
    //$("#selVBR1").removeAttr('disabled');

    updateSelect('#selFlxR');
    updateSelect('#selFlxR1');
    updateSelect('#selCBR1');
    updateSelect('#selVBR1');
}
function initCombos() {
    //$("#selSideL").chosen();
    ////$("#selFlxL").chosen();
    ////$("#selCBL").chosen();
    ////$("#selVBL").chosen();

    createSelect("#selSideL");
    createSelect("#selSideR");
    createSelect("#selFlxL");
    createSelect("#selCBL");
    createSelect("#selVBL");

    createSelect("#selFlxR");
    createSelect("#selCBR");
    createSelect("#selVBR");
    createSelect("#selRev");

    //$("#selSideR").chosen();
    //$("#selFlxR").chosen();
    //$("#selCBR").chosen();
    //$("#selVBR").chosen();
    //$("#selRev").chosen();
}
function initCombosLed2() {
    createSelect("#selFlxL1");
    createSelect("#selCBL1");
    createSelect("#selVBL1");
    createSelect("#selFlxR1");
    createSelect("#selCBR1");
    createSelect("#selVBR1");
    //$("#selFlxL1").chosen();
    //$("#selCBL1").chosen();
    //$("#selVBL1").chosen();
    //$("#selFlxR1").chosen();
    //$("#selCBR1").chosen();
    //$("#selVBR1").chosen();
}
function displayOverlay(text) {
    $("<table id='overlay'><tbody><tr><td><img src='"
        + getVirtDir() + "/images/loading.gif' style='' /><br/><div id='txtOver'>"
        + text + "</div></td></tr></tbody></table>").css({
        "position": "fixed",
        "top": "0px",
        "left": "0px",
        "width": "100%",
        "height": "100%",
        "background-color": "rgba(0,0,0,.5)",
        "z-index": "10000",
        "vertical-align": "middle",
        "text-align": "center",
        "color": "#fff",
        "font-size": "40px",
        "font-weight": "bold",
        "cursor": "wait"
    }).appendTo("body");
}

function blockV2(text) {
    $("<table id='overlay' style='margin-top:60px;'><tbody><tr><td>"
        + "<div class='cssload-wrap'>" +
        "<div class= 'translate' >" +
        "<div class='scale'></div>" +
	    "    </div >" +
        "</div >" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<div class='cssload-wrap'>" +
        "    <div class='translate'>" +
        "        <div class='scale'></div>" +
        "    </div>" +
        "</div>" +
        "<br/><br/><br/><div id='txtOver'>" + text + "</div>" +
        "</td></tr></tbody></table>").css({
            "position": "fixed",
            "top": "0px",
            "left": "0px",
            "width": "100%",
            "height": "100%",
            "background-color": "rgba(0,0,0,.5)",
            "z-index": "10000",
            "vertical-align": "middle",
            "text-align": "center",
            "color": "#fff",
            "font-size": "40px",
            "font-weight": "bold",
            "cursor": "wait"
        }).appendTo("body");
}

function blockV3(text) {
    $("<table id='overlay' style='margin-top:60px;'><tbody><tr><td>" +
        "<div class='cssload-square'>"+
        "<div><div><div><div><div></div></div></div></div></div>" +
        "<div><div><div><div><div></div></div></div></div></div>" +
        "</div >" +
        "<div class='cssload-square cssload-two'>" +
        "    <div><div><div><div><div></div></div></div></div></div>" +
        "    <div><div><div><div><div></div></div></div></div></div>" +
        "</div>" +
        "<br/><div id='txtOver'>" + text + "</div>" +
        "</td></tr></tbody></table>").css({
            "position": "fixed",
            "top": "0px",
            "left": "0px",
            "width": "100%",
            "height": "100%",
            "background-color": "rgba(0,0,0,.5)",
            "z-index": "10000",
            "vertical-align": "middle",
            "text-align": "center",
            "color": "#fff",
            "font-size": "40px",
            "font-weight": "bold",
            "cursor": "wait"
        }).appendTo("body");
}
function removeOverlay() {
    $("#overlay").remove();
}

function login(user, pass) {
    var result = "false";
    //loadHtml();
    $("#loginForm").hide();
    $("#sectionLoading").show("blind");
    //alert('hola');
    $.ajax({
        url: getVirtDir() + "/Controllers/login.ashx",
        type: "POST",
        data: { userN: user, userP: pass },
        async: false,
        success: function (data) {
            $("#sectionLoading").hide();
            var r = jQuery.parseJSON(data);
            if (r.result === "false") {
                $("#sectionLoading").hide();
                $("#message").html(r.messageError);
                $("#loginForm").hide();
                $("#sectionMessage").show("blind");
                result = "false";
                //return false;
            }
            if (r.result === "true")
                result = "true";

        },
        error: function () {
            result = "false";
        }
    });
    $("#sectionLoading").hide();
    //alert(result);
    return result;
}
function validateAuth(user, pass) {
    var result = "false";
    //loadHtml();
    $("#loginForm").hide();
    $("#sectionLoading").show("blind");
    //alert('hola');
    $.ajax({
        url: getVirtDir() + "/Controllers/validateUsrPrint.ashx",
        type: "POST",
        data: { userN: user, userP: pass },
        async: false,
        success: function (data) {
            $("#sectionLoading").hide();
            var r = jQuery.parseJSON(data);
            if (r.result === "false") {
                $("#sectionLoading").hide();
                $("#messageOK").hide();
                $("#message").html(r.messageError);
                $("#loginForm").hide();
                $("#sectionMessage").show("blind");
                result = "false";
                //return false;

            }
            if (r.result === "true") {
                result = "true";
                $("#overlay_printed").show();
                $("#printDJ").hide();
                //$('.modal-backdrop').remove();
                //$('#dlgGeneral').modal('hide');
                $("#message").hide();
                $("#messageOK").html(r.messageSuccess);
                $("#loginForm").hide();
                $("#showFormLog").hide();
                $("#sectionMessage").show("blind");
                //$('.modal-backdrop').css('display','none');
                //$('#dlgGeneral').modal('toggle');
            }

        },
        error: function () {
            result = "false";
        }
    });
    $("#sectionLoading").hide();
    //alert(result);
    return result;
}


/******************************************************************************************************/
/********************************************* log de eventos *****************************************/
/******************************************************************************************************/

function newPrint(idModel,idflx,idColor,idVolt,idUser, num_lbls,djNo,aName, lastfolio,initFolio) {
    //block();

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertSpecToPrint.ashx",
        data: { idModel: idModel, idflx: idflx, idColor: idColor, idVolt: idVolt, cantTot: num_lbls, idUser: idUser, djNo: djNo, aName: aName, lastfolio: lastfolio, initFolio: initFolio, dateDj: $("#txtDateDJ").val() },
        success: function (data) {
            r = jQuery.parseJSON(data);
            unblock();
            //alert(r.messagge);
            return false;
        },
        error: function () { }
    });
}

/******************************************************************************************************/
/********************************************* MODEL OPERATIONS ***************************************/
/******************************************************************************************************/

function insertModel() {
    //block();
    var isSpecial, isValid, isSC, has2L, isDFC, isLM, hasPanel, isLDM, numPCB;

    if ($("#isSpecial").is(':checked')) isSpecial = 1;
    else isSpecial = 0;
    if ($("#isActivate").is(':checked')) isValid = 1;
    else isValid = 0;
    if ($("#isTwoL").is(':checked')) has2L = 1;
    else has2L = 0;
    if ($("#isSerialCont").is(':checked')) isSC = 1;
    else isSC = 0;
    if ($("#isDFC").is(':checked')) isDFC = 1;
    else isDFC = 0;
    if ($("#isLM").is(':checked')) isLM = 1;
    else isLM = 0;
    if ($("#hasPanel").is(':checked')) hasPanel = 1;
    else hasPanel = 0;
    if ($("#isLDM").is(':checked')) isLDM = 1;
    else isLDM = 0;

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertModel.ashx",
        data: { descr: $("#se_description").val(), idProject: $("#se_id_family").val(), isSpecial: isSpecial, isValid: isValid, isSC: isSC, has2L: has2L, isDFC: isDFC, isML: isLM, hasPanel: hasPanel, isLDM: isLDM, numPCB: $("#se_num_pcb").val() },
        success: function (data) {
            r = jQuery.parseJSON(data);
            unblock();

            if (r.result === "true") {
                alertS("<div class='alert alert-success' role='alert'><center><i class='fa fa-check-circle'></i> <strong>Se agrego el modelo correctamente. <br>A continuación se configuraran las caracteristicas del modelo.</strong></center></div>");
                $("#configModel").hide(data);
                getAddSideModel(r.idModel);
            }
            if (r.result === "false")
                alertE("<div class='alert alert-danger' role='alert'><center><i class='fa fa-times-circle'></i> <strong>Ocurrio un error al agregar el modelo: " + r.Message + "</strong></center></div>");
            //alert(r.messagge);
            return false;
        },
        error: function () { }
    });
}

function insertSideModel() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertSideModel.ashx",
        data: { idModel: $("#se_id_model").val(), idSide: $("#se_id_side").val(), lblTag: $("#se_cust_part_num").val(), oracle: $("#se_oracle_par_num").val() },
        success: function (data) {
            r = jQuery.parseJSON(data);
            unblock();

            if (r.result === "true")
                alertS("<div class='alert alert-success' role='alert'><center><i class='fa fa-check-circle'></i> <strong>Se asigno el lado al modelo correctamente.</strong></center></div>");
            if (r.result === "false")
                alertE("<div class='alert alert-danger' role='alert'><center><i class='fa fa-times-circle'></i> <strong>Ocurrio un error al asignar el lado al modelo: " + r.Message + "</strong></center></div>");
            //alert(r.messagge);
            return false;
        },
        error: function () { }
    });
}

function insertFluxModel() {
    //block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/insertFluxModel.ashx",
        data: { idModel: $("#se_id_model").val(), idFlux: $("#se_id_flx").val()},
        success: function (data) {
            r = jQuery.parseJSON(data);
            unblock();

            if (r.result === "true")
                alertS("<div class='alert alert-success' role='alert'><center><i class='fa fa-check-circle'></i> <strong>Se asigno el Flux al modelo correctamente.</strong></center></div>");
            if (r.result === "false")
                alertE("<div class='alert alert-danger' role='alert'><center><i class='fa fa-times-circle'></i> <strong>Ocurrio un error al asignar el Flux al modelo: " + r.Message + "</strong></center></div>");
            //alert(r.messagge);
            return false;
        },
        error: function () { }
    });
}

function getAddModel() {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/AddModel/Create",
        success: function (data) {
            $("#configModel").html(data);
            $("#configModel").show("explode");
            //unblock();
            return false;
        },
        error: function () { }
    });
}  
function getAddSideModel(idModel) {
    //block();
    loadHtml();
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/siixsem_sidesModel/Create",
        success: function (data) {
            $("#configModel").html(data);
            $("#se_id_model").val(idModel);
            $('#se_id_model').attr('disabled', 'disabled');
            $("#configModel").show("explode");
            //unblock();
            return false;
        },
        error: function () { }
    });
}  

function log() {
    var r;
    if (!jQuery("#formLogin").validationEngine('validate')) {
        return;
    }
    r = login($("#userName").val(), $("#password").val());

    //alert(r);
    if (r === "false") {
        //alert(r);
        return false;
    }
    else {
        //alert(r);
        location.reload();
    }
    $("#sectionLoading").hide();
}
function acept() {
    //$("#sectionLoading").show("blind");
    //$("#message").html(r.messageError);
    $("#loginForm").show("blind");
    $("#sectionMessage").hide("blind");
}