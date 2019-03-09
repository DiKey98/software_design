//(function () {
//    'use strict';

//    $('.input-file').each(function () {
//        var $input = $(this),
//            $label = $input.next('.js-labelFile'),
//            labelVal = $label.html();

//        $input.on('change', function (element) {
//            var fileName = '';
//            if (element.target.value) fileName = element.target.value.split('\\').pop();
//            fileName
//                ? $label.addClass('has-file').find('.js-fileName').html(fileName)
//                : $label.removeClass('has-file').html(labelVal);
//        });
//    });
//})();

$(document).ready(function () {

    $('.form-control').css({
        borderWidth: "0"
    });

    
    $("#change").click(function (event) {
        event.preventDefault();

        var name = $('#inputName').val();
        var meas = $('#inputMeasurement').val();
        var cost = $('#inputCost').val();


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
            url: '/Services/ChangeAction',
            method: 'POST',
            data: {
                name: name,
                measurement: meas,
                costPerUnit: cost,
            },
            dataType: 'JSON',
            success: function (jsonData) {
                console.log(jsonData);
                if (jsonData.ok) {
                    window.location.replace("/");
                    return;
                }

                if (jsonData.message !== null && jsonData.message !== undefined) {
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

                    $('#reInputInfo6').text(jsonData.message).css({
                        color: 'red'
                    });

                    $('#reInputInfo7').text(jsonData.message).css({
                        color: 'red'
                    });

                    $('#reInputInfo8').text(jsonData.message).css({
                        color: 'red'
                    });

                }
            }
        });
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

});