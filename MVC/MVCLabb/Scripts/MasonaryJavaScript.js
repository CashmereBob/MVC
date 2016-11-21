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
            console.log("hej");
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
}