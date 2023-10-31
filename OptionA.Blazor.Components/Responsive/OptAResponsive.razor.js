export const registerHandler = (dotNetHelper, handlerName) => {
    const controller = new AbortController();
    window.addEventListener(
        "resize",
        async () => {
            const result = {
                width: window.innerWidth,
                height: window.innerHeight,
            };
            await dotNetHelper.invokeMethodAsync(handlerName, result);
        },
        {
            signal: controller.signal
        });
    return controller;
}

export const getDimension = () => {
    const result = {
        width: window.innerWidth,
        height: window.innerHeight
    }

    return result;
}