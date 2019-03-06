$(document).ready(function () {
    console.log(window.data);
    if (window.data !== undefined && window.data !== null && window.data !== "") {
        $('#loginTxt').text(window.data);
        $('#loginlogout')[0].innerHTML = '<span class="glyphicon glyphicon-log-out"></span> Выйти';
        $('#loginlogout').attr('href', '/Home/Logout');
    }
});