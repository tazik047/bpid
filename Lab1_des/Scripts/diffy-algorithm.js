(function($) {
	$('#send-my-key').click(function() {
		var key = $('#your-key').val();
		if (!isValidKey(key)) {
			return;
		}

		sendMessage('Секретный ключ собеседника: ' + prepareKey(+key));
	});

	$('#send-message').click(function () {
		if (isKeysValid()) {
			$('.bg-danger').addClass('invisible');
		} else {
			$('.bg-danger').removeClass('invisible');
			return;
		}
		var text = $('#message-body').val();
		var key = getKey();
		var module = $('#module-rsa-key').val();

		text = diffyEncryption(text, key, module, true);

		sendMessage(text);
	});

	$('body').on('mouseover', '.text-body', function () {
		if (isKeysValid()) {
			$('.bg-danger').addClass('invisible');
		} else {
			$('.bg-danger').removeClass('invisible');
			return;
		}
		$(this).text(diffyEncryption($(this).data('origin-text'), getKey(), $('#module-rsa-key').val()));
	}).on('mouseleave', '.text-body', function () {
		$(this).text($(this).data('origin-text'));
	});

	function getKey() {
		var your = +$('#your-key').val();
		var other = +$('#other-key').val();
		console.log(other * prepareKey(your));

		return other * prepareKey(your);
	}

	function prepareKey(key) {
		return modexp(3, key, 1987);
	}

	function isKeysValid() {
		var your = $('#your-key').val();
		var other = $('#other-key').val();

		return isValidKey(your) && isValidKey(other);
	}

	function isValidKey(key) {
		if (key == undefined || key == "") {
			return false;
		}

		return !isNaN(+key);
	}

	function diffyEncryption(text, key) {
		var res = "";
		for (var i = 0; i < text.length; i++) {
			var tCode = text[i].charCodeAt();
			var rCode = tCode ^ key;
			res += String.fromCharCode(rCode);
		}

		return res;
	}


})(jQuery);