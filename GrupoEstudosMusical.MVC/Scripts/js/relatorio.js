//function GerarRelatorioDeOcorrenciasAluno(id)
//{
//    $.ajax({
//        type: "GET",
//        url: document.location.origin + "/Relatorio/VerificarExistenciaDeDadosParaOcorrencia",
//        content: "application/json; charset=utf-8",
//        dataType: "json",
//        data: { AlunoId : id },
//        success: function (data) {
//            window.location.href = "/Relatorio/GerarRelatorioOcorrenciasPorAluno/" + id;
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            console.log("To");
//            swal("Atenção", xhr.responseText, "error")

//        }
//    });
//}

function GerarRelatorioDeDetalhamentoDeEmprestimo(id) {

    var existeDados = VerificaExistenciaDeDados(id, enumTiposDeRelatorio.DetalhesEmprestimo);

    if (existeDados) {
        window.location.href = "/Relatorio/GerarDetalhamentoDosEmprestimos/?alunoID=" + id;
    }

    //$.ajax({
    //    type: "GET",
    //    url: document.location.origin + "/Relatorio/VerificarExistenciaDeDadosParaOcorrencia",
    //    content: "application/json; charset=utf-8",
    //    dataType: "json",
    //    data: { AlunoId: id },
    //    success: function (data) {
    //        window.location.href = "/Relatorio/GerarRelatorioOcorrenciasPorAluno/" + id;
    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        console.log("To");
    //        swal("Atenção", xhr.responseText, "error")

    //    }
    //});
}

function GerarRelatorioDeOcorrenciasAluno(alunoID) {
    var existeDados = VerificaExistenciaDeDados(alunoID, enumTiposDeRelatorio.OcorrenciaPorAluno);
    if (existeDados) {
        window.location.href = "/Relatorio/GerarRelatorioOcorrenciasPorAluno/" + alunoID;
    }
}

function GerarRelatorioBoletimAluno(matriculaID) {
    var existeDados = VerificaExistenciaDeDados(matriculaID, enumTiposDeRelatorio.Boletim);
    
    if (existeDados) {
        window.location.href = "/Relatorio/GerarBoletim/?matriculaID=" + matriculaID;
    }
}

function GerarRelatorioBoletimTurma(turmaID) {
    var existeDados = VerificaExistenciaDeDados(turmaID, enumTiposDeRelatorio.BoletimDaTurma);

    if (existeDados) {
        window.location.href = "/Relatorio/GerarBoletimdaTurma/" + turmaID;
    }
}

function VerificaExistenciaDeDados(indice, tipoDeRelatorio) {
    var existe = false;
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Relatorio/VerificarExistenciaDeDadosParaRelatorio",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { indiceEmitirRelatorio: indice, tipoDeRelatorio },
        async: false,
        success: function (data) {
            if (data.result != null) {
                existe = true;
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            existe = false;
            swal("Atenção", xhr.responseText, "error");
            
        }
    });
    return existe;
}

const enumTiposDeRelatorio = {
    Boletim: 1,
    DetalhesEmprestimo: 2,
    OcorrenciaPorAluno: 3,
    BoletimDaTurma: 4
}

