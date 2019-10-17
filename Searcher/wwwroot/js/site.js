//TODO
function find(keyWord, url) {
    var model = { KeyWord: keyWord };
    var result = null;
    $.ajax({
        url: url,
        async: false,
        type: 'POST',
        data: model,
        datatype: 'json',
        success: function (data) { result = data; },
        error: function (jqXHR, textStatus, errorThrown) { alert(errorThrown); }
    });
    return result;
}

function addRow(tableId, model) {
    debugger;
    var empTab = document.getElementById(tableId);

    var rowCnt = empTab.rows.length;        // GET TABLE ROW COUNT.
    var tr = empTab.insertRow(rowCnt);      // TABLE ROW.
    tr = empTab.insertRow(rowCnt);

    for (var c = 0; c < 2; c++) {
        var td = document.createElement('td');          // TABLE DEFINITION.
        td = tr.insertCell(c);

        var displayText = model.value;
        if (c == 0) {           // FIRST COLUMN.
            displayText = model.url;
        }
        var ele = document.createElement('div');
        ele.innerHTML = displayText;
        td.appendChild(ele);
    }
}
