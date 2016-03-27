(function ($) {
    var notificationsHub;

    $(function () {
        if (Notification.permission === "default") {
            Notification.requestPermission();
        }
        $("body").on("click", "[data-href]", function () {
            var href = $(this).data("href");
            if (href !== "") {
                location.href = href;
            }
        });
    });

    notificationsHub = $.connection.notificationsHub;
    notificationsHub.client.notify = notify;

    $.connection.hub.start();

    $("#notifications-list").on("click", ".close-notification", function (e) {
        var $this = $(this),
            $notification = $this.closest(".notification");

        e.stopPropagation();

        $notification.fadeOut();
        setTimeout(function () {
            $notification.remove();
        }, 400);
    });

    $("#notifications-list").on("mouseenter", ".notification", function () {
        clearTimeout($(this).data("timeout-id"));
    });

    $("#notifications-list").on("mouseleave", ".notification", function () {
        var $this = $(this);
        var timeoutId = setTimeout(function () {
            $this.fadeOut();
            setTimeout(function () {
                $this.remove();
            }, 400);
        }, 5000);
        $this.data("timeout-id", timeoutId);
    });

    function notify(data) {
        var $container = $("#notifications-list"),
            sounds = [
                new Audio("/sounds/notification.mp3")
            ];
        data.OriginText = unescape(data.OriginText);
        if (data.RedirectLink == "") {
            data.RedirectLink = $container.data("base-href") + "#" + data.id;
        }
        if ($('#recipient-email').val() == data.FromId && !document.hidden) {
            document.renderMessage('#recipient-message-template', data.OriginText);
            return;
        }

        var template = $('#notification-template').clone();
        template.find('.notification').attr('data-href', data.RedirectLink);
        template.find('.notification-header').text(data.Header);
        template.find('.notification-body').text(data.Text);
        renderTemplate(template.children(), sounds, $container);
    }

    function renderTemplate(rendered, sounds, $container) {
                rendered.css("display", "none");
                $container.append(rendered);

                sounds[0].play();

                if (document.hidden) {
                    var notification = new Notification(data.Header, { body: data.Text, sound: "/sounds/notification.mp3" });
                    if (data.RedirectLink) {
                        notification.onclick = window.open(data.RedirectLink, "_blank");
                    }
                }

                rendered.fadeIn();

                var timeoutId = setTimeout(function () {
                    rendered.fadeOut();
                    setTimeout(function () {
                        rendered.remove();
                    }, 400);
                }, 5000);

                rendered.data("timeout-id", timeoutId);
            }
})(jQuery);