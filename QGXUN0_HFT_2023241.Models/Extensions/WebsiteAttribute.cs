using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace QGXUN0_HFT_2023241.Models.Extensions
{
    /// <summary>
    /// Specifies the minimum and maximum length of characters that are allowed in a data field.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    ///   <item>Scheme (optional):</item>
    ///   <description> only <c>HTTP</c> or <c>HTTPS</c> protocol allowed</description>
    ///   
    ///   <item>Subdomain (optional):</item>
    ///   <description>only one allowed, can only contain letters, digits and dashes</description>
    ///   
    ///   <item>Domain (required):</item>
    ///   <description>can only contain letters, digits and dashes</description>
    ///   
    ///   <item>Top Level Domain (required):</item>
    ///   <description>must be 2-4 letters</description>
    ///   
    ///   <item>Port (optional):</item>
    ///   <description>must be 1-5 digits</description>
    ///   
    ///   <item>Path (optional):</item>
    ///   <description>can only contain letters, digits, dashes, underscores and forward slashes</description>
    /// </list>
    /// </remarks>
    /// <example>
    /// <para>Accepted format examples:</para>
    /// <code>
    /// http://example.com
    /// https://example.com
    /// www.example.com
    /// sub-domain.example.com
    /// www.example01.example.com
    /// example.com
    /// other-example.com
    /// example01.com
    /// example.org
    /// example.hu
    /// example.com:80
    /// example.com:443
    /// example.com:8080
    /// example.com/page_address
    /// example.com/other-page-address
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
    public class WebsiteAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        public override bool RequiresValidationContext => false;

        /// <summary>
        /// Specifies a regular expression for a website.
        /// </summary>
        private static Regex websiteRegex = new(@"^((https?:)?(\/\/)?(www\.)?([a-zA-Z0-9\-]+\.)?([a-zA-Z0-9\-]+\.)([a-zA-Z]{2,4})(:\d{1,5})?(/[a-zA-Z0-9\-_\/]*)?)$");


        /// <inheritdoc/>
        public WebsiteAttribute() { }

        /// <summary>
        /// Determines whether a specified <see cref="object"/> is valid.
        /// </summary>
        /// <param name="value">The object to validate</param>
        /// <returns><see langword="true"/> if the specified object is valid; otherwise, <see langword="false"/></returns>
        public override bool IsValid(object value)
        {
            return value == null || websiteRegex.IsMatch(value.ToString());
        }
    }
}
