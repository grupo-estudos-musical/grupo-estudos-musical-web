function apagar(event, id, nome) {
    let modal = $('.modal-body');
    modal.find('input').val(id);

    let modalHeader = $('.modal-header');
    modalHeader.find('.modal-title').text(nome);
}

$("#Cep").blur(function (event) {
    let cep = $("#Cep").val();
    console.log(cep);

    $.ajax({
        url: "/Cep/Consultar?cep=" + cep,
        method: "GET"
    }).done(function (data) {
        $("#Endereco").val(data.Logradouro);
        $("#Bairro").val(data.Bairro);
        $("#Cidade").val(data.Localidade);
        $("#Uf").val(data.UF);
    });
});