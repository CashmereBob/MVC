$(window).load(function () {
    whileOverfloed();
});

$(document).resize(function () {
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

    $('#masonarycontainer').height(500);
    while (isOverflowed() === true) {
        $('#masonarycontainer').height($('#masonarycontainer').height() + 50);
    }

}