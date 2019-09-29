MVCGrid.refreshSignalR = function () {

};

MVCGrid.InitializeSignalR = function () {

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/MVCGridSignalR")
        .build();
    connection.start().then(function () {
        console.log("MVCGrid SignalR connected");
        var gridName = $(".MVCGridContainer")[0].id.replace("MVCGridContainer_", "");
        connection.invoke("Message", gridName, "INIT", "").catch(err => console.error(err.toString()));
    });
    connection.on('MVCGrid', (response) => {
        var obj = JSON.parse(response);
        console.log(obj);
        //var gridname = obj.gridname;
        //var html = obj.html;

    });
    connection.on('Message', (response) => {
        var obj = JSON.parse(response);
        console.log(obj);
        var gridname = obj.Gridname;
        var html = obj.Html;
        var tableHolderHtmlId = 'MVCGridTableHolder_' + gridname;
        $('#' + tableHolderHtmlId).html(html);
    });
};

$(function () {
    
});

//MVCGrid.reloadGrid = function (mvcGridName) {
//    var tableHolderHtmlId = 'MVCGridTableHolder_' + mvcGridName;
//    var loadingHtmlId = 'MVCGrid_Loading_' + mvcGridName;
//    var errorHtmlId = 'MVCGrid_ErrorMessage_' + mvcGridName;

//    var gridDef = findGridDef(mvcGridName);;

//    var ajaxBaseUrl = handlerPath;

//    if (gridDef.renderingMode == 'controller') {
//        ajaxBaseUrl = controllerPath;
//    }

//    var fullAjaxUrl = ajaxBaseUrl + location.search;

//    $.each(gridDef.pageParameters, function (k, v) {
//        var thisPP = "_pp_" + gridDef.qsPrefix + k;
//        fullAjaxUrl = updateURLParameter(fullAjaxUrl, thisPP, v);
//    });

//    queryOptions[mvcGridName].Name = mvcGridName;

//    $.ajax({
//        type: "GET",
//        url: fullAjaxUrl,
//        data: queryOptions[mvcGridName],
//        cache: false,
//        beforeSend: function () {
//            if (gridDef.clientLoading != '') {
//                window[gridDef.clientLoading]();
//            }

//            $('#' + loadingHtmlId).css("visibility", "visible");
//        },
//        success: function (result) {
//            $('#' + tableHolderHtmlId).html(result);
//        },
//        error: function (request, status, error) {
//            var errorhtml = $('#' + errorHtmlId).html();

//            if (showErrorDetails) {
//                $('#' + tableHolderHtmlId).html(request.responseText);
//            } else {
//                $('#' + tableHolderHtmlId).html(errorhtml);
//            }
//        },
//        complete: function () {
//            if (gridDef.clientLoadingComplete != '') {
//                window[gridDef.clientLoadingComplete]();
//            }

//            $('#' + loadingHtmlId).css("visibility", "hidden");
//        }
//    });
//};