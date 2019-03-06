$(document).ready(function () {
    var login = $.cookie('login');

    if (login === null || login === undefined || login === "") {
        window.location.replace("/Home/Autorisation");
        return;
    }

    if (login !== undefined && login !== null && login !== "") {
        $('#loginTxt').text(login);
        $('#loginlogout')[0].innerHTML = '<span class="glyphicon glyphicon-log-out"></span> Выйти';
        $('#loginlogout').attr('href', '/Home/Logout');
    }
});