$('#btn-enviar-email').click(function (event) {

    event.preventDefault();
    let dados = obterDados();
    enviarEmail(dados);
});

function obterDados() {

    let assunto = $('#input-assunto').val();
    let titulo = $('#input-titulo').val();
    let mensagem = $('#input-mensagem').val();

    let turmasSelecionadas = $('.ms-selection').find('.ms-selected').toArray();

    let idsTurma = turmasSelecionadas.map(function (li) {
        return $(li).attr('turma-id');
    });

    return {
        assunto,
        titulo,
        mensagem,
        idsTurma
    };
}

function enviarEmail(dados) {
    $.ajax({
        url: '/Notificacao/EnviarEmails',
        method: 'POST',
        data: dados
    }).done(function (data) {        
        console.log(data);
        swal("Sucesso", data.SuccessMessage, "success");
    }).fail(function (xhr, textStatus, error) {
        let data = xhr.responseJSON;
        swal("Erro", data.ErrorMessage, "error");
    });
}