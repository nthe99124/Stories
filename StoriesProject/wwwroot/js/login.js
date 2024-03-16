function addErrInfo($ele, msg) {
    var err_tpl =
        '<div class="j-verify-err"><i class="iconfont icon-ic-safe-off"></i><span></span></div>';
    if ($ele.parents('.form-item').find('.j-verify-err').length == 0) {
        var $err_tpl = $(err_tpl)
            .find('span')
            .text(msg)
            .end();
        $ele.parents('.form-item').append($err_tpl);
    } else {
        $ele
            .parents('.form-item')
            .find('.j-verify-err span')
            .text(msg);
    }
}

function validateLoginForm() {
    let account = $('.login-form input[name=account]'),
        password = $('.login-form input[name=password]');
    clearAllErrInfo();
    if (!account.val()) {
        addErrInfo(account, 'UserName không được trống');
        return false;
    }
    if (!password.val()) {
        addErrInfo(password, 'Password không được trống');
        return false;
    }
    return true;
}

function validateRegisterForm() {
    let account = $('.register-form input[name=account]'),
        phone = $('.register-form input[name=phone]'),
        password = $('.register-form input[name=setpassword]');
    clearAllErrInfo();
    if (!account.val()) {
        addErrInfo(account, 'UserName không được trống');
        return false;
    }
    if (!phone.val()) {
        addErrInfo(phone, 'Số điện thoại không được trống');
        return false;
    }
    if (!password.val()) {
        addErrInfo(password, 'Password không được trống');
        return false;
    }
    return true;
}

function clearAllErrInfo() {
    $('.dialog-login_form input')
        .parents('.form-item')
        .find('.j-verify-err')
        .remove();
}

function showToast(msg) {
    var $toast = $('.j-dialog-login-toast');
    var parent_width = $toast.parents('.dialog-login').outerWidth();
    var parent_height = $toast.parents('.dialog-login').outerHeight();
    $toast.outerHeight();
    $toast
        .fadeIn()
        .text(msg)
        .css({
            position: 'absolute',
            left: parent_width / 2 - $toast.outerWidth() / 2,
            top: parent_height / 2 - $toast.outerHeight() / 2 - 50
        });
    setTimeout(() => {

    }, 2000);
}