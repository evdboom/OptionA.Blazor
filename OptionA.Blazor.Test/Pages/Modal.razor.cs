using Microsoft.AspNetCore.Components.Web;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Pages
{
    public partial class Modal
    {
        private OptAModal? _modal;
        private bool _draggable;
        private ModalSize _size;

        private string _text = string.Empty;

        private void OpenModal(bool draggable = false, ModalSize size = ModalSize.Default)
        {
            if (_modal is null)
            {
                return;
            }

            _draggable = draggable;
            _size = size;

            _modal.Show();
        }
    }
}
