export const getScrollHeight = (element) => {
    // First make the element visible (firefox doesn't like it otherwise)

    element.style.overflow = "scroll";
    element.style.visibility = "visible";
    const scrollHeight = element.scrollHeight;
    element.style.overflow = "";
    element.style.visibility = "";
    return scrollHeight;
}