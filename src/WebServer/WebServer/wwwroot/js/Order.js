$(document).ready(function () {
    var maxValue = 100;

    var count = parseInt($('#counter').val());
    if (count <= maxValue) {
        $('#price').text(`Цена: ${window.data.CostPerUnit * count} руб.`);
    } else {
        $('#price').text(`Цена: ${window.data.CostPerUnit} руб.`);
    }

    $('#confim').click(function (e) {
        e.preventDefault();
        var count = parseInt($('#counter').val());
        $.ajax({
            url: '/Services/OrderAction',
            method: 'POST',
            data: {
                id: window.data.Id,
                units: count
            },
            success: function (jsonData) {
                if (jsonData.message === "NO_AUTHORIZED") {
                    window.location.replace("/Home/Autorisation");
                    return;
                }

                if (jsonData.ok) {
                    alert("Заказ успешно оформлен");
                    window.location.replace("/Home/Services");
                }

            }
        });
    });

   
    $(document).keypress(function (e) {
        if (e.keyCode === 13) {
            var count = parseInt($('#counter').val());
            if (count <= maxValue) {
                $('#price').text(`Цена: ${window.data.CostPerUnit * count} руб.`)
            } else {
                $('#price').text(`Цена: ${window.data.CostPerUnit} руб.`)
            }
        }
    });

    $(".btn-number").click(function (e) {
        e.preventDefault();

        var fieldName = $(this).attr("data-field");
        var type = $(this).attr("data-type");
        var input = $("input[name='" + fieldName + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if (type === "minus") {

                if (currentVal > input.attr("min")) {
                    input.val(currentVal - 1).change();
                }
                if (parseInt(input.val()) === input.attr("min")) {
                    $(this).attr("disabled", true);
                }

            } else if (type === "plus") {

                if (currentVal < input.attr("max")) {
                    input.val(currentVal + 1).change();
                }
                if (parseInt(input.val()) === input.attr("max")) {
                    $(this).attr("disabled", true);
                }

            }
        } else {
            input.val(0);
        }

        var count = parseInt($('#counter').val());
        if (count <= maxValue) {
            $('#price').text(`Цена: ${window.data.CostPerUnit * count} руб.`)
        } else {
            $('#price').text(`Цена: ${window.data.CostPerUnit} руб.`)
        }
    });

    $(".input-number").focusin(function () {
        $(this).data("oldValue", $(this).val());
    });

    $(".input-number").change(function () {

        var minValue = parseInt($(this).attr("min"));
        var maxValue = parseInt($(this).attr("max"));
        var valueCurrent = parseInt($(this).val());

        var name = $(this).attr("name");
        if (valueCurrent >= minValue) {
            $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr("disabled");
        } else {
            $(this).val($(this).data("oldValue"));
        }
        if (valueCurrent <= maxValue) {
            $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr("disabled");
        } else {
            $(this).val($(this).data("oldValue"));
        }
    });

    $(".input-number").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
            (e.keyCode === 65 && e.ctrlKey === true) ||
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }

        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});



