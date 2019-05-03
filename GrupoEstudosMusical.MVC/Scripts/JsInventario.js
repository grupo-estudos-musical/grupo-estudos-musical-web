function ExibirBotaoParaEdicao() {
    DesbloquearCamposDeEdicao(); 
    $("#formAlteracoesInventario").toggle();
    $("#BotaoPrincipal").prop("disabled","true");
    
}

function AtualizarInventario()
{
    if (VerificarIrregularidadesNoInventario() > 0) {
        swal("Atenção", "o estoque minímo e a quantidade disponível não pode ser inferior a 0!", "error");
    } else {
        $('table tbody tr').each(function () {
            var estoqueMinimo = $(this).find('#instrumento_EstoqueMinimo').val();
            var quantidadeDisponivel = $(this).find('#instrumento_QuantidadeDisponivel').val();
            const idInventario = $(this).find('#instrumento_InventarioID').val();
            ProcessarEdicao(idInventario, estoqueMinimo, quantidadeDisponivel);
        });
    }   
}

function VerificarIrregularidadesNoInventario() {
    console.log("To aqui");
    var irregularides = 0;
    $('table tbody tr').each(function () {
        var estoqueMinimo = $(this).find('#instrumento_EstoqueMinimo').val();
        var quantidadeDisponivel = $(this).find('#instrumento_QuantidadeDisponivel').val();
        if (estoqueMinimo < 0 || quantidadeDisponivel < 0) {
            irregularides += 1;
        }
    });
    console.log("a qwuantidade" + irregularides);
    return irregularides;
}


function ProcessarEdicao( idInventario,estoqueMinimo, quantidadeDisponivel) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Inventario/AtualizarInventario",
        data: { idInventario, quantidadeMinima: estoqueMinimo, quantidadeDisponivel },
        success: function (retorno) {
            if (retorno.result) {
                swal("Sucesso", "Inventário ajustado!", "success").then(function () {
                    OcultarBotaoParaEdicao();
                });
                
            } else {
                swal("Atenção", retorno.mensagem, "error");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

            alert(errorThrown);
        }

    });
}

function OcultarBotaoParaEdicao() {
    $('table tbody tr').each(function () {
        $(this).find('#instrumento_EstoqueMinimo').prop("disabled", true);
        $(this).find('#instrumento_QuantidadeDisponivel').prop("disabled", true);
    });
    $("#formAlteracoesInventario").toggle();
    $("#BotaoPrincipal").removeAttr("disabled");
}

function DesbloquearCamposDeEdicao() {
    $('table tbody tr').each(function () {
        $(this).find('#instrumento_EstoqueMinimo').prop("disabled", false);
        $(this).find('#instrumento_QuantidadeDisponivel').prop("disabled", false);
    });
}

$(document).ready(function () {
    $("#formAlteracoesInventario").hide();
});