$(document).ready(function () {

    $('.form-control').css({
        borderWidth: "0"
    });

    // зачем мы сначала удаляем а потом добавляем класс чтобы футтер внизу был

    $('#reg').click(function () {
        $('#enterForm').ready(function () {
            setTimeout(function () {
                $("#footerContent").removeClass("navbar-fixed-bottom");
            }, 200); 
        });
    });

    $('#auth').click(function () {
        $('#enterForm').ready(function () {
            setTimeout(function () {
                $("#footerContent").addClass("navbar-fixed-bottom");
            }, 150);
        });
    });

    $("#regButton").click(function (event) {
        event.preventDefault();

        var name = $('#inputName1').val();
        var login = $('#inputLogin1').val();
        var password = $('#inputPassword1').val();
        var password2 = $('#reinputPassword1').val();

       
        if (name.length === 0 && login.length === 0 && password.length === 0 && password2.length === 0) {

            $('#inputName1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo4').text("Не введено имя").css({
                color: 'red'
            });

            $('#inputLogin1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo3').text("Не введен логин").css({
                color: 'red'
            });

            $('#inputPassword1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo2').text("Не введен пароль").css({
                color: 'red'
            });

            $('#reinputPassword1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo').text("Не введен пароль").css({
                color: 'red'
            });

            return;
        };


        if (name.length === 0) {
            $('#inputName1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo4').text("Не введено имя").css({
                color: 'red'
            });

            return;
        };

        if (login.length === 0) {
            $('#inputLogin1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo3').text("Не введен логин").css({
                color: 'red'
            });

            return;
        };

        if (password.length === 0) {
            $('#inputPassword1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo2').text("Не введен пароль").css({
                color: 'red'
            });

            return;
        };

        if (password2 != password) {
            alert('Ошибка! Пароли не совпадают')

            $('#inputPassword1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reinputPassword1').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo2').text("Пароли не совпадают").css({
                color: 'red'
            });

            $('#reInputInfo').text("Пароли не совпадают").css({
                color: 'red'
            });

            return;
        };


        $.ajax({
            url: '/Home/RegAction',
            method: 'POST',
            data: {
                name: name,
                login: login,
                password: password,
                role: "Клиент"
            },
            success: function () {
                  
            }
        });
    });

    $('#inputName1').focusin(function (event) {
        $('#inputName1').css({
            borderWidth: "0"
        });
        $('#reInputInfo4').text("");
    });

    $('#inputLogin1').focusin(function (event) {
        $('#inputLogin1').css({
            borderWidth: "0"
        });
        $('#reInputInfo3').text("");
    });

    $('#inputPassword1').focusin(function (event) {
        $('#inputPassword1').css({
            borderWidth: "0"
        });
        $('#reInputInfo2').text("");
    });

    $('#reinputPassword1').focusin(function (event) {
        $('#reinputPassword1').css({
            borderWidth: "0"
        });
        $('#reInputInfo').text("");
    });

});