namespace OptionA.Blazor.Components.Modal.Services
{
    /// <summary>
    /// Default implementation of <see cref="IModalService"/>
    /// </summary>
    public class ModalService : IModalService
    {
        private readonly IModalDataProvider _dataProvider;
        private readonly Dictionary<OptaModal, int> _openModals;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataProvider"></param>
        public ModalService(IModalDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _openModals = new();
        }

        /// <inheritdoc/>
        public event EventHandler<OptaModal>? ModalClosed;
        /// <inheritdoc/>
        public event EventHandler<OptaModal>? ModalClicked;

        /// <inheritdoc/>
        public int OnModalClicked(OptaModal modal)
        {
            return OnModalClicked(modal, true);
        }

        private int OnModalClicked(OptaModal modal, bool sendEvent)
        {
            var current = RegisterOpen(modal);
            var higher = _openModals
                .Where(other => other.Value > current)
                .ToList();
            foreach (var other in higher)
            {
                _openModals[other.Key] -= _dataProvider.ModalZIndexSteps;
            }

            var index = _openModals.Values.Max() + _dataProvider.ModalZIndexSteps;
            _openModals[modal] = index;
            if (sendEvent)
            {
                ModalClicked?.Invoke(this, modal);
            }            
            return index;
        }

        /// <inheritdoc/>
        public void RegisterClosed(OptaModal modal)
        {
            OnModalClicked(modal, false);
            ModalClosed?.Invoke(this, modal);
            _openModals.Remove(modal);
        }

        /// <inheritdoc/>
        public int RegisterOpen(OptaModal modal)
        {
            if (_openModals.TryGetValue(modal, out var result))
            {
                return result;
            }

            if (!_openModals.Any())
            {
                _openModals[modal] = _dataProvider.ModalZIndex;
                return _dataProvider.ModalZIndex;
            }

            var index = _openModals.Values.Max() + _dataProvider.ModalZIndexSteps;
            _openModals[modal] = index;

            return index;
        }
    }
}
