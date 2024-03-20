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

window.handleSearchHover = () => {
    $('.in-header__search .search-hot__item').mouseenter(function (e) {
        $('.in-header__search .search-hot__item').removeClass('active');
        $(e.target).parents('.search-hot__item').addClass('active')
    });
}

window.addCategory = () => {
    showComicInfo.addEventListener('mouseleave', () => {
        showComicInfo.style.display = 'none';
    });
}

window.getValue = function (element) {
    return element.value;
}

window.reload = function () {
    window.location.reload();
}