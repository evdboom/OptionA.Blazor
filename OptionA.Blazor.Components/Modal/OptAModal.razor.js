export const showDialog = (dialogElement, dotNetHelper, closeHandlerName, isModal) => {
    if (!dialogElement) {
        return;
    }

    if (isModal) {
        dialogElement.showModal();
    } else {
        dialogElement.show();
    }
    
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
