$(document).ready(function () {
    var $container = $('.nav .row');
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
    // ANIMATEDLY DISPLAY THE NOTIFICATION COUNTER.
    $('#noti_Counter')
        .css({ opacity: 0 })
        .text('7')              // ADD DYNAMIC VALUE -- YOU CAN EXTRACT DATA FROM DATABASE OR XML.
        .css({ top: '-10px' })
        .animate({ top: '-2px', opacity: 1 }, 500);

    $('#noti_Button').click(function () {

        // TOGGLE (SHOW OR HIDE) NOTIFICATION WINDOW.
        $('#notifications').fadeToggle('fast', 'linear', function () {
            if ($('#notifications').is(':hidden')) {
                $('#noti_Button').css('background-color', '#2E467C');
            }
            else $('#noti_Button').css('background-color', '#FFF');        // CHANGE BACKGROUND COLOR OF THE BUTTON.
        });

        $('#noti_Counter').fadeOut('slow');                 // HIDE THE COUNTER.

        return false;
    });

    // HIDE NOTIFICATIONS WHEN CLICKED ANYWHERE ON THE PAGE.
    $(document).click(function () {
        $('#notifications').hide();

        // CHECK IF NOTIFICATION COUNTER IS HIDDEN.
        if ($('#noti_Counter').is(':hidden')) {
            // CHANGE BACKGROUND COLOR OF THE BUTTON.
            $('#noti_Button').css('background-color', '#2E467C');
        }
    });

    $('#notifications').click(function () {
        return false;       // DO NOTHING WHEN CONTAINER IS CLICKED.
    });
});