export const scrollActiveIntoView = (index) => {
    const element = document.querySelector(`[opta-thumbnail-image][opta-index="${index}"]`);
    element.scrollIntoView({
        block: "nearest",
        inline: "nearest",
        behavior: "smooth"
    });
}