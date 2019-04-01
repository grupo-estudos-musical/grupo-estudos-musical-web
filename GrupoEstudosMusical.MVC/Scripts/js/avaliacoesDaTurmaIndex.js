function ObterDadosDaAvaliacao(idAvaliacao, idTurma)
{
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Avaliacoes/ObterDadosDaAvaliacao",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { AvaliacaoID: idAvaliacao, TurmaID : idTurma},
        success: function (response) {
            if (response.Retorno) {
                console.log(Convert.Dresponse.DataPrevista);
                console.log(response.DataRealizada);
                console.log(response.listaAvaliacoes);
            } else {
                console.log("Teste");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Error");

        }
    });
}

function removerAvaliacaoTurma(event, Id, NomeTurma) {
    let modal = $('.modal-body');
    modal.find('#IdAvaliacaoTurma').val(Id);
    modal.find('#NomeTurma').val(NomeTurma);
}