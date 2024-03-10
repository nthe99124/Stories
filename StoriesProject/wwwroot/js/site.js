function setSessionStorage(key, value) {
    sessionStorage.setItem(key, value);
}

function getSessionStorage(key) {
    var value = sessionStorage.getItem(key);
    return value;
}

function removeSessionStorage(key) {
    var value = sessionStorage.removeItem(key);
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