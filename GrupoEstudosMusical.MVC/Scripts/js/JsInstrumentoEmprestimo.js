function OnSuccess(response) {
    if (response && response.result == true) {
        $("#modalInstrumento").modal("hide");
        swal("Sucesso", "Deu certo", "success").then(function () {
            location.reload();
        })

    } else {
        $("#modalAvaliacaoDaTurma").modal("hide");
        swal("Atenção", response.ResponseMessage, "warning");
    }
}