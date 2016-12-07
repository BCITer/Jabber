//var safeColors = ['00', '33', '66', '99', 'cc', 'ff'];
var counter = 1;
var rand = function () {
    //return Math.floor((Math.random() * 3) +1);
    counter++;
    if (counter == 4) {
        counter = 0;
        
    }
    return counter
};

var randomColor = function colors() {
    var color;
    switch (rand()) {
        case (0):
            color = "#3d4d6b";
            break;
        case (1):
            color = "#698951";
            break;
        case (2):
            color = "#991637";
            break;
        case (3):
            color = "#714089";
            break; 
    }
    return color;
};

var backColor = function bcolors() {
    var color;
    switch (rand()) {
        case (0):
            color = "#a8a8a8";
            break;
        case (1):
            color = "#D3D3D3";
            break;
        case (2):
            color = "#bdbdbd";
            break;
        case (3):
            color = "#d7d7d7";
            break;
    }
    return color;
};
$(document).ready(function() {
    $('.comment').each(function colors() {
        $(this).css("border-color", randomColor);
    });
    $('.comment').each(function bcolors() {
        $(this).css("background-color", backColor);
    });
});