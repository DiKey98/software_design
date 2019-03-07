$(window).ready(function () {
    $('.form-control').css({
        borderWidth: "0"
    });

    $('#datepicker').daterangepicker({
        "locale": {
            "format": "DD.MM.YYYY",
            "separator": " - ",
            "applyLabel": "Выбрать",
            "cancelLabel": "Отменить",
            "fromLabel": "От",
            "toLabel": "До",
            "customRangeLabel": "Custom",
            "daysOfWeek": [
                "Пн",
                "Вт",
                "Ср",
                "Чт",
                "Пт",
                "Сб",
                "Вс"
            ],
            "monthNames": [
                "Январь",
                "Февраль",
                "Март",
                "Апрель",
                "Май",
                "Июнь",
                "Июль",
                "Август",
                "Сентябрь",
                "Октябрь",
                "Ноябрь",
                "Декабрь"
            ],
            "firstDay": 0
        }
    }, function (start, end, label) {
        $.ajax({
            url: '/Manager/UsersActivity',
            method: 'POST',
            data: {
                start: start.format("DD.MM.YYYY"),
                end: end.format("DD.MM.YYYY")
            },
            dataType: 'JSON',
            success: function (jsonData) {
                $('.bootstrap-table').remove();

                $('#datepicker').after(`
<table id="table"
           data-filter-control="true"
           data-filter-show-clear="true"
           class="table table table-scroll">
        <thead>
            <tr>
                <th data-field="user" data-filter-control="input">Клиент</th>
                <th data-field="service" data-filter-control="input">Всего заказов</th>
                <th data-field="cost" data-filter-control="input">Оплаченных заказов</th>
                <th data-field="date" data-filter-control="input">Неоплаченных заказов</th>
                <th data-field="paid" data-filter-control="input">Общая стоимость заказов</th>
            </tr>
        </thead>
        <tbody id="table-content">
        </tbody>
    </table>`);



                for (var i = 0; i < jsonData.length; i++) {
                    var user = jsonData[i];
                    
                    $('#table-content').append(`
                    <tr>
                        <td>${user.userName}</td>
                        <td>${user.ordersCount}</td>
                        <td>${user.paidOrdersCount}</td> 
                        <td>${user.unPaidOrdersCount}</td>
                        <td>${user.ordersCost} руб.</td> 
                    </tr>`);
                }
                $('#table').bootstrapTable();
            }
        });
    });
});
