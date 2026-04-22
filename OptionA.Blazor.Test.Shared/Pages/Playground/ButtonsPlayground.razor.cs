using OptionA.Blazor.Components;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Test.Shared.Pages.Playground;

public partial class ButtonsPlayground
{
    private readonly PlaygroundDescriptor<OptAButton> _buttonDescriptor = new()
    {
        Title = "Buttons playground",
        Description = "Interactive playground for the OptAButton component.",
        Parameters =
        [
            new PlaygroundParameterDescriptor
            {
                Name = nameof(OptAButton.Name),
                DisplayName = "Name",
                Description = "The button label shown when the button type includes text.",
                DefaultValue = "Save",
                ValueType = typeof(string),
                EditorType = ParameterEditorType.Text,
                Group = "Content",
                Order = 0
            },
            new PlaygroundParameterDescriptor
            {
                Name = nameof(OptAButton.Description),
                DisplayName = "Description",
                Description = "Tooltip text for the button.",
                DefaultValue = "Save the current record",
                ValueType = typeof(string),
                EditorType = ParameterEditorType.Text,
                Group = "Content",
                Order = 1
            },
            new PlaygroundParameterDescriptor
            {
                Name = nameof(OptAButton.ActionType),
                DisplayName = "Action type",
                Description = "Determines the default icon and color classes.",
                DefaultValue = ActionType.Confirm,
                ValueType = typeof(ActionType),
                EditorType = ParameterEditorType.Enum,
                Group = "Appearance",
                Order = 0
            },
            new PlaygroundParameterDescriptor
            {
                Name = nameof(OptAButton.ButtonType),
                DisplayName = "Button type",
                Description = "Switch between icon-only, text-only, or full buttons.",
                DefaultValue = ButtonTypes.Full,
                ValueType = typeof(ButtonTypes),
                EditorType = ParameterEditorType.Enum,
                Group = "Appearance",
                Order = 1
            },
            new PlaygroundParameterDescriptor
            {
                Name = nameof(OptAButton.IsSubmit),
                DisplayName = "Submit button",
                Description = "Controls whether the button submits its parent form.",
                DefaultValue = false,
                ValueType = typeof(bool),
                EditorType = ParameterEditorType.Boolean,
                Group = "Behavior",
                Order = 0
            }
        ]
    };
}
