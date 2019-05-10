$('#btn-enviar-email').click(function (event) {

    event.preventDefault();    

    let validate = validarForm();
    if (validate.form()) {
        let dados = obterDados();
        enviarEmail(dados);
    }
});

function resetarForm() {
    $('#input-assunto').val('');
    $('#input-assunto').parent().removeClass('has-success');

    $('#input-titulo').val('');
    $('#input-titulo').parent().removeClass('has-success');

    $('#input-mensagem').val('');
    $('#input-mensagem').parent().removeClass('has-success');

    $('[name=turmas]').val('');
    $('[name=turmas]').parent().removeClass('has-success');
}

function validarForm() {
    return $('#form_email').validate({
        rules: {
            turmas: {
                required: true
            },
            assunto: {
                required: true
            },
            titulo: {
                required: true
            },
            mensagem: {
                required: true
            }
        },
        highlight: function (element) {
            var parent = $(element).parent('.form-group');
            parent.removeClass('has-success').addClass('has-error');
        },
        success: function (label, element) {
            var parent = $(element).parent('.form-group');
            parent.removeClass('has-error').addClass('has-success');
        }
    });
}

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
        resetarForm();
    }).fail(function (xhr, textStatus, error) {
        let data = xhr.responseJSON;
        swal("Erro", data.ErrorMessage, "error");
    });
}