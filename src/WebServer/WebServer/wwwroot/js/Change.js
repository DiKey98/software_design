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
            var meas = $('#inputMeasurement').val();
            var cost = $('#inputCost').val();

            data.append("name", name);
            data.append("measurement", meas);
            data.append("cost", cost);
            data.append("id", window.serviceid);

            if (name.length === 0 && meas.length === 0 && cost.length === 0) {

                $('#inputName').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo6').text("Не введено название услуги").css({
                    color: 'red'
                });

                $('#inputMeasurement').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo7').text("Не введена ед.из.").css({
                    color: 'red'
                });

                $('#inputCost').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo8').text("Не введена стоимость").css({
                    color: 'red'
                });

                return;
            };


            if (name.length === 0) {
                $('#inputName').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo6').text("Не введено название услуги").css({
                    color: 'red'
                });

                return;
            };

            if (meas.length === 0) {
                $('#inputMeasurement').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo7').text("Не введена ед.из.").css({
                    color: 'red'
                });

                return;
            };

            if (cost.length === 0) {
                $('#inputCost').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo8').text("Не введена стоимость").css({
                    color: 'red'
                });

                return;
            };

            var sign = "-";

            if (cost.includes(sign)) {
                $('#inputCost').css({
                    borderColor: "red",
                    borderWidth: "2px"

                });

                $('#reInputInfo8').text("Значение не может быть отрицательм").css({
                    color: 'red'
                });

                return;
            };

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

                    if (result.message !== null && result.message !== undefined) {
                        $('#inputName').css({
                            borderColor: "red",
                            borderWidth: "2px"

                        });

                        $('#inputMeasurement').css({
                            borderColor: "red",
                            borderWidth: "2px"

                        });

                        $('#inputCost').css({
                            borderColor: "red",
                            borderWidth: "2px"

                        });

                        $('#reInputInfo6').text(result.message).css({
                            color: 'red'
                        });

                        $('#reInputInfo7').text(result.message).css({
                            color: 'red'
                        });

                        $('#reInputInfo8').text(result.message).css({
                            color: 'red'
                        });

                    }
                }
            });
        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5!");
        }
    });

    $('#inputName').focusin(function (event) {
        $('#inputName').css({
            borderWidth: "0"
        });
        $('#reInputInfo6').text("");
    });

    $('#inputMeasurement').focusin(function (event) {
        $('#inputMeasurement').css({
            borderWidth: "0"
        });
        $('#reInputInfo7').text("");
    });

    $('#inputCost').focusin(function (event) {
        $('#inputCost').css({
            borderWidth: "0"
        });
        $('#reInputInfo8').text("");
    });
})();