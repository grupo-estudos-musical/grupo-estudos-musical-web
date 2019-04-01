function OnSuccess(response) {
    if (response && response.Ok == true) {
        $("#modalAvaliacaoDaTurma").modal("hide");
        swal("Sucesso", response.ResponseMessage, "success").then(function () {
            location.reload();
        })

    } else {
        $("#modalAvaliacaoDaTurma").modal("hide");
        swal("Atenção", response.ResponseMessage, "warning");
    }
}
