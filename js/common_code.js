//Thêm hàm indexOf cho Array trong IE8
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (obj, start) {
        for (var i = (start || 0), j = this.length; i < j; i++) {
            if (this[i] === obj) { return i; }
        }
        return -1;
    }
}

function loading(loading) {
    if (!document.getElementById("AjaxLoading")) {
        var left = (GetWindowWidth() - 36) / 2;
        var ajaxLoading = '<div id="AjaxLoading" onclick="loading(false)" style="background:#fff url(' + webUrl + 'cms/admin/cs/AllModul/AjaxLoading.gif) no-repeat center;display:none;width:36px;height:36px;line-height:36px;position:fixed;_position:absolute;top:40%;left:' + left + 'px;z-index:9999;border:solid 1px #fff;border-radius:36px"><!----></div>';
        jQuery("body").append(ajaxLoading);
    }

    if (typeof loading == 'undefined' || loading) {
        jQuery("#AjaxLoading").show();
    } else {
        jQuery("#AjaxLoading").fadeOut();
    }
}



$(document).ready(function () {
    $(".number").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
});

function RemoveNotDigit(s, removeDot) {
    s = s.replace(/[^\d.]/g, '');

    if (removeDot)
        s = s.replace(/\./g, '');

    if (s == '')
        s = '0';

    return s;
}

function CheckEmail(idInput, message) {
    if (!message)
        message = 'Email không hợp lệ!';

    var email = jQuery(idInput);
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email.val())) {
        alert(message);
        email.focus();
        return false;
    }
    return true;
}
function CheckEmailValue(email, message) {
    if (!message)
        message = 'Email không hợp lệ!';

    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email)) {
        alert(message);
        return false;
    }
    return true;
}

function CheckEmailValue01(email) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email)) {
        return false;
    }
    return true;
}

function InitCheckInputContact(selector) {
    jQuery(selector + " .required").change(function () {
        if (this.value.length < 1) {
            jQuery(this).addClass("notfilled");
            //jQuery(this).parent().append(errdiv);
            this.focus();
        } else {
            jQuery(this).removeClass("notfilled");
        }
    });
}

function InitCheckInputForm(selector) {
    InitCheckInputContact(selector);
}

function ResetInputContact(selector) {
    jQuery(selector + " .errorms").remove();
    jQuery(selector + " .notfilled").removeClass("notfilled");

    jQuery(selector + " input").val("");
    jQuery(selector + " textarea").val("");
}

function ResetInputForm(selector) {
    ResetInputContact(selector);
}

function CheckInputContact(selector, checkEmail) {
    var firstId = '';
    var pass = true;
    var errdiv = '<div class="errorms">Đây là ô bắt buộc phải điền</div>';
    jQuery(selector + " .errorms").remove();
    jQuery(selector + " .notfilled").removeClass("notfilled");

    jQuery(selector + " .required").each(function () {
        if (this.value.length < 1) {
            pass = false;
            jQuery(this).addClass("notfilled");

            //jQuery(this).parent().append(errdiv);
            //this.focus();
            if (firstId.length < 1)
                firstId = this.id;
        }
    });
    if (!pass)
        document.getElementById(firstId).focus();
    else {
        if (checkEmail)
            if (jQuery(selector + ' #tbEmail').length > 0)
                pass = CheckEmail(selector + ' #tbEmail');
        if (!pass) {
            document.getElementById('tbEmail').focus();
        }
    }

    return pass;
}

function CheckInputForm(selector, checkEmail) {
    return CheckInputContact(selector, checkEmail);
}

function ResetInputValue(selector) {
    jQuery(selector + " input").val("");
    jQuery(selector + " textarea").val("");
}