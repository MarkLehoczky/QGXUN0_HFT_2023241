using System.Collections.Generic;

namespace QGXUN0_HFT_2023241.Models.Extensions
{
    /// <summary>
    /// Filter options for an <see cref="IEnumerable{Book}"/> collection
    /// </summary>
    public enum BookFilter { MostExpensive, LeastExpensive, HighestRated, LowestRated }

    /// <summary>
    /// Filter options for an <see cref="Collection"/>
    /// </summary>
    public enum CollectionFilter { Series, NonSeries, Collection }
}
