function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        console.log(reader);
        reader.onload = function (e) {
            $('#imagemPreview').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        var img = input.value;
        $(input).next().attr('src', img);
    }
}

function verificaMostraBotao() {
    $('input[type=file]').each(function (index) {

        if ($('input[type=file]').eq(index).val() !== "") {
            readURL(this);

            $('.hide').show();
        }
    });
}

$('input[type=file]').on("change", function () {
    console.log(this);
    verificaMostraBotao();
});
