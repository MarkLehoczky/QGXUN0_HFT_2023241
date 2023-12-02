using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QGXUN0_HFT_2023241.Models.Attributes
{
    public class RequiredCollectionAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        public override bool RequiresValidationContext => false;


        /// <inheritdoc/>
        public RequiredCollectionAttribute() { }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> which is an <see cref="IEnumerable{T}"/> null or empty
        /// </summary>
        /// <param name="value">value of the <see cref="IEnumerable{T}"/> to validate</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> is valid, otherwise <see langword="false"/></returns>
        public override bool IsValid(object value)
        {
            return value != null && value is IEnumerable<object> collection && collection.Any();
        }
    }
}
