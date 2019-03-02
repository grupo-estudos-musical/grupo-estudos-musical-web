function selecionarTurma(event) {
    let btn = $(event);
    let inputTurmaId = $('#TurmaId');

    //verifica se já existe uma turma selecionada
    if (inputTurmaId.val() !== 0) {
        let turmas = $('[turma-id]');
        for (var i = 0; i < turmas.length; i++) {
            let turma = $(turmas[i]);
            if (!turma.hasClass('btn-border')) {
                turma.addClass('btn-border');
                break;
            }
        }
    }

    btn.removeClass('btn-border');
    let turmaId = btn.attr('turma-id');
    inputTurmaId.val(turmaId);

    let nomeTurma = btn.attr('turma-nome');
    $('#TurmaNome').val(nomeTurma);
}

function exibirCofirmacao(event) {
    let divTurma = $('#pills-tab3');
    if (!divTurma.hasClass('active')) {
        let divConfirmacao = $('#pills-tab4');
        if (!divConfirmacao.hasClass('active')) {
            return;
        }
        else {
            if (!verificarDocumentos()) {
                $('#modal-confirmar-matricula').modal();
            }
            else {
                submeterFormularioMatricula();
            }
        }
    }

    let inputNome = $('#Aluno_Nome');
    let inputTurma = $('#TurmaNome');
    let nomeAluno = $('#nome-aluno');
    let nomeTurma = $('#nome-turma');

    nomeAluno.text(inputNome.val());
    nomeTurma.text(inputTurma.val());
}

//verifica se todos os documentos estão selecionados
function verificarDocumentos() {
    let divDocumentos = $('#pills-tab2');
    let checkDocumentos = divDocumentos.find('span');
    for (var i = 0; i < checkDocumentos.length; i++) {
        let span = $(checkDocumentos[i]);
        let style = span.attr('style');
        if (style.includes('rgb(255, 255, 255)')) {
            return false;
        }
    }
    return true;
}

function submeterFormularioMatricula() {
    let form = $('#commentForm');
    form.submit();
}

function confirmarModal() {
    submeterFormularioMatricula();
}