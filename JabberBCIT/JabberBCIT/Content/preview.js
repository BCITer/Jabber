$(document).ready(function () {
    $('#Avatar').on("change", function () {
        var file = this.files[0];

        if (this.files && file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#profile-picture').attr('src', e.target.result);
                $('#fn').text(file.name);
                $('#fs').text(file.size + " bytes");
            }
            reader.readAsDataURL(file);
        }
    });
});