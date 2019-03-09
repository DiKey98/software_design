(function () {
    'use strict';

    $('.form-control').css({
        borderWidth: "0"
    });

    $('.input-file').each(function () {
        var $input = $(this),
            $label = $input.next('.js-labelFile'),
            labelVal = $label.html();

        $input.on('change', function (element) {
            var fileName = '';
            if (element.target.value) fileName = element.target.value.split('\\').pop();
            fileName
                ? $label.addClass('has-file').find('.js-fileName').html(fileName)
                : $label.removeClass('has-file').html(labelVal);
        });
    });

    $('#changeBtn').click(function(e) {
        e.preventDefault();
        var files = document.getElementById('file').files;
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }

            var name = $('#inputName').val();
            var mes = $('#inputMeasurement').val();
            var cost = $('#inputCost').val();

            data.append("name", name);
            data.append("measurement", mes);
            data.append("cost", cost);
            data.append("id", window.serviceid);

            $.ajax({
                method: "POST",
                url: "/Services/ChangeAction",
                contentType: false,
                processData: false,
                data: data,
                dataType: "JSON",
                success: function (result) {
                    if (result.ok) {
                        alert("Услуга успешно изменена");
                        window.location.replace("/Home/Services");
                        return;
                    }
                }
            });
        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5!");
        }
    });
})();