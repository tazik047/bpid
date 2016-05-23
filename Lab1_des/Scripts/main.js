window.sendMessage = function sendMessage(message, callback) {
    var data = {
        text: escape(message),
        id: $('#recipient-email').val()
    };
    $.ajax({
        url: "/api/message/send",
        data: data,
        type: "POST",
        success: function() {
            document.renderMessage('#my-message-template', message);
            if (callback != undefined) {
                callback();
            }
        }
    });
};

window.simpleEncryption = function(text, key) {
    var res = "";
    for (var i = 0; i < text.length; i++) {
        var tCode = text[i].charCodeAt();
        var kCode = key[i % key.length].charCodeAt();
        var rCode = tCode ^ kCode;
        res += String.fromCharCode(rCode);
    }

    return res;
}

document.renderMessage = function (templateSelector, message) {
    var template = $(templateSelector).clone();
    template.find('.text-body').text(message);
    template.find('.text-body').attr('data-origin-text', message);
    template.children().appendTo($('.chat-box-main'));
    $('#message-body').val('');
    $('#send-message').removeClass('invisible');
    $('.chat-box-main').scrollTo('.hr-clas:last', 1000);
}

$("body").on('click', '.hide-btn', function() {
    var id = $(this).data('hide-element-id');
    $('#' + id).toggleClass('invisible');
});

$(document).ready(function () {

    $('.chat-box-main .text-body').each(function () {
        var $el = $(this);
        $el.attr('data-origin-text', unescape($el.attr('data-origin-text')));
        $el.text(unescape($el.text()));
    });

    $('.chat-box-main').scrollTo('.hr-clas:last');
});