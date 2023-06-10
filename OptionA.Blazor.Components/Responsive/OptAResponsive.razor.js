const handlers = [];

export const registerHandler = (dotNetHelper, handlerName) => {
    handlers.push({ helper: dotNetHelper, name: handlerName });
}

export const getDimension = () => {
    var result = {
        width: window.innerWidth,
        height: window.innerHeight
    }

    return result;
}

const handleResize = async () => {
    if (handlers.length === 0) {
        return;
    }

    var result = {
        width: window.innerWidth,
        height: window.innerHeight,
    }

    for await (const handler of handlers) {
        await handler.helper.invokeMethodAsync(handler.name, result);
    }
}

window.onresize = handleResize;