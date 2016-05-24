(function($) {
	$('#get-secrete-key').click(function () {
		$.get('/api/keys/rsa').done(function (data) {
			$('#secrete-rsa-key').val(data.secreteKey);
			$('#public-rsa-key').val(data.publicKey);
			$('#module-rsa-key').val(data.module);
		});
	});

	$('#send-public-key').click(function() {
		if ($('#public-rsa-key').val() && $('#public-rsa-key').val() != "") {
			sendMessage('Мой открытый ключ: ' + $('#public-rsa-key').val() + ' и модуль: '+ $('#module-rsa-key').val());
		}
	});



	function isKeyValid() {
		var key = $('#secrete-rsa-key').val();
		var module = $('#module-rsa-key').val();

		return !(key === "" || key == undefined || module === "" || module == undefined);
	}

	$('#send-message').click(function() {
		if (isKeyValid()) {
			$('.bg-danger').addClass('invisible');
		} else {
			$('.bg-danger').removeClass('invisible');
			return;
		}
		var text = $('#message-body').val();
		var key = $('#secrete-rsa-key').val();
		var module = $('#module-rsa-key').val();

		text = rsaEncrypt(text, key, module, true);

		sendMessage(text);
	});

	$('body').on('mouseover', '.text-body', function () {
		if (isKeyValid()) {
			$('.bg-danger').addClass('invisible');
		} else {
			$('.bg-danger').removeClass('invisible');
			return;
		}
		$(this).text(rsaEncrypt($(this).data('origin-text'), $('#secrete-rsa-key').val(), $('#module-rsa-key').val()));
	}).on('mouseleave', '.text-body', function () {
		$(this).text($(this).data('origin-text'));
	});

	function rsaEncrypt(text, key, module, isEncode) {
		key = +key;
		module = +module;
		var k = key;//ключ
		var resText = [];
		for (var i = 0; i < text.length; i++){//для каждого значения в массиве
			var res = 1;
			var temp = text[i].charCodeAt();
			/*while (k > 0){//шифруем
				if (k & 1) {
					res = (res * temp) % module;
				}
				temp = (temp * temp) % module;
				k >>= 1;
			}*/
			/*for (var j = 0; j < k; j++) {
				res = (res * temp) % module;
			}*/
			if (isEncode) {
				var code = modexp(temp, key, module);
				var codePart = Math.floor(code / 2);
				resText.push(codePart);
				resText.push(code - codePart);
			} else {
				var codeDecode = modexp(temp + text[++i].charCodeAt(), key, module);
				resText.push(codeDecode);
			}
			k = key;
		}

		return toString(resText);
	}

	function toString(arr) {
		var res = "";

		for (var i = 0; i < arr.length; i++) {
			res += String.fromCharCode(arr[i]);
		}

		return res;
	}

})(jQuery);