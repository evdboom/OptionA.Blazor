using OptionA.Blazor.Components;
using OptionA.Blazor.Test.Struct;

namespace OptionA.Blazor.Test.Pages
{
    public partial class Direct
    {
        private bool _orderDescending;
        private EnumOrder _orderMode;
        private TestEnum _selectedEnum;
        private Dictionary<TestEnum, string> _nameMappings = new()
        {
            { TestEnum.One, "1_" },
            { TestEnum.Two, "2_" },
            { TestEnum.Four, "4_" },
            { TestEnum.Eight, "8_" }
        };
        private Dictionary<TestEnum, string> _titleMappings = new()
        {
            { TestEnum.One, "OneT" },
            { TestEnum.Two, "TwoT" },
            { TestEnum.Four, "FourT" },
            { TestEnum.Eight, "EightT" }
        };

        private int _selectedInt;
        private string? _selectedText;
        private string? _selectedText2;

        private bool _newLineAsSeparator;
        private bool _autoGrow;

        private IEnumerable<TestObject> _items =
        [
            new TestObject
            {
                Name = "Test3",
                Value = "Test3_2",
                ValueInt = 4
            },
            new TestObject
            {
                Name = "Test1",
                Value = "Test1_2",
                ValueInt = 1
            },
            new TestObject
            {
                Name = "Test4",
                Value = "Test4_2",
                ValueInt = 8
            },
            new TestObject
            {
                Name = "Test2",
                Value = "Test2_2",
                ValueInt = 2
            }
        ];

        private string? DisplayValue(TestObject item) => item.Name;
        private string? TitleValue(TestObject item) => item.Value;

        private TestObject? _selectedItem;
        private bool _useComparer;      

        private Orientation Orientation => _newLineAsSeparator ? Orientation.Vertical : Orientation.Horizontal;
        private TestObjectComparer? Comparer => _useComparer ? new TestObjectComparer() : null;

        private void ChangeOrderMode()
        {
            _orderMode = _orderMode switch
            {
                EnumOrder.Value => EnumOrder.Name,
                EnumOrder.Name => EnumOrder.DisplayValue,
                EnumOrder.DisplayValue => EnumOrder.Value,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}