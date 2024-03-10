using Newtonsoft.Json;
using QGXUN0_HFT_2023241.Models.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace QGXUN0_HFT_2023241.Models.Models
{
    /// <summary>
    /// Specifies the unique ID, name and website of a <see cref="Publisher"/>.
    /// </summary>
    public class Publisher : IComparable<Publisher>, IComparable<string>, IComparable, IEquatable<Publisher>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Publisher"/>
        /// </summary>
        /// <remarks>The value of this property is used as the <see langword="unique key"/> in the database</remarks>.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("PublisherID")]
        [JsonProperty("PublisherID")]
        [Required]
        [Key]
        public int PublisherID { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="Publisher"/>.
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [JsonPropertyName("PublisherName")]
        [JsonProperty("PublisherName")]
        [Required]
        public string PublisherName { get; set; }

        /// <summary>
        /// Gets or sets the books of the <see cref="Publisher"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        /// <summary>
        /// Gets or sets the website of the <see cref="Publisher"/>.
        /// </summary>
        [StringLength(250, MinimumLength = 1)]
        [JsonPropertyName("Website")]
        [JsonProperty("Website")]
        [Website]
        public string Website { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/>.
        /// </summary>
        public Publisher() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/> by using the required properties.
        /// </summary>
        /// <param name="publisherID">Unique ID of the <see cref="Publisher"/></param>
        /// <param name="publisherName">Name of the <see cref="Publisher"/></param>
        public Publisher(int publisherID, string publisherName)
        {
            PublisherID = publisherID;
            PublisherName = publisherName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/> by using the required and optional properties.
        /// </summary>
        /// <param name="publisherID">Unique ID of the <see cref="Publisher"/></param>
        /// <param name="publisherName">Name of the <see cref="Publisher"/></param>
        /// <param name="website">Website of the <see cref="Publisher"/></param>
        public Publisher(int publisherID, string publisherName, string website)
        {
            PublisherID = publisherID;
            PublisherName = publisherName;
            Website = website;
        }


        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Publisher"/>.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Publisher"/> to convert</param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see cref="Publisher"/> representation of the <paramref name="data"/></returns>
        /// <exception cref="ArgumentException">An error occurred during parsing</exception>
        /// <exception cref="ValidationException">The specified <paramref name="data"/> is not valid</exception>
        /// <example><code>
        /// Publisher p1 = Publisher.Parse("1;Publisher name;www.website.com");
        /// Publisher p2 = Publisher.Parse("2$Publisher name", "$");
        /// Publisher p3 = Publisher.Parse("3;Publisher name;www.website.com", ";", false);
        /// </code></example>
        public static Publisher Parse(string data, string splitString = ";", bool validate = true)
        {
            string[] splitData = data.Split(splitString);
            if (splitData.Length < 2)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var publisherID))
                throw new ArgumentException("The 'PublisherID' property cannot be parsed to an 'int' type", nameof(data));

            string publisherName = splitData[1];

            string website = splitData.Length > 2 ? splitData[2] : null;

            Publisher publisher = new Publisher(publisherID, publisherName, website);
            if (validate) publisher.Validate();

            return publisher;
        }

        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Publisher"/>. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Publisher"/> to convert</param>
        /// <param name="publisher"><see cref="Publisher"/> representation of the <paramref name="data"/> if the parsing was successful; otherwise, <see langword="null"/></param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see langword="true"/> if the parsing was successful; otherwise, <see langword="false"/></returns>
        /// <example><code>
        /// bool isPublisher = Publisher.TryParse("1;Publisher name;www.website.com", out Publisher p1);
        /// Publisher p2 = null; Publisher.TryParse("2$Publisher name", out p2, "$");
        /// if (Publisher.TryParse("3;Publisher name;www.website.com", out Publisher p3, ";", false)) { }
        /// </code></example>
        public static bool TryParse(string data, out Publisher publisher, string splitString = ";", bool validate = true)
        {
            publisher = null;

            try { publisher = Parse(data, splitString, validate); return true; }
            catch { return false; }
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            if (Website != null)
                return $"[#{PublisherID}]{PublisherName} ({Website})";
            else
                return $"[#{PublisherID}]{PublisherName}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not Publisher publisher) return false;
            else return Equals(publisher);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(PublisherName, Website);
        }


        /// <inheritdoc/>
        public int CompareTo(Publisher other)
        {
            int comparer = Comparer.Default.Compare(PublisherName, other.PublisherName);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Website, other.Website);
            return comparer;
        }
        /// <inheritdoc/>
        public int CompareTo(string other)
        {
            if (!TryParse(other, out var otherBook))
                return Comparer.Default.Compare(ToString(), other.ToString());
            else
                return CompareTo(otherBook);
        }
        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            if (obj is not Book)
                return CompareTo(obj.ToString());
            else
                return CompareTo(obj as Book);
        }

        /// <inheritdoc/>
        public bool Equals(Publisher other)
        {
            if (other == null) return false;
            else if (ReferenceEquals(this, other)) return true;
            else if (PublisherName != other.PublisherName) return false;
            else if (!Books.SequenceEqual(other.Books)) return false;
            else if (Website != other.Website) return false;
            else return true;
        }
    }
}
