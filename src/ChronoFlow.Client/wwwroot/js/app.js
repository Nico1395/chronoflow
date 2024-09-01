// Object literals
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

// Objects
class FloatingContainer {
    componentReference;
    floatingContainerId;
    clickOrFocusEventHandler;

    constructor(componentReference, floatingContainerId) {
        this.componentReference = componentReference;
        this.floatingContainerId = floatingContainerId;
        this.clickOrFocusEventHandler = (event) => this.onClickOrFocus(event);
    }

    onOpen() {
        this.addClickOfFocusEventListener();
    }

    onClose() {
        this.removeClickOfFocusEventListener();
    }

    addClickOfFocusEventListener() {
        document.body.addEventListener("click", this.clickOrFocusEventHandler);
        document.body.addEventListener("focus", this.clickOrFocusEventHandler);
    }

    removeClickOfFocusEventListener() {
        document.body.removeEventListener("click", this.clickOrFocusEventHandler);
        document.body.removeEventListener("focus", this.clickOrFocusEventHandler);
    }

    onClickOrFocus(event) {
        const floatingContainer = document.getElementById(this.floatingContainerId);
        if (floatingContainer === undefined || floatingContainer == null)
            return;

        if (!floatingContainer.contains(event.target))
            this.componentReference.invokeMethodAsync("OnFocusLostAsync");
    }
}

// Object factory functions
function createFloatingContainer(componentReference, floatingContainerId) {
    return new FloatingContainer(componentReference, floatingContainerId);
}

