using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Test.Pages
{
    public partial class Buttons
    {
        private bool _oneDisabled;
        private Task ClickButtonOne(MouseEventArgs args)
        {
            _oneDisabled = !_oneDisabled;
            return Task.CompletedTask;
        }

        private bool ButtonOneDisabled()
        {
            return _oneDisabled;
        }
    }
}
