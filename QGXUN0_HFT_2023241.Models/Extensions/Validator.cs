using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace QGXUN0_HFT_2023241.Models.Extensions
{
    /// <summary>
    /// Specifies generic methods for <see cref="ValidationAttribute"/> validation
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Determines whether the specified <paramref name="value"/> of the <typeparamref name="T"/> is valid
        /// </summary>
        /// <typeparam name="T"><see langword="class"/></typeparam>
        /// <param name="value">The value of the <typeparamref name="T"/> to validate</param>
        /// <returns><see langword="true"/> if the specified <typeparamref name="T"/> is valid; otherwise, <see langword="false"/></returns>
        public static bool IsValid<T>(this T value) where T : class
        {
            if (value == null) return false;

            foreach (var property in value.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                    if (!attribute.IsValid(property.GetValue(value)))
                        return false;

            return true;
        }

        /// <summary>
        /// Validates a specified <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"><see langword="class"/></typeparam>
        /// <param name="value">The value of the <typeparamref name="T"/> to validate</param>
        /// <exception cref="ArgumentNullException">The specified <typeparamref name="T"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">The current attribute is malformed</exception>
        /// <exception cref="TypeLoadException">Type-loading failure occurred</exception>
        /// <exception cref="ValidationException"><paramref name="value"/> is not valid</exception>
        public static void Validate<T>(this T value) where T : class
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            foreach (var property in value.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                    attribute.Validate(property.GetValue(value), property.Name);
        }
    }
}
