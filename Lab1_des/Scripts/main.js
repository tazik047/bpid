(function ($) {

    var sboxes = [
            [14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7, 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8, 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0, 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13],
            [15, 1, 8, 14, 6, 11, 3, 2, 9, 7, 2, 13, 12, 0, 5, 10, 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5, 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15, 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9],
            [10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8, 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1, 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7, 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12],
            [7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15, 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9, 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4, 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14],
            [2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9, 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6, 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14, 11, 8, 12, 7, 1, 14, 2, 12, 6, 15, 0, 9, 10, 4, 5, 3],
            [12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11, 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8, 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6, 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13],
            [4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1, 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6, 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2, 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12],
            [13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7, 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2, 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8, 2, 1, 14, 7, 4, 10, 18, 13, 15, 12, 9, 0, 3, 5, 6, 11]
    ];

    var fPermutation = [16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25];

    // key permutation at round i 56 => 48
    var pc2 = [14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32];

    // key shift for each round
    var keyShift = [1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1];

    var ip = [58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36,
        28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32,
        24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19,
        11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7];

    var invIp = [40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47,
        15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13,
        53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51,
        19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17,
        57, 25];

    $(document).ready(function () {
        $('#send-message').click(function () {
            if (isKeyValid()) {
                $('.bg-danger').addClass('invisible');
            } else {
                $('.bg-danger').removeClass('invisible');
                return;
            }
            if ($('#message-body').val().trim() == '') {
                return;
            }
            $('#send-message').addClass('invisible');
            var encryptedText = desEncryption($('#message-body').val().trim());
            sendMessage(encryptedText);
        });

        $('.chat-box-main .text-body').each(function () {
            var $el = $(this);
            $el.attr('data-origin-text', unescape($el.attr('data-origin-text')));
            $el.text(unescape($el.text()));
        });

        $('.chat-box-main').scrollTo('.hr-clas:last');

        $('#toggle-key').click(function () {
            $('#des-key').toggleClass('invisible');
        });

        document.renderMessage = function (templateSelector, message) {
            var template = $(templateSelector).clone();
            template.find('.text-body').text(message);
            template.find('.text-body').attr('data-origin-text', message);
            template.children().appendTo($('.chat-box-main'));
            $('#message-body').val('');
            $('#send-message').removeClass('invisible');
            $('.chat-box-main').scrollTo('.hr-clas:last', 1000);
        }

        $('body').on('mouseover', '.text-body', function () {
            if (isKeyValid()) {
                $('.bg-danger').addClass('invisible');
            } else {
                $('.bg-danger').removeClass('invisible');
                return;
            }
            $(this).text(desDecryption($(this).data('origin-text')));
        }).on('mouseleave', '.text-body', function () {
            $(this).text($(this).data('origin-text'));
        });
    });

    function isKeyValid() {
        var number = $('#des-key').val();
        var re = /^(([\w\d]{7}))$/gi;
        return re.test(number);
    }

    function testGenArray(max) {
        var arr = [];
        for (var i = 0; i < max; i++) {
            arr[i] = i + 1;
        }
        return arr;
    }

    function testShowTable(arr, base) {
        var res = '';
        for (var j = 0; j < arr.length; j++) {
            if (j % base === 0 && j !== 0) {
                res += '</tr><tr>';
            }
            res += '<td>' + arr[j] + ' </td>';
        }
        $('body').append('<table class="table"><tr>' + res + '</tr></table>');
    }

    function sendMessage(message) {
        var data = {
            text: escape(message),
            id: $('#recipient-email').val()
        };
        $.ajax({
            url: "/api/message/send",
            data: data,
            type: "POST",
            success: function () {
                document.renderMessage('#my-message-template', message);
            }
        });
    }

    function desEncryption(message) {
        var key = $('#des-key').val();
        var keyArray = keyToArray(key);
        var array = strToArray(message, 64);

        var i = 0;
        for (i = 0; i < array.length; i++) {
            array[i] = step1SwapArray(array[i]);
        }

        var splittedKey = splitArray(keyArray, 28);
        for (i = 0; i < array.length; i++) {
            var splittedData = splitArray(array[i], 32);
            var itData = {
                left: splittedData[0],
                right: splittedData[1],
                c: splittedKey[0],
                d: splittedKey[1],
                i: 0
            };
            for (var j = 0; j < 16; j++) {
                itData = iteration(itData);
                itData.i++;
            }

            array[i] = concatArrays(itData.left, itData.right);
        }

        for (i = 0; i < array.length; i++) {
            array[i] = lastStepPermutation(array[i]);
        }

        return arrayToStr(array);
    }

    function step1SwapArray(array) {
        return permutation(array, ip);
    }

    function iteration(iterationData) {
        var extendedRight = extend(iterationData.right);
        var key = generateKey(iterationData.c, iterationData.d, iterationData.i);
        extendedRight = endPartOfF(xor(extendedRight, key.key));

        return {
            left: iterationData.right,
            right: xor(iterationData.left, extendedRight),
            c: key.c,
            d: key.d,
            i: iterationData.i
        };
    }

    function endPartOfF(arr) {
        var mas = splitArray(arr, 6);
        var i = 0;
        for (i = 0; i < mas.length; i++) {
            var a = mas[i][0] * 2 + mas[i][5];
            var b = 0;
            for (var j = 1; j < 5; j++) {
                b = b << 1 | mas[i][j];
            }
            var bin = dec2bin(sboxes[i][a * 16 + b]);
            mas[i] = [bin[2], bin[3], bin[4], bin[5], bin[6], bin[7]];
        }
        var res = [];
        i = 0;
        for (var k = 0; k < mas.length; k++) {
            for (var l = 0; l < mas[k].length; l++) {
                res[i++] = mas[k][l];
            }
        }

        return permutation(res, fPermutation);
    }

    function lastStepPermutation(array) {
        return permutation(array, invIp);
    }



    function extend(arr) {
        var res = [arr[31]];

        var j = 1;

        for (var i = 0; i < arr.length; i++) {
            if (i % 4 === 0 && i !== 0) {
                res[j++] = arr[i - 1];
            }

            res[j++] = arr[i];

            if ((i + 1) % 4 === 0 && (i + 1) !== arr.length) {
                res[j++] = arr[i + 1];
            }
        }

        res[47] = arr[0];

        return res;
    }

    function strToArray(text, base) {
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
    }

    function xor(arr1, arr2) {
        var res = [];
        for (var i = 0; i < arr1.length; i++) {
            res[i] = arr1[i] ^ arr2[i];
        }

        return res;
    }

    function splitArray(arr, base) {
        var res = [];
        for (var i = 0; i < arr.length; i++) {
            var j = Math.floor(i / base);
            if (res[j] == undefined) {
                res[j] = [];
            }
            res[j][i % base] = arr[i];
        }

        return res;
    }

    function concatArrays(arr1, arr2) {
        var res = [];
        var i = 0,
            j = 0;
        for (j = 0; j < arr1.length; j++) {
            res[i++] = arr1[j];
        }
        for (j = 0; j < arr2.length; j++) {
            res[i++] = arr2[j];
        }

        return res;
    }

    function arrayToStr(arr) {
        var mas = [];
        var i = 0;
        for (var j = 0; j < arr.length; j++) {
            for (var k = 0; k < arr[j].length; k += 16) {
                mas[i++] = bin2dec(arr[j], k, 16);
            }
        }

        return String.fromCharCode.apply(null, mas);
    }

    function keyToArray(key) {
        var mas = [];
        var i = 0;
        for (var g = 0; g < key.length; g++) {

            var charCode = key.charCodeAt(key[g]);

            var bytes = dec2bin(charCode & 0xFF);

            for (var k2 = 0; k2 < 8; k2++) {
                mas[i] = bytes[k2];
                i++;
            }

        }

        return mas;
    }

    function cyclicShift(arr) {
        var first = arr[0];
        for (var i = 1; i < arr.length; i++) {
            arr[i - 1] = arr[i];
        }
        arr[arr.length - 1] = first;

        return arr;
    }

    function generateKey(c, d, i) {
        for (var j = 0; j < keyShift[i]; j++) {
            c = cyclicShift(c);
            d = cyclicShift(d);
        }

        var key = [];
        for (var k = 0; k < c.length; k++) {
            key.push(c[pc2[k * 2]]);
            key.push(d[pc2[k * 2 + 1]]);
        }

        return {
            key: key,
            c: c,
            d: d
        };
    }

    function permutation(arr, table) {
        var res = [];
        for (var i = 0; i < table.length; i++) {
            res[i] = arr[table[i] - 1];
        }

        return res;
    }

    function dec2bin(num) {
        var res = [0, 0, 0, 0, 0, 0, 0, 0];
        var i = 7;
        while (num !== 0) {
            res[i--] = num % 2;
            num = num >> 1;
        }

        return res;
    }

    function bin2dec(binArr, start, length) {
        var res = 0;
        for (var i = 0; i < length; i++) {
            res = (res << 1) | binArr[start + i];
        }

        return res;
    }


    function desDecryption(message) {
        var key = $('#des-key').val();
        var keyArray = keyToArray(key);
        var array = strToArray(message, 64);

        var i = 0;
        for (i = 0; i < array.length; i++) {
            array[i] = step1SwapArray(array[i]);
        }

        var keys = [];
        var splittedKey = splitArray(keyArray, 28);
        for (var k = 0; k < 16; k++) {
            keys[k] = generateKey(splittedKey[0], splittedKey[1], k);
            splittedKey[0] = keys[k].c;
            splittedKey[1] = keys[k].d;
        }

        for (i = 0; i < array.length; i++) {

            var splittedData = splitArray(array[i], 32);
            var itData = {
                left: splittedData[0],
                right: splittedData[1]
            };

            for (var j = 0; j < 16; j++) {
                itData = iterationDecryption(itData, keys[15 - j].key);
            }

            array[i] = concatArrays(itData.left, itData.right);
        }

        for (i = 0; i < array.length; i++) {
            array[i] = lastStepPermutation(array[i]);
        }

        return arrayToStr(array);
    }

    function iterationDecryption(iterationData, key) {
        var extendedLeft = extend(iterationData.left);
        extendedLeft = endPartOfF(xor(extendedLeft, key));

        return {
            right: iterationData.left,
            left: xor(iterationData.right, extendedLeft)
        };
    }

})(jQuery);