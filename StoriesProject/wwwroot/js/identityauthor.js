function validateRegisterForm() {
    let name = $('.identity-form input[name="Name"]'),
        email = $('.identity-form input[name="Email"]'),
        phone = $('.identity-form input[name="PhoneNumber"]'),
        bankName = $('.identity-form select[name="BankName"]'),
        bankNumber = $('.identity-form input[name="BankNumber"]'),
        bankAccount = $('.identity-form input[name="BankAccount"]');
    clearAllErrInfo();
    if (!name.val()) {
        addErrInfo(name, 'Họ tên không được trống');
        return false;
    }
    if (!email.val()) {
        addErrInfo(email, 'Email không được trống');
        return false;
    }
    if (!phone.val()) {
        addErrInfo(phone, 'Số điện thoại không được trống');
        return false;
    }
    if (!bankName.val()) {
        addErrInfo(bankName, 'Tên ngân hàng không được trống');
        return false;
    }
    if (!bankNumber.val()) {
        addErrInfo(bankNumber, 'Số thẻ không được trống');
        return false;
    }
    if (!bankAccount.val()) {
        addErrInfo(bankAccount, 'Số tài khoản không được trống');
        return false;
    }
    return true;
}

function showToast(message) {
    // tạm thời show alert
    alert(message);
}

function addErrInfo($ele, msg) {
    var err_tpl =
        '<div class="j-verify-err"><i class="iconfont icon-ic-safe-off"></i><span></span></div>';
    if ($ele.parents('.layui-form-item').find('.j-verify-err').length == 0) {
        var $err_tpl = $(err_tpl)
            .find('span')
            .text(msg)
            .end();
        $ele.parents('.layui-form-item').append($err_tpl);
    } else {
        $ele
            .parents('.layui-form-item')
            .find('.j-verify-err span')
            .text(msg);
    }
}


function clearAllErrInfo() {
    $('.identity-form input')
        .parents('.layui-form-item')
        .find('.j-verify-err')
        .remove();
}