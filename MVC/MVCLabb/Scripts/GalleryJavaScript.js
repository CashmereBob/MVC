var content = $("#content");
var search = $("#search");
var partial = "photo";

var photoButton = $("#photoButton");
var albumButton = $("#albumButton");

photoButton.addClass('active');

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
