using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace QGXUN0_HFT_2023241.Models.Extensions
{
    /// <summary>
    /// Attribute which checks whether a string is a correct website
    /// </summary>
    /// <remarks>
    /// 
    /// Format:
    /// <list type="bullet">
    /// 
    ///   <item>Scheme (optional):</item>
    ///   <description> only <c>HTTP</c> or <c>HTTPS</c> protocol allowed (e.g. <c>http://example.com</c>, <c>https://example.com</c>)</description>
    ///   
    ///   <item>Subdomain (optional):</item>
    ///   <description>only one allowed, can only contain letters, digits and dashes (e.g. <c>sub-domain.example.com</c>, <c>www.example.com</c>)</description>
    ///   
    ///   <item>Domain (required):</item>
    ///   <description>can only contain letters, digits and dashes (e.g. <c>example.com</c>, <c>web-site.com</c>, <c>example01.com</c>)</description>
    ///   
    ///   <item>Top Level Domain (required):</item>
    ///   <description>must be 2-4 letters (e.g. <c>example.com</c>, <c>example.org</c>, <c>example.hu</c>)</description>
    ///   
    ///   <item>Port (optional):</item>
    ///   <description>must be 1-5 digits (e.g. <c>example.com:443</c>, <c>example.com:80</c>, <c>example.com:8080</c>)</description>
    ///   
    ///   <item>Path (optional):</item>
    ///   <description>can only contain letters, digits, dashes and underscores (e.g. <c>example.com/page_address</c>, <c>example.com/other-page-address</c>)</description>
    ///   
    /// </list>
    /// 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
    public class WebsiteAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        public override bool RequiresValidationContext => false;

        /// <summary>
        /// Regular expression for a website
        /// </summary>
        protected static Regex websiteRegex = new(@"^((https?:)?(\/\/)?(www\.)?([a-zA-Z0-9\-]+\.)?([a-zA-Z0-9\-]+\.)([a-zA-Z]{2,4})(:\d*)(\/?[a-zA-Z0-9\-_]*))$");


        /// <inheritdoc/>
        public WebsiteAttribute() { }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> of the <see cref="object"/> is valid
        /// </summary>
        /// <param name="value">value of the <see cref="object"/> to validate</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> is valid, otherwise <see langword="false"/></returns>
        public override bool IsValid(object value)
        {
            return value == null || websiteRegex.IsMatch(value.ToString());
        }
    }
}
