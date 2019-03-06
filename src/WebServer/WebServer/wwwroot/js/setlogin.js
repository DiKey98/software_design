$(document).ready(function () {
    if (window.login !== undefined && window.login !== null && window.login !== "") {
        $('#loginTxt').text(window.login);
        $('#loginlogout')[0].innerHTML = '<span class="glyphicon glyphicon-log-out"></span> Выйти';
        $('#loginlogout').attr('href', '/Home/Logout');
    }
});