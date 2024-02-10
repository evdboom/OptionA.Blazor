namespace OptionA.Blazor.Test.Struct
{
    public class TestObjectComparer : IComparer<TestObject>
    {
        public int Compare(TestObject? x, TestObject? y)
        {
            return (x?.ValueInt ?? 0) - (y?.ValueInt ?? 0);
        }
    }
}
