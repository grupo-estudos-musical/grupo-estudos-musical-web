$(function () {
    (function exibirMensagem() {
        let mensagem = $('[name=mensagem]').val();
        let tipo = $('[name=tipo]').val();

        if (mensagem && tipo) {
            if (tipo === "success")
                swal("Sucesso", mensagem, "success");
            else if (tipo === "error")
                swal("Ops!", mensagem, "error");
            else if (tipo === "warning")
                swal("Atenção", mensagem, "warning");
        }
    })();

    $("#Cep").blur(function (event) {
        let cep = $("#Cep").val();
        console.log(cep);

        $.ajax({
            url: "/Cep/Consultar?cep=" + cep,
            method: "GET"
        }).done(function (data) {
            $("#Logradouro").val(data.Logradouro);
            $("#Bairro").val(data.Bairro);
            $("#Cidade").val(data.Localidade);
            $("#Uf").val(data.UF);
        });
    });
});

function apagar(event, id, nome) {
    let modal = $('.modal-body');
    modal.find('input').val(id);

    let modalHeader = $('.modal-header');
    modalHeader.find('.modal-title').text(nome);
}