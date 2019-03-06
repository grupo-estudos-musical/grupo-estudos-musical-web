$(function () {
    let inputMensagem = $('#mensagem');
    let value = inputMensagem.val();
    if (value) {
        showErrorMessage(value);
    }
});