
var MVCGrid = new function () {

    var handlerPath = '%%HANDLERPATH%%';
    var controllerPath = '%%CONTROLLERPATH%%';
    var showErrorDetails = %%ERRORDETAILS%%;
    var currentGrids = [];
    var queryOptions = {};

    // public
    this.init = function () {
        $('.MVCGridContainer').each(function () {

            var mvcGridName = $("#" + this.id).find("input[name='MVCGridName']").val();
            var jsonData = $('#' + 'MVCGrid_' + mvcGridName + '_JsonData').html();

            currentGrids.push(
                $.parseJSON(jsonData)
            );
            queryOptions[mvcGridName] = {};
        });

        for (var i = 0; i < currentGrids.length; i++) {
            var obj = currentGrids[i];

            if (!obj.preloaded) {
                MVCGrid.reloadGrid(obj.name);
            }
        }

        bindToolbarEvents();
    };

    var applyBoundFilters = function (mvcGridName) {

        var o = {};

        $("[data-mvcgrid-type='filter']").each(function () {

            var gridName = getGridName($(this));
            if (gridName == mvcGridName) {

                var option = $(this).attr('data-mvcgrid-option');
                var val = $(this).val();

                o[option] = val;
            }
        });

        MVCGrid.setFilters(mvcGridName, o);
    };

    var loadBoundFilters = function () {
        $("[data-mvcgrid-type='filter']").each(function () {
            var gridName = getGridName($(this));
            var option = $(this).attr('data-mvcgrid-option');

            var val = MVCGrid.getFilters(gridName)[option];
            $(this).val(val);
        });
    };

    var applyAdditionalQueryOptions = function (mvcGridName) {

        var o = {};
        $("[data-mvcgrid-type='additionalQueryOption']").each(function () {
            var gridName = getGridName($(this));

            if (gridName == mvcGridName) {
                var option = $(this).attr('data-mvcgrid-option');
                var val = $(this).val();

                o[option] = val;
            }
        });

        MVCGrid.setAdditionalQueryOptions(mvcGridName, o);
    };

    var loadAdditionalQueryOptions = function () {
        $("[data-mvcgrid-type='additionalQueryOption']").each(function () {
            var gridName = getGridName($(this));
            var option = $(this).attr('data-mvcgrid-option');

            var val = MVCGrid.getAdditionalQueryOptions(gridName)[option];
            $(this).val(val);
        });
    };

    var getGridName = function (elem) {
        var gridName = currentGrids[0].name;
        var nameAttr = elem.attr('data-mvcgrid-name');
        if (typeof nameAttr !== typeof undefined && nameAttr !== false) {
            gridName = nameAttr;
        }
        return gridName;
    };

    var bindToolbarEvents = function () {

        loadBoundFilters();
        loadAdditionalQueryOptions();

        $("[data-mvcgrid-apply-filter]").each(function () {

            var eventName = $(this).attr("data-mvcgrid-apply-filter");

            $(this).on(eventName, function () {
                var gridName = getGridName($(this));

                applyBoundFilters(gridName);
            });
        });

        $("[data-mvcgrid-apply-additional]").each(function () {

            var eventName = $(this).attr("data-mvcgrid-apply-additional");

            $(this).on(eventName, function () {
                var gridName = getGridName($(this));

                applyAdditionalQueryOptions(gridName);
            });
        });

        $("[data-mvcgrid-type='export']").each(function () {

            $(this).click(function () {
                var gridName = getGridName($(this));

                location.href = MVCGrid.getExportUrl(gridName);
            });

        });

        $("[data-mvcgrid-type='pageSize']").each(function () {

            var gridName = getGridName($(this));
            $(this).val(MVCGrid.getPageSize(gridName));

            $(this).change(function () {
                var gridName = getGridName($(this));
                MVCGrid.setPageSize(gridName, $(this).val());
            });


        });


        $("[data-mvcgrid-type='columnVisibilityList']").each(function () {

            var listElement = $(this);
            var gridName = getGridName($(this));

            var colVis = MVCGrid.getColumnVisibility(gridName);
            $.each(colVis, function (colName, col) {

                if (!col.visible && !col.allow) {
                    return true;
                }
                var html = '<li><a><label><input type="checkbox" name="' + gridName + 'cols" value="' + colName + '"';
                if (col.visible) {
                    html += ' checked';
                }
                if (!col.allow) {
                    html += ' disabled';
                }
                html += '> ' + col.headerText + '</label></a></div></li>';
                listElement.append(html);
            });

            $("input:checkbox[name='" + gridName + "cols']").change(function () {
                var jsonData = {};
                var gridName = getGridName($(this).closest('ul'));

                $("input:checkbox[name='" + gridName + "cols']:checked").each(function () {
                    var columnName = $(this).val();
                    jsonData[columnName] = true;
                });
                MVCGrid.setColumnVisibility(gridName, jsonData);
            });
        });

    };

    // private
    var getClientData = function (mvcGridName) {
        var jsonData = $('#' + 'MVCGrid_' + mvcGridName + '_ContextJsonData').html();

        return $.parseJSON(jsonData);
    };

    // private
    var findGridDef = function (mvcGridName) {
        var gridDef;
        for (var i = 0; i < currentGrids.length; i++) {
            var obj = currentGrids[i];

            if (obj.name == mvcGridName) {
                gridDef = obj;
                break;
            }
        }

        if (gridDef == undefined) {
            window.console && console.log('Grid not found: ' + mvcGridName);
        }

        return gridDef;
    };

    // public
    this.getColumnVisibility = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.columnVisibility;
    };

    // public
    this.setColumnVisibility = function (mvcGridName, obj) {
        var colString = '';
        $.each(obj, function (k, v) {
            if (v) {
                if (colString != '') colString += ',';
                colString += k;
            }
        });

        queryOptions[mvcGridName].cols = colString;
        MVCGrid.reloadGrid(mvcGridName);
    };

    // public
    this.getFilters = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.filters;
    };

    // public
    this.setFilters = function (mvcGridName, obj) {
        setAdditionalQueryOptions(mvcGridName, obj);
    };

    // public
    this.getSortColumn = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.sortColumn;
    };

    // public
    this.getSortDirection = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.sortDirection;
    };

    // public
    this.setSort = function (mvcGridName, sortColumn, sortDirection) {
        queryOptions[mvcGridName].sort = sortColumn;
        queryOptions[mvcGridName].dir = sortDirection;
        MVCGrid.reloadGrid(mvcGridName);
    };

    // public
    this.getPage = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.pageNumber;
    };

    // public
    this.setPage = function (mvcGridName, pageNumber) {
        queryOptions[mvcGridName].page = pageNumber;
        MVCGrid.reloadGrid(mvcGridName);
    };

    // public
    this.getPageSize = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.itemsPerPage;
    };

    // public
    this.setPageSize = function (mvcGridName, pageSize) {
        queryOptions[mvcGridName].pagesize = pageSize;
        MVCGrid.reloadGrid(mvcGridName);
    };

    // public
    this.getAdditionalQueryOptions = function (mvcGridName) {
        var clientJson = getClientData(mvcGridName);
        return clientJson.additionalQueryOptions;
    };

    // public
    this.setAdditionalQueryOptions = function (mvcGridName, obj) {
        $.each(obj, function (k, v) {
            queryOptions[mvcGridName][k] = v;
        });
        MVCGrid.reloadGrid(mvcGridName);
    };

    // public
    this.reloadGrid = function (mvcGridName) {
        var tableHolderHtmlId = 'MVCGridTableHolder_' + mvcGridName;
        var loadingHtmlId = 'MVCGrid_Loading_' + mvcGridName;
        var errorHtmlId = 'MVCGrid_ErrorMessage_' + mvcGridName;

        var gridDef = findGridDef(mvcGridName);;

        var ajaxBaseUrl = handlerPath;

        if (gridDef.renderingMode == 'controller') {
            ajaxBaseUrl = controllerPath;
        }

        var fullAjaxUrl = ajaxBaseUrl + location.search;

        $.each(gridDef.pageParameters, function (k, v) {
            var thisPP = "_pp_" + gridDef.qsPrefix + k;
            fullAjaxUrl = updateURLParameter(fullAjaxUrl, thisPP, v);
        });

        queryOptions[mvcGridName].Name = mvcGridName;

        $.ajax({
            type: "GET",
            url: fullAjaxUrl,
            data: queryOptions[mvcGridName],
            cache: false,
            beforeSend: function () {
                if (gridDef.clientLoading != '') {
                    window[gridDef.clientLoading]();
                }

                $('#' + loadingHtmlId).css("visibility", "visible");
            },
            success: function (result) {
                $('#' + tableHolderHtmlId).html(result);
            },
            error: function (request, status, error) {
                var errorhtml = $('#' + errorHtmlId).html();

                if (showErrorDetails) {
                    $('#' + tableHolderHtmlId).html(request.responseText);
                } else {
                    $('#' + tableHolderHtmlId).html(errorhtml);
                }
            },
            complete: function () {
                if (gridDef.clientLoadingComplete != '') {
                    window[gridDef.clientLoadingComplete]();
                }

                $('#' + loadingHtmlId).css("visibility", "hidden");
            }
        });
    };

    // public
    this.getExportUrl = function (mvcGridName) {
        return MVCGrid.getEngineExportUrl(mvcGridName, 'export');
    };

    // public
    this.getEngineExportUrl = function (mvcGridName, engineName) {
        var gridDef = findGridDef(mvcGridName);

        var exportBaseUrl = handlerPath;

        var fullExportUrl = exportBaseUrl + location.search;
        fullExportUrl = updateURLParameter(fullExportUrl, 'engine', engineName);
        fullExportUrl = updateURLParameter(fullExportUrl, 'Name', mvcGridName);

        $.each(gridDef.pageParameters, function (k, v) {
            var thisPP = "_pp_" + gridDef.qsPrefix + k;
            fullExportUrl = updateURLParameter(fullExportUrl, thisPP, v);
        });

        return fullExportUrl;
    };
};

$(function () {
    MVCGrid.init();
});
