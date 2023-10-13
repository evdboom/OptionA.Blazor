using OptionA.Blazor.Storage.Utilities;

namespace OptionA.Blazor.Storage.Extensions
{
    internal static class DatabasePlanExtensions
    {
        public static IDatabaseAccess ToAccess(this DatabasePlan plan)
        {
            return new DatabaseAccess
            {
                DatabaseName = plan.Name,
                Version = plan.Version,
            };
        }
    }
}
