$(document).ready(function ()
{

    $("#idForm").submit(function (e) 
    {
        e.preventDefault();
        var correctLength = 14;   
        var snils = $("#idSnils").val();
        var idPatient = $("#IdPatient").val();

        if (snils.length != correctLength)
        {
            document.getElementById('idInformation').innerHTML = "Введено неверное количество символов для СНИЛС";
            return false;
        }
        else
        {
            document.getElementById('idInformation').innerHTML = "";

            $.ajax({
                type: "Post",
                url: '/Patient/CheckSnils',
                data:
                {
                    Snils: snils,
                    IdPatient: idPatient
                },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                success: function (data) {
                    let result = data;

                    if (result.success) {
                        document.getElementById('idInformation').innerHTML = "";
                        document.getElementById("idForm").submit();
                    }
                    else
                    {
                        document.getElementById('idInformation').innerHTML = result.errorsMessage;
                    }
                }
            })
        }
    })
})
