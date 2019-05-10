$("#btn-alterar-documentos").click(function () {
    let dados = obterDados();
    enviarDados(dados);
});

function obterDados() {
    let data = {};
    data["IdMatricula"] = parseInt($('[name=Id]').val());
    let documentos = $('#documentos');
    let checkDocumentos = documentos.find('.form-block');
    for (var i = 0; i < checkDocumentos.length; i++) {
        let input = $(checkDocumentos[i]).find('[type=checkbox]').attr('name');
        let span = $(checkDocumentos[i]).find('span');
        let style = span.attr('style');
        if (style.includes('rgb(255, 255, 255)')) {
            data[input] = false;
        } else {
            data[input] = true;
        }
    }
    return data;
}

function enviarDados(dados) {
    //let headers = {};
    //let token = $('[name=__RequestVerificationToken]').val();
    //headers["__RequestVerificationToken"] = token;
    console.log(dados);
    $.ajax({
        method: "POST",
        url: "/Matriculas/AlterarDocumentosApresentados",
        data: dados
        //contentType: "application/json"
        //headers: headers,
    }).done(function (data) {
        let statusMatricula = $('#status-matricula');
        if (data.pendente) {
            statusMatricula.text("Pendente");
            statusMatricula.parent().removeClass("status-concluida");
            statusMatricula.parent().addClass("status-ativo");
        } else {
            statusMatricula.text("Em ordem");
            statusMatricula.parent().removeClass("status-ativo");
            statusMatricula.parent().addClass("status-concluida");
        }
        showSuccess(data.SuccessMessage);
    }).fail(function () {
        showErrorMessage(data.ErrorMessage);
    });
}