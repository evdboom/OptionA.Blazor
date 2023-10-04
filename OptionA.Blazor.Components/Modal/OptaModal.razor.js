let handlers = [];

export const registerHandler = (dotNetHelper, handlerName) => {
    if (handlers.length === 0) {
        window.addEventListener("scroll", handleScroll);
    }
    const id = generateId(6);
    handlers.push({ helper: dotNetHelper, name: handlerName, id: id });
    return id;
}

export const unregisterHandler = (id) => {
    handlers = handlers.filter(handler => handler.id !== id);

    if (handlers.length === 0) {
        window.removeEventListener("scroll", handleScroll);
    }
}

export const getScrollPosition = () => {
    var result = {
        scrollX: window.scrollX,
        scrollY: window.scrollY
    }

    return result;
}

const handleScroll = async () => {
    if (handlers.length === 0) {
        return;
    }

    var result = {
        scrollX: window.scrollX,
        scrollY: window.scrollY,
    }

    for await (const handler of handlers) {
        await handler.helper.invokeMethodAsync(handler.name, result);
    }
}

const generateId = function makeid(length) {
    let result = '';
    const characters = 'abcdefghijklmnopqrstuvwxyz0123456789';
    const charactersLength = characters.length;
    let counter = 0;
    while (counter < length) {
        result += characters.charAt(Math.random() * charactersLength);
        counter++;
    }
    return result;
}