MudarCor()
function MudarCor() {
    var status = document.querySelectorAll('#status_turma');

    status.forEach(function (texto)
    {
        if (texto.textContent == "Ativo") {
            texto.style.color = "green";
        } else {
            texto.style.color = "blue";
        }
    });
}