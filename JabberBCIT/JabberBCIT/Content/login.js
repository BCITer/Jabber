$(document).ready(function () {
    var $container = $('.nav .row');
    var isDesktop = (function () {
        return !('ontouchstart' in window) // works on most browsers 
        || !('onmsgesturechange' in window); // works on ie10
    })();
    window.isDesktop = isDesktop;
    if ($(window).width() > 768) {
        $('#loginLink').on('mouseover', function () {
            $container.stop(true).fadeIn(100);
        }).on('mouseleave', function () {
            $container.stop(true).delay(100).fadeOut(100);
        });

        $container.on('mouseenter', function () {
            $(this).stop(true).clearQueue().show();
        }).on('mouseleave', function () {
            $(this).delay(200).fadeOut(100);
        });
    }
});