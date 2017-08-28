/* Create an array with the values of all the input boxes in a column */
$.fn.dataTable.ext.order['dom-text'] = function (settings, col) {
    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
        return $('input', td).val();
    });
}

/* Create an array with the values of all the input boxes in a column, parsed as numbers */
$.fn.dataTable.ext.order['dom-text-numeric'] = function (settings, col) {
    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
        return $('input', td).val() * 1;
    });
}

/* Create an array with the values of all the select options in a column */
$.fn.dataTable.ext.order['dom-select'] = function (settings, col) {
    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
        return $('select', td).val();
    });
}

/* Create an array with the values of all the checkboxes in a column */
$.fn.dataTable.ext.order['dom-checkbox'] = function (settings, col) {
    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
        return $('input', td).prop('checked') ? '1' : '0';
    });
}

$(document).ready(function () {

    AplicarDataTableResposta();
    AplicarDataTableRecebe();

    $("#btnRespostaIniciar").on("click", function () {

        var tempo = $("#tempoResposta").val();

        if (tempo === undefined || tempo > "0") {
            tempo = (tempo * 1000);
            setInterval(AtualizarPartialViewResposta, parseInt(tempo));
            alert("Atualizacao Automatica Iniciada");
        } else {
            alert("Defina um valor maior que 0");
        }
    });

});

function AtualizarPartialViewResposta(){
    console.log("Atualizando tabela");
    $.ajax(
        {
            type: 'GET',
            url: '/Painel/TabelaPartialViewResposta',
            dataType: 'html',
            cache: false,
            async: true,
            success: function (data) {
                $('#respostaTable').html(data);
                AplicarDataTableResposta();
            }
        });

};

function AplicarDataTableResposta() {
    $('.data-table-painel-resposta').DataTable({
        "Columns": [
            null,
            null,
            {
                sSortDataType: 'dom-text',
                sType: 'numeric'
            }
        ],
        "order": [[2, "desc"]],
        "createdRow": function (row, data) {
            if (data[2] >= 2000) {
                $(row).addClass("alert-danger");
            }
            if (data[2] < 2000 && data[2] >= 1000) {
                $(row).addClass("alert-info");
            }
            if (data[2] < 1000) {
                $(row).addClass("alert-success");
            }
        }
    });
}

function AplicarDataTableRecebe() {
    $('.data-table-painel').DataTable({
        "Columns": [
            null,
            null,
            {
                sSortDataType: 'dom-text',
                sType: 'numeric'
            }
        ],
        "order": [[2, "desc"]],
        "createdRow": function (row, data) {
            if (data[2] >= 2000) {
                $(row).addClass("alert-danger");
            }
            if (data[2] < 2000 && data[2] >= 1000) {
                $(row).addClass("alert-info");
            }
            if (data[2] < 1000) {
                $(row).addClass("alert-success");
            }
        }
    });
}