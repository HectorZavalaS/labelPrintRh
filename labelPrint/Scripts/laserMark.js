function startMark() {
    setMonitor();
}
function getLaserModelsToPrint() {
    //block();
    blockV2("Cargando información de modelos...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbLaserDjs.ashx",
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {

                //alert(data);
                $("#selModel").html(r.html);
                //$("#selModel").chosen();
                createSelect("#selModel");
                setDJASSEMBLY();
                getStep($("#selModel").val());
                getSidesByIdModel($("#selModel").val());
                getBinsLed1($("#selModel").val());
                getBinsLed2($("#selModel").val());
                checkIfIsPrinted($("#txtDjNo").val(), $("#selModel").val());
                getBinesByBatch($("#txtDjGrp").val(), $("#selModel").val());
                if ($("#selModel").val() === "453" || $("#selModel").val() === "2" || $("#selModel").val() === "398") {
                    //alert("T7SA");
                    //enableBinCombosR();
                    enableBinCombosRLed2();
                }
                else {
                    disableBinCombosR();
                    disableBinCombosRLed2();
                    disableBinCombosLLed2();
                }
                //getCants($("input:checked").val());
                //$("input").on("click", function () {
                //    getCants($("input:checked").val());
                //});
                UpdateDjQty();
                getDjQty();
                $("#selModel").change(function () {
                    setDJASSEMBLY();
                    blockV2("Cargando " + $("#selModel option:selected").text() + "...");
                    g00etBinsLed1($("#selModel").val());
                    getBinsLed2($("#selModel").val());
                    getStep($("#selModel").val());
                    getSidesByIdModel($("#selModel").val());
                    UpdateDjQty();
                    getDjQty();
                    removeOverlay();
                    //alert($("#selModel").val());
                    checkIfIsPrinted($("#txtDjNo").val(), $("#selModel").val());
                    getBinesByBatch($("#txtDjGrp").val(), $("#selModel").val());
                });
            }
            else
                alertE('No se pudieron obtener los modelos.');
            removeOverlay();
            unblock();
            return false;
        },
        error: function () { }
    })
}

function getBinesByBatch(batch, idModel) {
    //block();
    blockV3("Obteniendo bines...");
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getBinesFromBatch.ashx",
        data: { batch: batch, idModel: idModel },
        //async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //$("#txtFlux").html(r.flux);
                //$("#txtColor").html(r.color);
                //$("#txtVolt").html(r.voltage);
                ////alert(r.idflux + ' - ' + r.idcolor + ' - ' + r.idvoltage);
                //$("#selFlxR").val(r.idflux);
                //$('#selFlxR').selectpicker('refresh');
                //$("#selFlxL").val(r.idflux);
                //$('#selFlxL').selectpicker('refresh');
                //$("#selVBL").val(r.idvoltage);
                //$('#selVBL').selectpicker('refresh');
                //$("#selVBR").val(r.idvoltage);
                //$('#selVBR').selectpicker('refresh');
                //$("#selCBL").val(r.idcolor);
                //$('#selCBL').selectpicker('refresh');
                //$("#selCBR").val(r.idcolor);
                //$('#selCBR').selectpicker('refresh');
                //disableBinCombosL();
                $("#LED1").html(r.html1);
                if (r.hasTwoLeds === "True") {
                    $("#LED2").html(r.html2);
                }
            }
            else {
                $("#txtFlux").html(r.flux);
                $("#txtColor").html(r.color);
                $("#txtVolt").html(r.voltage);
                $("#printDJ").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>DJ NO PICKEADA.</strong> <br><br>" +
                    "No se han pickeado los leds para esta DJ, no se puede imprimir o marcar." +
                    "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    "<span aria-hidden='true'>&times;</span>" +
                    "</button>" +
                    "</div > ");

                document.getElementById("overlay_printed").style.display = "none";
                $('#btnImpEt').attr('disabled', 'disabled');
                //alert(r.MessageError);
            }
            removeOverlay();
            return false;
        },
        error: function () { }
    });
}
function checkPartLSM(djGroup, idflx, idVol, idColor, qty, pcbDjQty, printMode) {
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/checkPartLSM.ashx",
        data: { djGroup: djGroup, idColor: idColor, idflx: idflx, idVol: idVol, qty: qty, pcbDjQty: pcbDjQty, idModel: $("#selModel").val() },
        async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                getViewPrintLblLMark($("#selModel").val(), $("#selSideL").val(), $("#selSideR").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selCBL").val(), $("#selRev").val(), qty, printMode);
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

function getViewPrintLblLMark(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode) {
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
                showDlgMark(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode);
            });
            return false;
        },
        error: function () { }
    });

}

function showDlgMark(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, printMode) {
    //blockV2("");

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/dlgMark/",
        //async: false,
        success: function (data) {
            //initDlgPrintLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls);
            $(".modal-dialog").css("width", "550px");
            $(".modal-dialog").css("max-width", "550px");
            $("#loginDlg").html(data);
            $("#dlgTitle").html("MARCADO LASER");
            $("#dlgFooter").html("<select class='form-control form-control-sm' id='cmbPrinters' style='text-align:left;width:240px; display:inline; margin-left:15px;'></select>" +
                "<button type='button' class='btn btn-sm btn-danger' data-dismiss='modal' id='btnClose'> " +
                "<span class='glyphicon glyphicon-remove-circle' style='margin-right:7px;'></span>Cancelar" +
                "</button>" +
                "<button class='btn btn-sm btn-info'  id='btnTestPrint'>" +
                "<span class='glyphicon glyphicon-print' style='margin-right:7px;'></span>Generar prueba" +
                "</button>" +
                "<button class='btn btn-sm btn-success'  id='btnPrint'>" +
                "<span class='glyphicon glyphicon-print' style='margin-right:7px;'></span>Generar todos" +
                "</button>");
            getLaserMarks();
            $("#progressMark").hide();
            $("#dlgFooter").show();
            $("#numPcbs").html($("#pcbDjQty").val());
            $("#totPcbs").text($("#pcbDjQty").val());
            $("#txtModel").html($("#txtModelDJ").val());
            $("#txtFlxL").html($("#selFlxL option:selected").text());
            $("#txtCBL").html($("#selCBL option:selected").text());
            $("#txtVBL").html($("#selVBL option:selected").text());
            $("#btnPrint").click(function () {
                $('#btnPrint').prop('disabled', true);
                if ($("#txtTPrint").val() === "1") {
                    printLaserMark(idModel, idSideL, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO");
                    if (idSideL === "1") {
                        alert("Se imprimiran las etiquetas del lado RH");
                        printLaserMark(idModel, 2, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO");
                    }
                }
                else
                    printLbl(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), printMode);

                startMark();

            });
            $("#btnTestPrint").click(function () {

                if ($("#txtTPrint").val() === "1") {
                    printLaserMark(idModel, idSideL, idflx, idVol, idColor, idRev, 1, $("#cmbPrinters").val(), "TEST");
                    if (idSideL === "1") {
                        alert("Se imprimiran las etiquetas del lado RH");
                        printLaserMark(idModel, 2, idflx, idVol, idColor, idRev, 1, $("#cmbPrinters").val(), "TEST");
                    }
                }
                else
                    printLblTest(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, $("#cmbPrinters").val(), "1");

            });
            $("#btnClose").click(function () {
                location.reload(true);
            });
            return false;
        },
        error: function () { }
    });
}

function printLaserMark(idModel, idSide, idflx, idVol, idColor, idRev, num_lbls, idPrinter, typePrint, idflx1, idVol1, idColor1) {
    Mark();
    $("#progressMark").show();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/printLaserSerial.ashx",
        data: {
            num_lbls: num_lbls, idModel: idModel, idSide: idSide, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev,
            idPrinter: idPrinter, idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(), dateDj: $("#txtDateDJ").val(),
            isRem: $("#isRem").val(), djGrp: $("#txtDjGrp").val(), typePrint: typePrint,//"FOLIO",
            idflx1: idflx1, idVol1: idVol1, idColor1: idColor1
        },
       // async: false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            $("#progressMark").hide();
            if (r.result === "true") {
                var pcbQty;
                if (typePrint === "FOLIO") {
                    if ($("#partPrint").is(':checked')) {
                        pcbQty = $("#pcbDjQtyPar").val();
                    } else {
                        pcbQty = $("#pcbDjQty").val();
                    }
                    insertPartialSM(idModel, pcbQty, r.ID_RESULT);
                }
            }
            if (r.result === "false") {
                alert(r.messageError);
            }

                
            unblock();
            return false;
        },
        error: function () { }
    });
}

//function getBinsLed1(idmodel) {
//    getFluxesLed1ByIdModel(idmodel);
//    getColorsLed1ByIdModel(idmodel);
//    getVoltsByLed1IdModel(idmodel);
//    getRevsByIdModel(idmodel);
//    //getLabelSize(idmodel);
//    initCombos();
//}

//function getBinsLed2(idmodel) {
//    getFluxesLed2ByIdModel(idmodel);
//    getColorsLed2ByIdModel(idmodel);
//    getVoltsLed2ByIdModel(idmodel);
//    getRevsByIdModel(idmodel);
//    getSidesByIdModelLed2(idmodel);
//    getLabelSize(idmodel);
//    initCombosLed2();
//}

///************** metodos para primer led ********************/

//function getFluxesLed1ByIdModel(idModel) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getFluxesByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selFlxL").html(r.html);
//                updateSelect('#selFlxL');
//                $("#selFlxR").html(r.html);
//                updateSelect('#selFlxR');
//                $("#selFlxL").change(function (e) {
//                    e.preventDefault();
//                    e.stopImmediatePropagation();
//                    $("#selFlxR").val($("#selFlxL").val());
//                    updateSelect('#selFlxR');
//                    blockV3("Loading configuration...");
//                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                    removeOverlay();
//                });
//            }
//            else
//                alertE(r.messageError);
//            return false;
//        },
//        error: function () { }
//    })
//}

//function getVoltsByLed1IdModel(idModel) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getVoltsByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selVBL").html(r.html);
//                updateSelect('#selVBL');
//                $("#selVBR").html(r.html);
//                updateSelect('#selVBR');
//                //$("#selSide").chosen();
//                $("#selVBL").change(function (e) {
//                    e.preventDefault();
//                    e.stopImmediatePropagation();
//                    $("#selVBR").val($("#selVBL").val());
//                    updateSelect('#selVBR');
//                    blockV3("Loading configuration...");
//                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                    removeOverlay();
//                });
//            }
//            else
//                alertE(r.messageError);
//            return false;
//        },
//        error: function () { }
//    })
//}

//function getColorsLed1ByIdModel(idModel) {

//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getColorsByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selCBL").html(r.html);
//                updateSelect('#selCBL');
//                $("#selCBR").html(r.html);
//                updateSelect('#selCBR');
//                //$("#selSide").chosen();
//                $("#selCBL").change(function (e) {
//                    e.preventDefault();
//                    e.stopImmediatePropagation();
//                    $("#selCBR").val($("#selCBL").val());
//                    updateSelect('#selCBR');
//                    blockV3("Loading configuration...");
//                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                    removeOverlay();
//                });
//            }
//            else {
//                //alert("EL modelo no tiene Bin de color configurado.");
//                alertE("EL modelo no tiene Bin de color configurado.");
//                $("#selCBR").html("<option></option>");
//                updateSelect('#selCBR');
//                $("#selCBL").html("<option></option>");
//                updateSelect('#selCBL');
//            }
//            return false;
//        },
//        error: function () { }
//    })
//}

///************** metodos para segundo led********************/
//function getFluxesLed2ByIdModel(idModel) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getFluxesLed2ByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selFlxL1").html(r.html);
//                updateSelect('#selFlxL1');
//                $("#selFlxR1").html(r.html);
//                updateSelect('#selFlxR1');

//                $("#selFlxL1").change(function (e) {
//                    e.preventDefault();
//                    e.stopImmediatePropagation();
//                    $("#selFlxR1").val($("#selFlxL1").val());
//                    updateSelect('#selFlxR1');
//                    blockV3("Loading configuration...");
//                    getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                    removeOverlay();
//                });
//                if (idModel == "453" || $("#selModel").val() === "2" || $("#selModel").val() === "398") {

//                    $("#selFlxR").change(function (e) {
//                        e.preventDefault();
//                        e.stopImmediatePropagation();
//                        //$("#selFlxR1").val($("#selFlxL1").val());
//                        updateSelect('#selFlxR1');
//                        blockV3("Loading configuration...");
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                        if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                            getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                        removeOverlay();
//                    });
//                    $("#selFlxR1").change(function (e) {
//                        e.preventDefault();
//                        e.stopImmediatePropagation();
//                        //$("#selFlxR1").val($("#selFlxL1").val());
//                        updateSelect('#selFlxR1');
//                        blockV3("Loading configuration...");
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                        if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                            getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                        removeOverlay();
//                    });
//                }
//            }
//            else
//                alertE(r.messageError);
//            unblock();
//            return false;
//        },
//        error: function () { }
//    })
//}

//function getVoltsLed2ByIdModel(idModel) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getVoltsLed2ByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selVBL1").html(r.html);
//                updateSelect('#selVBL1');
//                $("#selVBR1").html(r.html);
//                updateSelect('#selVBR1');
//                //$("#selSide").chosen();
//                $("#selVBL1").change(function (e) {
//                    e.preventDefault();
//                    e.stopImmediatePropagation();
//                    $("#selVBR1").val($("#selVBL1").val());
//                    updateSelect('#selVBR1');
//                    blockV3("Loading configuration...");
//                    getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                    removeOverlay();

//                });
//            }
//            else
//                alertE(r.messageError);
//            return false;
//        },
//        error: function () { }
//    })
//}


//function getColorsLed2ByIdModel(idModel) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getColorsLed2ByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selCBL1").html(r.html);
//                updateSelect('#selCBL1');
//                $("#selCBR1").html(r.html);
//                updateSelect('#selCBR1');
//                //$("#selSide").chosen();
//                $("#selCBL1").change(function (e) {
//                    e.preventDefault();
//                    e.stopImmediatePropagation();
//                    $("#selCBR1").val($("#selCBL1").val());
//                    updateSelect('#selCBR1');
//                    blockV3("Loading configuration...");
//                    getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                    removeOverlay();

//                });
//            }
//            else {
//                //alert("EL modelo no tiene Bin de color configurado.");
//                alertE("EL modelo no tiene Bin de color configurado.");
//                $("#selCBR1").html("<option></option>");
//                updateSelect('#selCBR1');
//                $("#selCBL1").html("<option></option>");
//                updateSelect('#selCBL1');
//            }
//            return false;
//        },
//        error: function () { }
//    })
//}

//function getLblPreviewLed2(idModel, idSide, idFlx, idVol, idRev, divName, txtSide, idColor, idFlx1, idVol1, idColor1) {
//    genLblTemplateLed2(idModel, idSide, idFlx, idVol, idColor, idRev, divName, txtSide, $("#txtDateDJ").val(), idFlx1, idVol1, idColor1);
//    getPreviewLblLed2($("#" + txtSide).val(), divName, $("#" + txtSide + "1").val(), $("#" + txtSide + "2").val(), idModel, idFlx, idVol, idColor, $("#" + txtSide + "1U611").val(), $("#" + txtSide + "2U611").val());
//}

//function genLblTemplateLed2(idModel, idSide, idflx, idVol, idColor, idRev, divName, nSides, dateDj, idflx1, idVol1, idColor1) {

//    if (idSide != null) {
//        $.ajax({
//            method: "POST",
//            url: getVirtDir() + "/Controllers/genLblTemplateTwoLed.ashx",
//            data: {
//                idModel: idModel, idSide: idSide,
//                idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev, nSides: nSides, dateDj: dateDj,
//                idflx1: idflx1, idVol1: idVol1, idColor1: idColor1
//            },
//            async: false,
//            //cache:false,
//            success: function (data) {
//                r = jQuery.parseJSON(data);
//                if (r.result === "true") {
//                    //alert(nSides);
//                    $("#" + nSides).val(r.lblTemp);
//                    $("#" + nSides + "1").val(r.line1);
//                    $("#" + nSides + "2").val(r.line2);
//                    $("#" + nSides + "1U611").val(r.line1U611);
//                    $("#" + nSides + "2U611").val(r.line2U611);
//                }
//                else
//                    alert(r.MessageError);
//                return false;
//            },
//            error: function () { }
//        });
//    }
//}
//function getPreviewLblLed2(code, nDiv, line1, line2, idModel, idflx, idVol, idColor, line1U611, line2U611) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/lblWEBServiceLed2.ashx",
//        data: {
//            code: code, line1: line1, line2: line2, idModel: idModel,
//            idflx: idflx, idColor: idColor, idVol: idVol,
//            line1U611: line1U611, line2U611: line2U611
//        },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true")
//                $("#" + nDiv).html("<center><img src='" + getVirtDir() + "/Labels/" + r.dirprev + "' style='width: 50%;'></center>");
//            else
//                alert(r.MessageError);
//            return false;
//        },
//        error: function () { }
//    })
//}
//function getSidesByIdModelLed2(idModel) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getSidesByIdModel.ashx",
//        data: { idModel: idModel },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                $("#selSideL").html(r.htmlL);
//                updateSelect('#selSideL');
//                $("#selSideR").html(r.htmlR);
//                updateSelect('#selSideR');
//                if (r.nSides === "1") {
//                    $("#rSide").hide("clip");
//                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    $("#txtLblCodeR").val("");
//                    $("#lblFrameR").hide("clip");
//                }
//                else {
//                    $("#rSide").show("clip");
//                    $("#lblFrameR").show("clip");
//                    if ($("#selSideL").val() != '' && $("#selFlxL").val() != '' && $("#selVBL").val() != '' && $("#selRev").val() != '' && $("#selCBL") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val(), $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val());
//                    if ($("#selSideR").val() != '' && $("#selFlxR").val() != '' && $("#selVBR").val() != '' && $("#selRev").val() != '' && $("#selCBR") != '')
//                        getLblPreviewLed2($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val(), $("#selFlxR1").val(), $("#selVBR1").val(), $("#selCBR1").val());
//                }
//            }
//            else
//                alert(r.messageError);
//            return false;
//        },
//        error: function () { }
//    })
//}

//function initDlgPrintLblLed2(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idflx1, idVol1, idColor1) {
//    //genLblTemplate(79, 1, 2, 2, 4);

//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/getPreviewLabelsLed2.ashx",
//        data: {
//            num_lbls: num_lbls, idModel: idModel, idSideL: idSideL, idSideR: idSideR,
//            idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev, dateDj: $("#txtDateDJ").val(), isRem: $("#isRem").val(),
//            idflx1: idflx1, idVol1: idVol1, idColor1: idColor1
//        },
//        success: function (data) {
//            var r = jQuery.parseJSON(data);

//            if (r.result === 'true') {
//                $('#topPrev').html(r.htmlTop);
//                $('#botPrev').html(r.htmlBot);
//                $('#tblEtiquetas tbody').html(r.tblTop);
//                $('#tblEtiquetas').DataTable({
//                    "paging": false,
//                    //"order": [[0, "asc"]],
//                    "recordsFiltered": 10,
//                    "lengthChange": false,
//                    "searching": false,
//                    "pageLength": 5,
//                    "ordering": false,
//                    "info": false,
//                    "autoWidth": false,
//                    'language': { 'url': getVirtDir() + '/Scripts/Spanish.json' }
//                });
//            }
//            return false;
//        },
//        error: function () { }
//    });
//}
//function getViewPrintLblLed2(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idflx1, idVol1, idColor1, printMode) {
//    block();

//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/dlgPrintlbl/",
//        async: false,
//        success: function (data) {
//            initDlgPrintLblLed2(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idflx1, idVol1, idColor1);
//            $(".modal-dialog").css("width", "550px");
//            $(".modal-dialog").css("max-width", "550px");

//            $("#loginDlg").html(data);
//            $("#dlgTitle").html("ETIQUETAS GENERADAS SEGUN ESPECIFICACIONES");
//            $("#dlgFooter").html("<select class='form-control form-control-sm' id='cmbPrinters' style='text-align:left;width:240px; display:inline; margin-left:15px;'></select>" +
//                "<div id='labelPos' style='width:300px;text-align:center;'>" +
//                "<div id='up'><button type='button' class='btn btn-sm btn-info' id='btnUp'><i class='metismenu-icon pe-7s-up-arrow' style='font-size: 18px;'></i></button></div>" +
//                "<div id='lr'><button type='button' class='btn btn-sm btn-info' id='btnLeft' style='margin-right:30px;'><i class='metismenu-icon pe-7s-left-arrow' style='font-size: 18px;'></i></button>" +
//                "<button type='button' class='btn btn-sm btn-info' id='btnRight'><i class='metismenu-icon pe-7s-right-arrow' style='font-size: 18px;'></i></div>" +
//                "<div id='down'><button type='button' class='btn btn-sm btn-info' id='btnDown'><i class='metismenu-icon pe-7s-bottom-arrow' style='font-size: 18px;'></i></button></div>" +
//                "</div> " +
//                "<button type='button' class='btn btn-sm btn-danger' data-dismiss='modal' id='btnClose'> " +
//                "<span class='glyphicon glyphicon-remove-circle' style='margin-right:7px;'></span>Cancelar" +
//                "</button>" +
//                "<button class='btn btn-sm btn-info'  id='btnTestPrint'>" +
//                "<span class='glyphicon glyphicon-print' style='margin-right:7px;'></span>Imprimir prueba" +
//                "</button>" +
//                "<button class='btn btn-sm btn-success'  id='btnPrint'>" +
//                "<span class='glyphicon glyphicon-print' style='margin-right:7px;'></span>Imprimir" +
//                "</button>");
//            getPrinters();
//            $("#dlgFooter").show();
//            unblock();
//            $('#btnUp').click(function () {
//                /*if ($("#txtLvlUser").val() === "1")*/
//                updateLblPositionVer(-5);
//                //else
//                //    alert("No tienes privilegios de Administrador para realizar esta operación.");
//            });
//            $('#btnLeft').click(function () {
//                /*if ($("#txtLvlUser").val() === "1")*/
//                updateLblPositionHor(-5);
//                //else
//                //    alert("No tienes privilegios de Administrador para realizar esta operación.");

//            });
//            $('#btnRight').click(function () {
//                /*if ($("#txtLvlUser").val() === "1")*/
//                updateLblPositionHor(5);
//                //    else
//                //        alert("No tienes privilegios de Administrador para realizar esta operación.");
//            });
//            $('#btnDown').click(function () {
//                /*if ($("#txtLvlUser").val() === "1")*/
//                updateLblPositionVer(5);
//                //else
//                //    alert("No tienes privilegios de Administrador para realizar esta operación.");
//            });
//            $("#btnPrint").click(function () {
//                //alert('Las etiquetas no han sido validadas por AXIAL, no se imprimiran.');
//                $('#btnPrint').prop('disabled', true);
//                if ($("#txtTPrint").val() === "1") {
//                    printLabelLed2V2(idModel, idSideL, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO", idflx1, idVol1, idColor1);
//                    if (idSideL === "1") {
//                        alert("Se imprimiran las etiquetas del lado RH");
//                        printLabelLed2V2(idModel, 2, $("#selFlxR").val(), idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO", $("#selFlxR1").val(), idVol1, idColor1);
//                    }
//                }
//                else
//                    printLabelLed2V2(idModel, idSideL, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), "FOLIO", idflx1, idVol1, idColor1);
//                //printLblLed2(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, $("#cmbPrinters").val(), idflx1, idVol1, idColor1);

//            });
//            $("#btnTestPrint").click(function () {
//                //alert('Se imprimiran las etiquetas');
//                if ($("#txtTPrint").val() === "1") {
//                    printLabelLed2V2(idModel, idSideL, idflx, idVol, idColor, idRev, 10, $("#cmbPrinters").val(), "TEST", idflx1, idVol1, idColor1);
//                    if (idSideL === "1") {
//                        alert("Se imprimiran las etiquetas del lado RH");
//                        printLabelLed2V2(idModel, 2, $("#selFlxR").val(), idVol, idColor, idRev, 10, $("#cmbPrinters").val(), "TEST", $("#selFlxR1").val(), idVol1, idColor1);
//                    }
//                }
//                else
//                    printLabelLed2V2(idModel, idSideL, idflx, idVol, idColor, idRev, 10, $("#cmbPrinters").val(), "TEST", idflx1, idVol1, idColor1);
//            });
//            $("#btnClose").click(function () {
//                //alert('Se imprimiran las etiquetas');
//                //$("#selModel").html("");
//                //$("#selModel").trigger("destroy");
//                //getModelsToPrint();
//                //$("#selModel").trigger("chosen:updated");
//                location.reload(true);
//            });
//            return false;
//        },
//        error: function () { }
//    });
//}
//function printLblLed2(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, num_lbls, idPrinter, idflx1, idVol1, idColor1) {
//    alert($("#txtDjGrp").val());
//    block();
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/printLabelLed2.ashx",
//        data: {
//            num_lbls: num_lbls, idModel: idModel, idSideL: idSideL, idSideR: idSideR,
//            idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev, idPrinter: idPrinter,
//            idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(),
//            dateDj: $("#txtDateDJ").val(), isRem: $("#isRem").val(),
//            idflx1: idflx1, idVol1: idVol1, idColor1: idColor1, djGrp: $("#txtDjGrp").val(), typePrint: "FOLIO"
//        },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                //alert(idModel + " - " + idflx + " - " + idColor + " - " + idVol + " - " + $("#idUser").val() + " - " + num_lbls);
//                //newPrint(idModel, idflx, idColor, idVol, $("#idUser").val(), num_lbls, $("#txtDjNo").val(), $("#txtAName").val(), r.lastfolio,r.initfolio);
//                var pcbQty;

//                if ($("#partPrint").is(':checked')) {
//                    pcbQty = $("#pcbDjQtyPar").val();
//                } else {
//                    pcbQty = $("#pcbDjQty").val();
//                }

//                insertPartialSM(idModel, pcbQty, r.ID_RESULT);

//                //generateExcelSimons($("#txtDjNo").val());
//            }
//            //$("#lblFrame").html("<center><img src='" + getVirtDir() + "/Labels/" + r.dirprev + "' style='width: 70%;'></center>");
//            else
//                alert(r.messageError);
//            unblock();
//            return false;
//        },
//        error: function () { }
//    })
//}

//function printLblLed2Test(idModel, idSideL, idSideR, idflx, idVol, idColor, idRev, idPrinter, idflx1, idVol1, idColor1) {
//    block();
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/printLabelLed2.ashx",
//        data: {
//            num_lbls: 10, idModel: idModel, idSideL: idSideL, idSideR: idSideR,
//            idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev, idPrinter: idPrinter,
//            dateDj: $("#txtDateDJ").val(), isRem: 0,
//            idflx1: idflx1, idVol1: idVol1, idColor1: idColor1, djGrp: $("#txtDjGrp").val(), typePrint: "TEST"
//        },
//        //async: false,
//        //cache:false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                //alert(idModel + " - " + idflx + " - " + idColor + " - " + idVol + " - " + $("#idUser").val() + " - " + num_lbls);
//                //newPrint(idModel, idflx, idColor, idVol, $("#idUser").val(), num_lbls, $("#txtDjNo").val(), $("#txtAName").val());
//            }
//            //$("#lblFrame").html("<center><img src='" + getVirtDir() + "/Labels/" + r.dirprev + "' style='width: 70%;'></center>");
//            else
//                alertE(r.messageError);
//            unblock();
//            return false;
//        },
//        error: function () { }
//    })
//}

//function insertPartialSM(idModel, pcbQty, idSpec) {

//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/insertPartialSM.ashx",
//        data: {
//            idModel: idModel, idSpec: idSpec, pcbQty: pcbQty, djNo: $("#txtDjNo").val()
//        },
//        async: false,
//        //cache:false,
//        success: function (data) {
//            //r = jQuery.parseJSON(data);
//            //if (r.result === "true") {

//            //else
//            //    alert(r.messageError);

//            return false;
//        },
//        error: function () { }
//    });
//}

//function checkPartSM(djGroup, idflx, idVol, idColor, idflx1, idVol1, idColor1, qty, pcbDjQty, printMode) {
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/checkPartSM.ashx",
//        data: {
//            djGroup: djGroup, idColor: idColor, idflx: idflx, idVol: idVol, qty: qty, pcbDjQty: pcbDjQty, idModel: $("#selModel").val(),
//            idColor1: idColor1, idflx1: idflx1, idVol1: idVol1
//        },
//        async: false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                //getViewPrintLbl($("#selModel").val(), $("#selSideL").val(), $("#selSideR").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selCBL").val(), $("#selRev").val(), qty);
//                getViewPrintLblLed2($("#selModel").val(), $("#selSideL").val(), $("#selSideR").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selCBL").val(), $("#selRev").val(), qty, $("#selFlxL1").val(), $("#selVBL1").val(), $("#selCBL1").val(), printMode);
//                $('#dlgGeneral').on('show.bs.modal', function () {
//                    var myModal = $(this);
//                    //myModal.modal('hide');
//                    clearTimeout(myModal.data('hideInterval'));
//                });
//            }
//            if (r.result === "false") {
//                //alertE("<div class='alert alert-danger' role='alert'><center><i class='fa fa-times-circle'></i> <strong>" + r.Message + "</strong></center></div>");

//                $("#printDJ").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>No hay suficientes PCB's´para imprimir.</strong> <br><br>" +
//                    "Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad."
//                    + "<br><br><strong> Correo de contacto: it@siix-sem.com.mx</strong>" +
//                    "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
//                    "<span aria-hidden='true'>&times;</span>" +
//                    "</button>" +
//                    "</div > ");

//                $("#loginDlg").html("<div class='alert alert-danger alert-dismissible fade show' role='alert'><i class='fa fa-warning'></i> <strong>No hay suficientes PCB's´para imprimir.</strong> <br><br>" +
//                    "Para habilitar nuevamente la impresión debes solicitar autorización por correo para reimprimir la DJ. Incluir en el correo el motivo de la reimpresión, No. de DJ y Cantidad."
//                    + "<br><br><strong> Correo de contacto: it@siix-sem.com.mx</strong>" +
//                    "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
//                    "<span aria-hidden='true'>&times;</span>" +
//                    "</button>" +
//                    "</div > ");

//                //document.getElementById("overlay_printed").style.display = "none";
//                //$('#dlgGeneral').on('show.bs.modal', function () {
//                //    var myModal = $(this);
//                //    //myModal.modal('hide');
//                //    clearTimeout(myModal.data('hideInterval'));
//                //    myModal.data('hideInterval', setTimeout(function () {
//                //        myModal.modal('hide');
//                //    }, 3));
//                //});


//                return;
//            }
//        },
//        error: function () { }
//    });
//}
//function printLblSideLed2(idModel, idSide, idflx, idVol, idColor, idRev, num_lbls, idPrinter, typePrint, idflx1, idVol1, idColor1) {
//    print();
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/printLabelSideLed2.ashx",
//        data: {
//            num_lbls: num_lbls, idModel: idModel, idSide: idSide, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev,
//            idPrinter: idPrinter, idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(), dateDj: $("#txtDateDJ").val(),
//            isRem: $("#isRem").val(), djGrp: $("#txtDjGrp").val(), typePrint: typePrint,//"FOLIO",
//            idflx1: idflx1, idVol1: idVol1, idColor1: idColor1
//        },
//        async: false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                var pcbQty;

//                if ($("#partPrint").is(':checked')) {
//                    pcbQty = $("#pcbDjQtyPar").val();
//                } else {
//                    pcbQty = $("#pcbDjQty").val();
//                }
//                insertPartialSM(idModel, pcbQty, r.ID_RESULT);
//            }
//            else
//                alert(r.messageError);
//            unblock();
//            return false;
//        },
//        error: function () { }
//    });
//}

//function printLabelLed2V2(idModel, idSide, idflx, idVol, idColor, idRev, num_lbls, idPrinter, typePrint, idflx1, idVol1, idColor1) {
//    print();
//    $.ajax({
//        method: "POST",
//        url: getVirtDir() + "/Controllers/printLabelLed2V2.ashx",
//        data: {
//            num_lbls: num_lbls, idModel: idModel, idSide: idSide, idflx: idflx, idVol: idVol, idColor: idColor, idRev: idRev,
//            idPrinter: idPrinter, idUser: $("#idUser").val(), noDJ: $("#txtDjNo").val(), aName: $("#txtAName").val(), dateDj: $("#txtDateDJ").val(),
//            isRem: $("#isRem").val(), djGrp: $("#txtDjGrp").val(), typePrint: typePrint,//"FOLIO",
//            idflx1: idflx1, idVol1: idVol1, idColor1: idColor1
//        },
//        async: false,
//        success: function (data) {
//            r = jQuery.parseJSON(data);
//            if (r.result === "true") {
//                var pcbQty;
//                if (typePrint === "FOLIO") {
//                    if ($("#partPrint").is(':checked')) {
//                        pcbQty = $("#pcbDjQtyPar").val();
//                    } else {
//                        pcbQty = $("#pcbDjQty").val();
//                    }
//                    insertPartialSM(idModel, pcbQty, r.ID_RESULT);
//                }
//            }
//            else
//                alert(r.messageError);
//            unblock();
//            return false;
//        },
//        error: function () { }
//    });
//}