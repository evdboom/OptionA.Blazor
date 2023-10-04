using Microsoft.AspNetCore.Components.Web;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Pages
{
    public partial class Modal
    {
        private OptaModal? _modal;
        private OptaModal? _modal2;
        private OptaModal? _modal3;
        private bool _draggable;
        private bool _scrollable;
        private bool _dropdown;
        private bool _canClose;
        private bool _open;

        private string _text = string.Empty;

        private async Task OpenModal(MouseEventArgs args, bool draggable = false, bool scrollable = false, bool dropdown = false, bool canClose = true, bool onMouse = false)
        {
            if (_modal is null)
            {
                return;
            }

            _draggable = draggable;
            _scrollable = scrollable;
            _dropdown = dropdown;
            _canClose = canClose;

            if (_open)
            {
                await _modal.Close();
            }
            else if (onMouse)
            {
                await _modal.ShowOnMouse(args);
            }
            else
            {
                await _modal.Show();
            }
        }

        private async Task OpenModal2(MouseEventArgs args, bool draggable = false, bool scrollable = false, bool dropdown = false, bool canClose = true, bool onMouse = false)
        {
            if (_modal2 is null)
            {
                return;
            }

            _draggable = draggable;
            _scrollable = scrollable;
            _dropdown = dropdown;
            _canClose = canClose;

            if (onMouse)
            {
                await _modal2.ShowOnMouse(args);
            }
            else
            {
                await _modal2.Show();
            }
        }

        private async Task OpenModal3(MouseEventArgs args, bool draggable = false, bool scrollable = false, bool dropdown = false, bool canClose = true, bool onMouse = false)
        {
            if (_modal3 is null)
            {
                return;
            }

            _draggable = draggable;
            _scrollable = scrollable;
            _dropdown = dropdown;
            _canClose = canClose;

            if (onMouse)
            {
                await _modal3.ShowOnMouse(args);
            }
            else
            {
                await _modal3.Show();
            }
        }

        private void ModalOpen()
        {
            _open = true;
        }

        private void ModalClose()
        {
            _open = false;
        }
    }
}
