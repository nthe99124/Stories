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