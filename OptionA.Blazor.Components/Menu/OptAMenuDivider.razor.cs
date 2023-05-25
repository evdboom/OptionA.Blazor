﻿using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components.Menu
{
    public partial class OptAMenuDivider
    {
        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Additonal classes to add
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        private string GetClasses()
        {
            return $"{Provider.GetDividerClass()} {AdditionalClasses}".Trim();
        }
    }
}