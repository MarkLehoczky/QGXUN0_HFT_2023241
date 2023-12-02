using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace QGXUN0_HFT_2023241.Models.Attributes
{
    /// <summary>
    /// Extension methods for the models
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Determines whether the specified <paramref name="value"/> of the <typeparamref name="T"/> is valid
        /// </summary>
        /// <typeparam name="T">generic class</typeparam>
        /// <param name="value">value for validation</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> is valid, otherwise <see langword="false"/></returns>
        public static bool IsValid<T>(this T value) where T : class
        {
            if (value == null)
                return false;

            foreach (var property in value.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                    if (!attribute.IsValid(property.GetValue(value)))
                        return false;

            return true;
        }
        /// <summary>
        /// Determines whether the specified <paramref name="value"/> of the <typeparamref name="T"/> is valid
        /// </summary>
        /// <typeparam name="T">generic class</typeparam>
        /// <param name="value">value for validation</param>
        /// <param name="ignoreTypes">ignored <see cref="ValidationAttribute"/> types</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> is valid, otherwise <see langword="false"/></returns>
        public static bool IsValid<T>(this T value, IEnumerable<Type> ignoreTypes) where T : class
        {
            if (value == null)
                return false;

            foreach (var property in value.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                    if (!ignoreTypes.Contains(attribute.GetType()) && !attribute.IsValid(property.GetValue(value)))
                        return false;

            return true;
        }
        /// <summary>
        /// Determines whether the specified <paramref name="value"/> of the <typeparamref name="T"/> is valid
        /// </summary>
        /// <typeparam name="T">generic class</typeparam>
        /// <param name="value">value for validation</param>
        /// <param name="ignoreTypes">ignored <see cref="ValidationAttribute"/> types</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> is valid, otherwise <see langword="false"/></returns>
        public static bool IsValid<T>(this T value, params Type[] ignoreTypes) where T : class
        {
            return value.IsValid(ignoreTypes.AsEnumerable());
        }

        /// <summary>
        /// Validates a specified <paramref name="value"/> of the <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">generic class</typeparam>
        /// <param name="value">value for validation</param>
        /// <exception cref="ValidationException"><paramref name="value"/> is not valid</exception>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">The current attribute is malformed</exception>
        public static void Validate<T>(this T value) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            foreach (var property in value.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                    attribute.Validate(property.GetValue(value), property.Name);
        }
        /// <summary>
        /// Validates a specified <paramref name="value"/> of the <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">generic class</typeparam>
        /// <param name="value">value for validation</param>
        /// <param name="ignoreTypes">ignored <see cref="ValidationAttribute"/> types</param>
        /// <exception cref="ValidationException"><paramref name="value"/> is not valid</exception>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">The current attribute is malformed</exception>
        public static void Validate<T>(this T value, IEnumerable<Type> ignoreTypes) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            foreach (var property in value.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                    if (!ignoreTypes.Contains(attribute.GetType()))
                        attribute.Validate(property.GetValue(value), property.Name);
        }
        /// <summary>
        /// Validates a specified <paramref name="value"/> of the <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">generic class</typeparam>
        /// <param name="value">value for validation</param>
        /// <param name="ignoreTypes">ignored <see cref="ValidationAttribute"/> types</param>
        /// <exception cref="ValidationException"><paramref name="value"/> is not valid</exception>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">The current attribute is malformed</exception>
        public static void Validate<T>(this T value, params Type[] ignoreTypes) where T : class
        {
            value.Validate(ignoreTypes.AsEnumerable());
        }
    }
}
