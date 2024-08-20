window.localStorageManager = {
    getItems: function () {
        return localStorage;
    }
}

window.sessionStorageManager = {
    getItems: function () {
        return sessionStorage;
    }
}
