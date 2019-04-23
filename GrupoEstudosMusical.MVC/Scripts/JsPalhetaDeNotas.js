function ObterPalheta() {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Turmas/ObterPalhetasDeNotasPorAvaliacaoEhTurma",
        data: { AvaliacaoID: $("#Avaliacao").val(), TurmaID : $("#TurmaID").val() },
        success: function (retorno) {
            $("#divPalhetasDeNotas").html(retorno);
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

            alert(errorThrown);
        }
    });
}



function ObterNotasPelaTabela() {
    $('table tbody tr').each(function () {
        var nota = $(this).find('#palheta_Nota').val().replace('.', ',');

        var idpalheta = $(this).find('#PalhetaId').val();

        if (ValidarCampoNota(nota)) {
            swal("Atenção", "Insira um valor correspondente a uma nota!", "warning");

        } else {
            LancarNotaAluno(nota, idpalheta);
        }



    });
}
function LancarNotaAluno(nota, idpalheta) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Turmas/LancarNotaDoAluno",
        data: { nota, palhetaID: idpalheta },
        success: function (retorno) {
            if (retorno.result) {
                swal("Sucesso", retorno.mensagem, "success").then(function () {
                    location.reload();
                })
            } else {
                swal("Atenção", retorno.mensagem, "error");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

            alert(errorThrown);
        }

    });
}
function ValidarCampoNota(nota) {
    var verificarSeEhUmValorValido = parseFloat(nota);
    return isNaN(verificarSeEhUmValorValido);
}