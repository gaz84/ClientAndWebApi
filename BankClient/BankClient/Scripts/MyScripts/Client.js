
$(function () {
    //Delete

    $(document).on("click", ".deleteClient", function () {
        var id = $(this).data("id");
        $.ajax({
            url: "/Client/Delete",
            type: "POST",
            data: { id: id },
            success: function (data) {
                $("#client" + data).remove();
            }
        });
    });


    //Edit

    $(document).on("click", ".editClient", function () {
        var id = $(this).data("id");
        $.ajax({
            url: "/Client/Edit",
            type: "POST",
            data: { id: id },
            success: function (data) {
                $("#client" + id).html(data);
            }

        });
    });


    $(document).on("click", ".cancelEditCat", function (event) {
        var id = $(this).data("id");
        $.ajax({
            url: "/Client/CancelEdit",
            type: "POST",
            data: { id: id },
            success: function (data) {
                $("#client" + id).html(data);
            }

        });
    });

    $(document).on("click", ".okEditCat", function (event) {
        var id = $(this).data("id");
        var name = $("#name" + id).val();
        var surname = $("#surname" + id).val();
        $.ajax({
            url: "/Client/OkEdit",
            type: "POST",
            datatype: "json",
            data: { id: id, Name: name, Surname: surname },
            success: function (data) {
                $("#client" + id).html(data);
            }

        });
    });


    //Add   

    $(document).on("click", "#addClientButton", function () {

        $.ajax({
            url: "/Client/Add",
            type: "POST",
            success: function (data) {
                if ($("#addClientButton").is(":hidden")) {
                    return false;
                }
                else {
                    $("#addClientButton").hide();
                    $("#addRow").append(data);
                }

            }

        });

    });

    $(document).on("click", ".okAddCat", function () {

        var name = $("#Name").val();
        var surname = $("#Surname").val();
        $.ajax({
            url: "/Client/ConfirmAdd",
            type: "POST",
            datatype: "json",
            data: { Name: name, Surname: surname },
            success: function (data) {
                    $("#addTable").remove();
                    $("#addClientButton").show();
                    $("#myTable").append("<tr>" + data + "</tr>");
                }
        });

    });

    $(document).on("click", ".cancelAddCat", function () {
        $("#addTable").remove();
        $("#addClientButton").show();
    });

});





//$(document).on("click", ".okEditCat", function (event) {
//    var id = $(this).data("id");
//    var name = $("#name" + id).val();
//    $.ajax({
//        url: "http://localhost:8889/api/category",
//        headers: {"Authorization": "Bearer " + $('#myToken').val()},
//        type: "PUT",
//        datatype: "json",
//        data: { id: id, Name: name},
//        success: function (data) {
//            $(event.target).parent().parent().html(data);
//        }

//    });
//});
//$(document).on("click", ".deleteCategory", function () {
//    var id = $(this).data("id");
//    var token=$('#myToken').val();
//    $.ajax({
//        url: "http://localhost:8889/api/category",
//        type: "DELETE",
//        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + token); },
//        data: { id: id },
//        success: function (data) {

//            $("#category" + id).remove();

//        },

//        error: function (ts) { alert("EROR") }

//    });

//});