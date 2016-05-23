(function($) {
    $('#get-secrete-key').click(function() {
        $.get('/api/keys/simple').done(function(data) {
            var key = data.key;
            $('#simple-key').val(key);
        });
    });

    function isKeyValid() {
        var key = $('#simple-key').val();

        return !(key === "" || key == undefined);
    }

    $('body').on('mouseover', '.text-body', function () {
        if (isKeyValid()) {
            $('.bg-danger').addClass('invisible');
        } else {
            $('.bg-danger').removeClass('invisible');
            return;
        }
        $(this).text(simpleEncryption($(this).data('origin-text'), $('#simple-key').val()));
    }).on('mouseleave', '.text-body', function () {
        $(this).text($(this).data('origin-text'));
    });

    $('#send-message').click(function() {
        var text = $('#message-body').val();
        var key = $('#simple-key').val();

        text = simpleEncryption(text, key);

        sendMessage(text);
    });

})(jQuery);