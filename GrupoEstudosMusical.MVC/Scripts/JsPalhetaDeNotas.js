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
    if (VerificarIrregularidesNasNotas() > 0) {
        swal("Atenção", "Existe uma ou mais notas inválidas!", "warning");
    } else {
        $('table tbody tr').each(function () {
            var nota = $(this).find('#palheta_Nota').val().replace('.', ',');
            var idpalheta = $(this).find('#PalhetaId').val();
            LancarNotaAluno(nota, idpalheta);
        });
    }
}
function VerificarIrregularidesNasNotas() {
    console.log("Entrei na verificação das irregularidades");
    var qntNotasIrregulares = 0;
    $('table tbody tr').each(function () {
        var nota = $(this).find('#palheta_Nota').val().replace('.', ',');
        if (ValidarCampoNota(nota)) {
            qntNotasIrregulares = qntNotasIrregulares + 1;
        }
    });
    return qntNotasIrregulares;
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