function OnSuccess(response) {
    if (response && response.result == true) {
        $("#modalInstrumento").modal("hide");
        swal("Sucesso", response.mensagem, "success").then(function () {
            location.reload();
        })

    } else {
        $("#modalInstrumento").modal("hide");
        swal("Atenção", response.mensagem, "warning");
    }
}

function EfetuarDevolucao(instrumentoDoAlunoID)
{
    swal("Confirma a devolução deste instrumento ?",
        {
            buttons:
                {
                    Cancelar: "Cancelar",
                    Confirmar:
                        {
                            text: "Confirmar",
                            value: "Catch",
                        },
                },
        })
        .then((value) => {
            switch (value) {
                case "Catch":
                    ProcessarDevolucao(instrumentoDoAlunoID)
                    break;
                default:
            }
        });
    
}

function ProcessarDevolucao(instrumentoDoAlunoID) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Instrumentos/RealizarDevolucao",
        data: { instrumentoDoAlunoID },
        success: function (retorno) {
            if (retorno.result == true) {
                swal("Sucesso!", retorno.mensagem, "success").then(function () {
                    location.reload();
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

            alert(errorThrown);
        }
    });

}