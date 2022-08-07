﻿
    $("#btnExport").click(
    function () {
        tableToExcel('testExportId', 'test', 'TestExport');

            });

    function getIEVersion() {
            var rv = -1;
    if (navigator.appName == 'Microsoft Internet Explorer') {
                var ua = navigator.userAgent;
    var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
    if (re.exec(ua) != null)
    rv = parseFloat(RegExp.$1);
            }
    return rv;
        }

    function tableToExcel(table, sheetName, fileName) {
            var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If Internet Explorer
    {
                return fnExcelReport(table, fileName);
            }

    var uri = 'data:application/vnd.ms-excel;base64,',
    templateData = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines /></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8" /></head><body><table>{table}</table></body></html>',
    base64Conversion = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
    formatExcelData = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

            $("tbody > tr[data-level='0']").show();

    if (!table.nodeType)
    table = document.getElementById(table)

    var ctx = {worksheet: sheetName || 'Worksheet', table: table.innerHTML }

    var element = document.createElement('a');
    element.setAttribute('href', 'data:application/vnd.ms-excel;base64,' + base64Conversion(formatExcelData(templateData, ctx)));
    element.setAttribute('download', fileName);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);

            $("tbody > tr[data-level='0']").hide();
        }


    function fnExcelReport(table, fileName) {

            var tab_text = "<table border='2px'>";
        var textRange;

        if (!table.nodeType)
        table = document.getElementById(table)

            $("tbody > tr[data-level='0']").show();
        tab_text = tab_text + table.innerHTML;

        tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    txtArea1.document.open("txt/html", "replace");
    txtArea1.document.write(tab_text);
    txtArea1.document.close();
    txtArea1.focus();
    sa = txtArea1.document.execCommand("SaveAs", false, fileName + ".xlsx");
            $("tbody > tr[data-level='0']").hide();
    return (sa);
        }