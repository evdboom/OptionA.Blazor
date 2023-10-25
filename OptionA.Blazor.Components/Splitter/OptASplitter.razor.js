export const getBoundingRect = (element) => {
    return element.getBoundingClientRect();
}

export const startDragging = (dotNetHelper, dragMethod, endMethod) => {
    const controller = new AbortController();
    window.addEventListener(
        "mousemove",
        async (event) => {
            var result = {
                clientX: event.clientX,
                clientY: event.clientY
            };
            await dotNetHelper.invokeMethodAsync(dragMethod, result);
        },
        {
            signal: controller.signal
        });
    window.addEventListener(
        "mouseup",
        async () => {
            controller.abort();
            await dotNetHelper.invokeMethodAsync(endMethod);
        },
        {
            once: true
        });
}
