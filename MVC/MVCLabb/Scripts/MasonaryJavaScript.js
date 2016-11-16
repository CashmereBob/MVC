$(window).load(function () {
    whileOverfloed();

});


$(window).resize(function () {
    whileOverfloed();
});


function isOverflowed() {
    var overflow = false;
    $('#masonarycontainer').children('.masonaryitem').each(function () {
        if ($('#masonarycontainer')[0].clientWidth < $(this).position().left) {
            overflow = true;
        }
    });
    return overflow;
}
function whileOverfloed() {

    console.log("hepp");
    $('#masonarycontainer').height(1);
    while (isOverflowed() === true) {
        $('#masonarycontainer').height($('#masonarycontainer').height() + 50);
    }

}