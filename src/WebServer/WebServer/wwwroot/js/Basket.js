
    $(window).ready(function () {
        $('.form-control').css({
            borderWidth: "0"
        });

        for (var i = 0; i < window.ordersCount; i++) {
            $(`#confim${i}`).click(function (event) {
                event.preventDefault();
                var columnId = `#${$(this).data("columnid")}`;
                $.ajax({
                    url: '/Services/Buy',
                    method: 'POST',
                    data: {
                        id: $(this).data("id")
                    },
                    dataType: 'JSON',
                    success: function (jsonData) {
                        if (jsonData.ok) {
                            alert("Услуга оплачена");
                            $(columnId).remove();
                            return;
                        }
                    }
                });

            });

            $(`#deny${i}`).click(function (event) {
                event.preventDefault();
                var columnId = `#${$(this).data("columnid")}`;
                $.ajax({
                    url: '/Services/Cancel',
                    method: 'POST',
                    data: {
                        id: $(this).data("id")
                    },
                    dataType: 'JSON',
                    success: function (jsonData) {
                        if (jsonData.ok) {
                            $(columnId).remove();
                            return;
                        }
                    }
                });

            });
        }
    });