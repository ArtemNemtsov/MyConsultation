$(document).ready(function ()
{
    var correctLength = 14;

    $("idForm").submit(function (e) {
        const snils = $("#idSnils").val();
        e.preventDefault();

        if (snils.length != correctLength) {

            document.getElementById('idInformation').innerHTML = "Введено неверное количество символов для СНИЛС";
            return false;
        }
        else {
            document.getElementById('idInformation').innerHTML = "";

            $.ajax({
                type: "Post",
                url: '/Patient/CheckSnilsValid',
                data:
                {
                    newSnils: snils
                },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                success: function (data) {
                    let result = data;

                    if (result.isValid) {
                        document.getElementById('idInformation').innerHTML = "";
                        document.getElementById("idForm").submit();

                    }
                    else {
                        document.getElementById('idInformation').innerHTML = "Введенные данные СНИЛС некоректны, попробуйте снова!";
                    }
                }
            })
        }
    });
})
