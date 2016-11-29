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
    setPartial();
    SetLinksToDetail();
});

photoButton.click(function (e) {
    partial = "photo";
    photoButton.addClass('active');
    albumButton.removeClass('active');
    setPartial();
    SetLinksToDetail();
});

search.keyup(function (e) {
    setPartial();
    SetLinksToDetail();
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
            SetLinksToDetail();
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

    var app2 = new Vue({
        el: '#app2',
        data: {
            comments: {}
        }
    });

    var photo = {
        id: ""
    }

    photo.id = id;

        $.ajax({
            type: "GET",
            url: "/Photo/PhotoComments",
            data: photo,
            success: function (data) {
                app2.comments = data;
               
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
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
                    app2.comments = data;
                    
                    $.ajax({
                        type: "GET",
                        url: "/Photo/PhotoComments",
                        data: photo,
                        success: function (data) {
                            app2.comments = data;

                        },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    });

                },
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });


        }

    });
}

