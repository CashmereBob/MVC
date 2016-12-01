$(window).on('load', function () {
    whileOverfloed();
});

$(document).resize(function () {
    whileOverfloed();
});

$(window).resize(function () {
    whileOverfloed();
});


$(document).ready(function () {
    whileOverfloed();
});


var container = $('#masonarycontainer');


function isOverflowed() {
    
    var overflow = false;

    container.children('.masonaryitem').each(function () {
        if (container[0].clientWidth < $(this).position().left || container[0].clientHeight < $(this).position().top) {
            overflow = true;
        }
    });
    return overflow;
}

function whileOverfloed() {
    
    container.height("40vh");
    while (isOverflowed() === true) {
            container.height(container.height() + 200);
            
        }   
}