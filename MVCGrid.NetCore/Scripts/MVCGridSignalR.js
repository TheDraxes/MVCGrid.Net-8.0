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
        var type = obj.Type;
        var gridname = obj.Gridname;
        var html = obj.Html;
        var summaryhtml = obj.SummaryHtml;
        if (type == "Table") {
            var tableHtmlId = 'MVCGridTableHolder_' + gridname;
            $('#' + tableHolderHtmlId).html(html);
        }
        if (type == "Row") {
            $(".noresults").remove();
            var tableHtmlId = 'MVCGridTable_' + gridname;
            $('#' + tableHtmlId + ' tbody').append(html);

            var tableSummaryHtmlId = 'MVCGridTable_' + gridname + '_Summary';
            $('#' + tableSummaryHtmlId).html(summaryhtml);
        }
    });
};

$(function () {
    
});