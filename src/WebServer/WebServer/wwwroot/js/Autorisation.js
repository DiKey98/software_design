$(window).ready(function () {
    $('.form-control').css({
        borderWidth: "0"
    });

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
                if (jsonData.where !== null && jsonData.where !== undefined) {
                    window.where = jsonData.where;
                }

                if (jsonData.ok) {
                    if (window.where !== null && window.where !== undefined
                        && window.where !== "" && window.where.length !== 0 &&
                        jsonData.role.toLowerCase() === "клиент") {
                        window.location.replace(window.where);
                        return;
                    }
                    window.location.replace("/");
                    return;
                }

                if (jsonData.message !== null && jsonData.message !== undefined) {
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

