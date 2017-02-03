
$(function () {
    //Delete

    $(document).on("click", ".deleteCredit", function () {
        var id = $(this).data("id");
        $.ajax({
            url: "/Credit/Delete",
            type: "POST",
            data: { id: id },
            success: function (data) {
                $("#credit" + data).remove();
            }
        });
    });


    //Edit

    $(document).on("click", ".editCredit", function () {
        var id = $(this).data("id");
        $.ajax({
            url: "/Credit/Edit",
            type: "POST",
            data: { id: id },
            success: function (data) {
                $("#credit" + id).html(data);
            }

        });
    });


    $(document).on("click", ".cancelEditCredit", function (event) {
        var id = $(this).data("id");
        $.ajax({
            url: "/Credit/CancelEdit",
            type: "POST",
            data: { id: id },
            success: function (data) {
                $("#credit" + id).html(data);
            }

        });
    });

    $(document).on("click", ".okEditCredit", function (event) {
        var id = $(this).data("id");
        var ammount = $("#ammount" + id).val();
        var percent = $("#percent" + id).val();
        var description = $("#description" + id).val();
        var dayOfCredit = $("#dayOfCredit" + id).val();
        var clientId = $("#clientId" + id).val();
        $.ajax({
            url: "/Credit/OkEdit",
            type: "POST",
            datatype: "json",
            data: { id: id, Ammount: ammount, Percent: percent, Description:description, DayOfCredit:dayOfCredit, ClientId:clientId},
            success: function (data) {
                $("#credit" + id).html(data);
            }

        });
    });


    //Add   

    $(document).on("click", "#addCreditButton", function () {

        $.ajax({
            url: "/Credit/Add",
            type: "POST",
            success: function (data) {
                if ($("#addCreditButton").is(":hidden")) {
                    return false;
                }
                else {
                    $("#addCreditButton").hide();
                    $("#addRow").append(data);
                }

            }

        });

    });

    $(document).on("click", ".okAddCredit", function () {

        var ammount = $("#Ammount").val();
        var percent = $("#Percent").val();
        var description = $("#Description").val();
        var dayOfCredit = $("#DayOfCredit").val();
        var clientId = $("#ClientId").val();
        $.ajax({
            url: "/Credit/ConfirmAdd",
            type: "POST",
            datatype: "json",
            data: { Ammount: ammount, Percent: percent, Description: description, DayOfCredit: dayOfCredit, ClientId: clientId },
            success: function (data) {
                $("#addTable").remove();
                $("#addCreditButton").show();
                $("#myTable").append("<tr>" + data + "</tr>");
            }
        });

    });

    $(document).on("click", ".cancelAddCredit", function () {
        $("#addTable").remove();
        $("#addCreditButton").show();
    });

});



