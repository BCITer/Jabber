$(document).ready(function () {
    var width = $(window).width();
    var height = $(window).height()
    if ($(window).width() <= 768) {
        $('.dropdown').css({
            "display": "inline",
            "float": "left",
            "margin": "7px 10px 0px 4px",
            "position": "relative"
        });
        $('.dropdown-menu').css({
            "width": width,
            "left": "-90px",
            "height": "auto",
            "max-height": height,
            "overflow-x":"hidden"
        });
        $('.btn-circle').css({
            "border-radius":"50px"
        })        
        $('.dropdown > li.dropdown.open .dropdown-menu').css({
            "display": "list-item",
            "width": "300px", 
            "text-align": "center", 
            "left":"0", 
            "right": "0"  
        });        
        $('.dropdown-menu>li').css({
           
        })     
        $('.dropdown > li.dropdown.open').css({
            "position": "static"
        });
    } else {
        $('.dropdown').css({
            "display": "none"
        });
    }
    $(window).on('orientationchange', function (e) {
       location.reload();
    });

});