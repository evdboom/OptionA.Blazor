namespace OptionA.Blazor.Components.Modal.Services
{
    /// <summary>
    /// Interface for support of multiple modals at the same time
    /// </summary>
    public interface IModalService
    {
        /// <summary>
        /// Registers the modal as open, returns the required zindex to be on top
        /// </summary>
        /// <param name="modal"></param>
        /// <returns></returns>
        int RegisterOpen(OptaModal modal);
        /// <summary>
        /// Register the modal as closed, removing it from the list
        /// </summary>
        /// <param name="modal"></param>
        void RegisterClosed(OptaModal modal);
        /// <summary>
        /// Gets the required zindex for the modal 
        /// </summary>
        /// <param name="modal"></param>
        /// <returns></returns>
        int OnModalClicked(OptaModal modal);
        /// <summary>
        /// Occurs when a modal is closed
        /// </summary>
        event EventHandler<OptaModal> ModalClosed;
        /// <summary>
        /// Occurs when a modal is clicked
        /// </summary>
        event EventHandler<OptaModal> ModalClicked;
    }
}
