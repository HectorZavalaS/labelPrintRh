function getAllModelsToAdmin() {
    block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/cmbAllModels.ashx",
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selModel").html(r.html);
                $("#selModel").chosen();
                getBinsAdm($("#selModel").val());
                $("#selModel").chosen().change(function () {
                    getBinsAdm($("#selModel").val());
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
function getBinsAdm(idmodel) {

    getFluxesByIdModelAdm(idmodel);
    getColorsByIdModelAdm(idmodel);
    getVoltsByIdModelAdm(idmodel);
    getReviews();
    selectReview(idmodel)
    //getRevsByIdModel(idmodel);
    //getSidesByIdModel(idmodel);
    //getLabelSize(idmodel);
}

function selectReview(idmodel) {

    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getRevByIdModel.ashx",
        data: { idModel: idmodel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#selRev").val(r.idRev);
                $('#selRev').trigger("chosen:updated");
                //$("#selSide").chosen();
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
    $("#selCBR").val(r.color);
    $('#selCBR').trigger("chosen:updated");
}

function getFluxesByIdModelAdm(idModel) {
    block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getFluxesByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                //$("#selFlxL").html(r.html);
                $('#tblFlxModel tbody').html(r.tbl);
                //$('#selFlxL').trigger("chosen:updated");
                //$("#selFlxR").html(r.html);
                //$('#selFlxR').trigger("chosen:updated");

                //$("#selFlxL").chosen().change(function () {
                //    $("#selFlxR").val($("#selFlxL").val());
                //    $('#selFlxR').trigger("chosen:updated");
                //    getLblPreview($("#selModel").val(), $("#selSideL").val(), $("#selFlxL").val(), $("#selVBL").val(), $("#selRev").val(), "lblFrameL", "txtLblCodeL", $("#selCBL").val());
                //    getLblPreview($("#selModel").val(), $("#selSideR").val(), $("#selFlxR").val(), $("#selVBR").val(), $("#selRev").val(), "lblFrameR", "txtLblCodeR", $("#selCBR").val());
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
function getColorsByIdModelAdm(idModel) {
    block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getColorsByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#tblColorModel tbody").html(r.tbl);
            }
            else {
                //alert("EL modelo no tiene Bin de color configurado.");
                alertE("EL modelo no tiene Bin de color configurado.");
            }
            unblock();
            return false;
        },
        error: function () { }
    })
}
function getVoltsByIdModelAdm(idModel) {
    block();
    $.ajax({
        method: "POST",
        url: getVirtDir() + "/Controllers/getVoltsByIdModel.ashx",
        data: { idModel: idModel },
        async: false,
        //cache:false,
        success: function (data) {
            r = jQuery.parseJSON(data);
            if (r.result === "true") {
                $("#tblVoltModel tbody").html(r.tbl);
            }
            else
                alertE(r.messageError);
            unblock();
            return false;
        },
        error: function () { }
    })
}