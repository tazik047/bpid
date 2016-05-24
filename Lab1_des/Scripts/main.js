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

window.strToArray = function (text, base) {
	var mas = [];
	var i = 0;
	for (var g = 0; g < text.length; g++) {
		var j = Math.floor(i / base);
		if (mas[j] == undefined) {
			mas[j] = [];
		}

		var charCode = text.charCodeAt(g);
		var bytes = dec2bin((charCode & 0xFF00) >> 8);

		for (var k = 0; k < 8; k++) {
			mas[j][i % base] = bytes[k];
			i++;
		}

		bytes = dec2bin(charCode & 0xFF);

		for (var k2 = 0; k2 < 8; k2++) {
			mas[j][i % base] = bytes[k2];
			i++;
		}

	}
	while (i % base !== 0) {
		mas[Math.floor(i / base)][i % base] = 0;
		i++;
	}

	return mas;
};

window.dec2bin = function (num) {
	var res = [0, 0, 0, 0, 0, 0, 0, 0];
	var i = 7;
	while (num !== 0) {
		res[i--] = num % 2;
		num = num >> 1;
	}

	return res;
}

window.bin2dec = function (binArr, start, length) {
	var res = 0;
	for (var i = 0; i < length; i++) {
		res = (res << 1) | binArr[start + i];
	}

	return res;
}

window.arrayToStr = function (arr) {
	var mas = [];
	var i = 0;
	for (var j = 0; j < arr.length; j++) {
		for (var k = 0; k < arr[j].length; k += 16) {
			mas[i++] = bin2dec(arr[j], k, 16);
		}
	}

	return String.fromCharCode.apply(null, mas);
};

// x^y mod N
window.modexp = function(x, y, n) {
		if (y === 0) return 1;
		var z = modexp(x, Math.floor(y / 2), n);
		if (y % 2 == 0)
			return (z * z) % n;
		else
			return (x * z * z) % n;
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