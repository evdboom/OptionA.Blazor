export const showDialog = (dialogElement, dotNetHelper, closeHandlerName) => {
    if (!dialogElement) {
        return;
    }

    dialogElement.showModal();
    dialogElement.addEventListener("close", async () => {
        await dotNetHelper.invokeMethodAsync(closeHandlerName);
    });
}

export const closeDialog = (dialogElement) => {
    if (!dialogElement) {
        return;
    }

    dialogElement.close();
}

export const getBoundingRect = (element) => {
    return element.getBoundingClientRect();
}