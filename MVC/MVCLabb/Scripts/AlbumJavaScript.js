


function GoToAlbum(){
$(".albumButton").each(function (e) {
    
    var button = $(this);
    var model = {
        id: ""
    };

    model.id = this.getAttribute("data-imageId");
    button.click(function () {
  
        $.ajax({
            type: "GET",
            url: "/Photo/AlbumDetails",
            data: model,
            success: function (data) {
                content.html(data);
                AddPhotoes(model.id);
                LoadComent(model.id);
                
            },
            contentType: "application/json; charset=utf-8",
            dataType: "text"
        });

    });
});
}

function AddPhotoes(id) {

    var album = {
        id: ""
    }

    album.id = id;

    $.ajax({
        type: "GET",
        url: "/Photo/AlbumPhotoes",
        data: album,
        success: function (data) {
            $("#masonarycontainer").html(data);
            whileOverfloed();
            SetLinksToDetail();
        },
        contentType: "application/json; charset=utf-8",
        dataType: "text"
    });


}

function LoadComent(id) {
    
    $("#inputField").hide();
    $("#closeButton").hide();

    var app = new Vue({
        el: '#app',
        data: {
            comments: {}
        }
    });

    var album = {
        id: ""
    }

    album.id = id;

    $("#closeButton").click(function (e) {
        $("#inputField").hide();
        $("#list").hide();
        $("#commentButton").show();
        $("#closeButton").hide();

    });


    $("#commentButton").click(function (e) {
        $("#closeButton").show();
        $("#commentButton").hide();
        $("#inputField").show();
        $("#list").show();

        $.ajax({
            type: "GET",
            url: "/Photo/AlbumComments",
            data: album,
            success: function (data) {
                $("#inputField").show();
                app.comments = data;
                console.log(data.id);
               


            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });

    });

    $("#submit").click(function (e) {

        var comm = {
            id: "",
            comment: ""
        }
        comm.id = id;

        comm.comment = $("#actualComment").val()

        if ($("#actualComment").val().length > 1) {

            $.ajax({
                type: "POST",
                url: "/Photo/AlbumComments",
                data: JSON.stringify(comm),
                success: function (data) {
                    $("#inputField").show();
                    app.comments = data;
                    console.log(data.id);
                    $("#actualComment").val("");
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });


        }

    });
}






