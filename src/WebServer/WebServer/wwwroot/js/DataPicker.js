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
            url: '/Admin/Orders',
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
                <th data-field="id" data-filter-control="input">ID</th>
                <th data-field="user" data-filter-control="input">Клиент</th>
                <th data-field="service" data-filter-control="input">Услуга</th>
                <th data-field="cost" data-filter-control="input">Стоимость</th>
                <th data-field="date" data-filter-control="input">Дата</th>
                <th data-field="paid" data-filter-control="input">Оплачен</th>
            </tr>
        </thead>
        <tbody id="table-content">
        </tbody>
    </table>`);




                for (var i = 0; i < jsonData.length; i++) {
                    var order = jsonData[i];
                    var date = moment(order.orderDate).format('DD.MM.YYYY');
                    var paid = "Да";
                    if (!order.isPaid) {
                        paid = "Нет"
                    }

                    $('#table-content').append(`
                    <tr>
                        <td>${order.id}</td>
                        <td>${order.user.fio}</td>
                        <td>${order.service.name}</td>
                        <td>${order.cost} руб.</td>
                        <td>${date}</td>
                        <td>${paid}</td>
                    </tr>`);
                }

                $('#table').bootstrapTable();
            }
        });
    });  
});
