const handlers = [];

export const registerHandler = (dotNetHelper, handlerName) => {
    handlers.push({ helper: dotNetHelper, name: handlerName });
    
}

export const unRegisterHandler = () => {
    window.removeEventListener("unload", handleUnload);
}

const handleUnload = async () => {
    if (handlers.length === 0) {
        return;
    }

    for await (const handler of handlers) {
        await handler.helper.invokeMethodAsync(handler.name);
    }
}

window.addEventListener("unload", handleUnload);