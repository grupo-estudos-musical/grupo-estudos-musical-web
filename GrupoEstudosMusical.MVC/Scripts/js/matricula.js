function selecionarTurma(event) {
    let btn = $(event);
    let inputTurmaId = $('#TurmaId');
    selecionarBotao(btn, inputTurmaId, 'turma-id');
    
    let turmaId = btn.attr('turma-id');
    inputTurmaId.val(turmaId);

    let nomeTurma = btn.attr('turma-nome');
    $('#TurmaNome').val(nomeTurma);
}

function selecionarModulo(event) {
    let btn = $(event);
    let inputModuloId = $('#ModuloId');
    selecionarBotao(btn, inputModuloId, 'modulo-id');

    let moduloId = btn.attr('modulo-id');
    inputModuloId.val(moduloId);

    let idAluno = $('#AlunoId').val();
    obterTurmasPorModulo(moduloId, idAluno);
}

function selecionarBotao(elementoClicado, inputId, atributoElemento) {
    const classeBotao = 'btn-border';
    if (inputId.val() !== 0) {
        let botoes = $(`[${atributoElemento}]`);
        for (var i = 0; i < botoes.length; i++) {
            let botao = $(botoes[i]);
            if (!botao.hasClass(classeBotao)) {
                botao.addClass(classeBotao);
            }
        }
    }

    elementoClicado.removeClass(classeBotao);
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

function obterTurmasPorModulo(idModulo, idAluno) {
    
    $.ajax({
        url: '/Turmas/TurmasAtivas?moduloId=' + idModulo + '&alunoId=' + idAluno,
        method: 'GET'
    }).done(function (data) {
        console.log(data);
        exibirTurmas(data);
    }).fail(function (error) {
        console.log('Não foi possível obter turmas.');
    });
}

function exibirTurmas(dados) {
    const TAMANHO_LINHA = 4;
    let quantFinal = 4;
    let quantLinhas = Math.ceil(dados.length / TAMANHO_LINHA);

    let divTurmas = $('#turmas');
    divTurmas.html('');

    for (var pagina = 0; pagina < quantLinhas; pagina++) {
        let linha = $('<div>').addClass('row');
        let turmasLinha = dados.slice(TAMANHO_LINHA * pagina, quantFinal);
        for (var i = 0; i < turmasLinha.length; i++) {
            let turma = turmasLinha[i];
            let divTurma = $('<div>').addClass('col-md-3');
            let btnTurma =
                $('<button>').addClass('btn').addClass('btn-primary').addClass('btn-lg').addClass('btn-border').addClass('btn-matricula');
            btnTurma.attr('type', 'button');
            btnTurma.attr('turma-id', turma.Id);
            btnTurma.attr('turma-nome', turma.Nome);
            btnTurma.attr('onclick', 'selecionarTurma(this)');
            btnTurma.html(turma.Nome);
            btnTurma.append($('<br>'));
            btnTurma.append($('<br>'));
            btnTurma.append(`Alunos ${turma.QuantidadeMatriculas}/${turma.QuantidadeAlunos}`);
            if (turma.AlunoMatriculado) {
                btnTurma.attr('disabled', 'disabled');
                btnTurma.removeClass('btn-primary');
                btnTurma.addClass('btn-danger');
            }
            divTurma.append(btnTurma);
            linha.append(divTurma);
        }
        divTurmas.append(linha);
        divTurmas.append($('<br>'));
        quantFinal += 4;
    }
}