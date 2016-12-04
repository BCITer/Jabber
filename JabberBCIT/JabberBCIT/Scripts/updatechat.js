$(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#discussion').append('<div>' + '<span>' + htmlEncode(name)
            + ': ' + htmlEncode(message) + '</span>' + '</div>');
        $('#discussion').animate({ scrollTop: $('#discussion').prop("scrollHeight") }, 10);
    };
    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            chat.server.send($('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
