var content = $("#content");
var search = $("#search");
var partial = "photo";

var photoButton = $("#photoButton");
var albumButton = $("#albumButton");

photoButton.addClass('active');
SetLinksToDetail();
albumButton.click(function (e) {
    partial = "album";
    albumButton.addClass('active');
    photoButton.removeClass('active');
    setPartial()
});

photoButton.click(function (e) {
    partial = "photo";
    photoButton.addClass('active');
    albumButton.removeClass('active');
    SetLinksToDetail();
    setPartial()
});

search.keyup(function (e) {
    setPartial();
});

var model = {
    Search: "",
    Filter: ""
};



function setPartial() {
    
    model.Search = search.val();
    model.Filter = partial; 
    $.ajax({
        type: "GET",
        url: "/Home/Media",
        data: model,
        success: function (data) {
            content.html("");
            $("#masonarycontainer").html(data);
            GoToAlbum();
            
            whileOverfloed();
        },
        contentType: "application/json; charset=utf-8",
        dataType: "text"
    });
    
    
}

function SetLinksToDetail() {

    $(".detailLink").each(function (e) {
        $(this).click(function (e) {

     

            var model = {
                id: ""
            }

            model.id = this.getAttribute("data-imageID");

            $.ajax({
                type: "GET",
                url: "/Photo/Details",
                data: model,
                success: function (data) {
                    content.html(data);
                    $("#masonarycontainer").html("");
                    whileOverfloed();
                    LoadComentPhoto(model.id);
                },
                contentType: "application/json; charset=utf-8",
                dataType: "text"
            });




        });

    });

}

function LoadComentPhoto(id) {

    var app = new Vue({
        el: '#app',
        data: {
            comments: {}
        }
    });

    var photo = {
        id: ""
    }

    photo.id = id;


    $("#commentButton").click(function (e) {

        $.ajax({
            type: "GET",
            url: "/Photo/PhotoComments",
            data: photo,
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
                url: "/Photo/PhotoComments",
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

