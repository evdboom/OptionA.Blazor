let handlers = [];

export const registerHandler = (dotNetHelper, handlerName) => {
    if (handlers.length === 0) {
        window.addEventListener("unload", handleUnload);
    }
    const id = generateId(6);
    handlers.push({ helper: dotNetHelper, name: handlerName, id: id });
    return id;
}

export const unRegisterHandler = (id) => {
    handlers = handlers.filter(handler => handler.id !== id);

    if (handlers.length === 0) {
        window.removeEventListener("unload", handleUnload);
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

const handleUnload = async () => {
    if (handlers.length === 0) {
        return;
    }

    for await (const handler of handlers) {
        await handler.helper.invokeMethodAsync(handler.name);
    }
}

