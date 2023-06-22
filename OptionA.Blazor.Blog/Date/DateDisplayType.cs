namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Way to display the date
    /// </summary>
    public enum DateDisplayType
    {
        /// <summary>
        /// Display just date (d string format)
        /// </summary>
        Date,
        /// <summary>
        /// Display date and time (g string format)
        /// </summary>
        DateTime,
        /// <summary>
        /// Display just time (t string format)
        /// </summary>
        Time,
        /// <summary>
        /// Display sortable date (yyyyMMdd string format)
        /// </summary>
        UsableDate,
        /// <summary>
        /// Display sortable date and time (u string format)
        /// </summary>
        UsableDateTime,
        /// <summary>
        /// Display long date (D string format)
        /// </summary>
        LongDate,
        /// <summary>
        /// Display long date and time (f string format)
        /// </summary>
        LongDateTime,
        /// <summary>
        /// Display just the month (MMMM string format)
        /// </summary>
        Month,
        /// <summary>
        /// Display just the year (yyyy string format)
        /// </summary>
        Year,
        /// <summary>
        /// Display the year and the month (Y string format)
        /// </summary>
        YearMonth
    }
}
