$(window).ready(function () {
    $('.form-control').css({
        borderWidth: "0"
    });

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

    //if (message !== null && message !== undefined && message !== "") {

    //};

    $('#inputLogin').focusin(function (event) {
        $('#inputLogin').css({
            borderWidth: "0"
        });
        $('#reInputInfo5').text("");
    });

    $('#inputPassword').focusin(function (event) {
        $('#inputPassword').css({
            borderWidth: "0"
        });
        $('#reInputInfo6').text("");
    });

    $("#au").click(function (event) {
        event.preventDefault();

        var login = $('#inputLogin').val();
        var password = $('#inputPassword').val();


        if (login.length === 0 && password.length === 0) {

            $('#inputLogin').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo5').text("Не введен логин").css({
                color: 'red'
            });

            $('#inputPassword').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo6').text("Не введен пароль").css({
                color: 'red'
            });

            return;
        };

        if (login.length === 0) {
            $('#inputLogin').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo5').text("Не введен логин").css({
                color: 'red'
            });

            return;
        };

        if (password.length === 0) {
            $('#inputPassword').css({
                borderColor: "red",
                borderWidth: "2px"

            });

            $('#reInputInfo6').text("Не введен пароль").css({
                color: 'red'
            });

            return;
        };
        

        $.ajax({
            url: '/Home/Login',
            method: 'POST',
            data: {
                login: login,
                password: password
            },
            dataType: 'JSON',
            success: function (jsonData) {
                if (jsonData.ok) {
                    window.location.replace("/Home/Index");
                    return;
                }

                if (jsonData !== null && jsonData !== undefined) {
                    $('#inputLogin').css({
                        borderColor: "red",
                        borderWidth: "2px"

                    });

                    $('#inputPassword').css({
                        borderColor: "red",
                        borderWidth: "2px"

                    });

                    $('#reInputInfo5').text(jsonData.message).css({
                        color: 'red'
                    });

                    $('#reInputInfo6').text(jsonData.message).css({
                        color: 'red'
                    });
                }
            }
        });
    });

});

