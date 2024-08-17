window.cultureManager = {
    get: () => window.localStorage["app-culture"],
    set: (value) => window.localStorage["app-culture"] = value
}
