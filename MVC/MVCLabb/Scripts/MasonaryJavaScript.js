$(window).load(function () {
    console.log($('#masonarycontainer').height());
    whileOverfloed();

});


$(window).resize(function () {
    console.log($('#masonarycontainer').height());
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

    $('#masonarycontainer').height("80vh");
    while (isOverflowed() === true) {
        $('#masonarycontainer').height($('#masonarycontainer').height() + 50);
    }

    console.log($('#masonarycontainer').height());
}