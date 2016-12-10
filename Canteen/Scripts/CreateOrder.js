$(document).ready(function () {
    $("select").click(function () {

        var selectedIds = $("select").val();

        $.ajax({
            type: "POST",
            url: "/Orders/GetOrderPrice",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(selectedIds)
        }).done(function (data) {
            $("#price").val(data.price);
        });
    });
});