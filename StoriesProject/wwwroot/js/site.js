﻿function setSessionStorage(key, value) {
    sessionStorage.setItem(key, value);
}

function getSessionStorage(key) {
    var value = sessionStorage.getItem(key);
    return value;
}