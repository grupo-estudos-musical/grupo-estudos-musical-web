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
}