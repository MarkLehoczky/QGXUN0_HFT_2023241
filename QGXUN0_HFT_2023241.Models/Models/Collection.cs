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
    /// Specifies the unique ID, name and series status of a <see cref="Collection"/>.
    /// </summary>
    public class Collection : IComparable<Collection>, IComparable<string>, IComparable, IEquatable<Collection>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Collection"/>.
        /// </summary>
        /// <remarks>The value of this property is used as the <see langword="unique key"/> in the database</remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("CollectionID")]
        [JsonProperty("CollectionID")]
        [Required]
        [Key]
        public int CollectionID { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="Collection"/>.
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [JsonPropertyName("CollectionName")]
        [JsonProperty("CollectionName")]
        [Required]
        public string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the books of the <see cref="Collection"/>.
        /// </summary>
        [JsonPropertyName("Books")]
        [JsonProperty("Books")]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        /// <summary>
        /// Gets or sets the connector for the books of the <see cref="Collection"/>.
        /// </summary>
        [JsonPropertyName("BookConnector")]
        [JsonProperty("BookConnector")]
        public virtual ICollection<BookCollectionConnector> BookConnector { get; set; }

        /// <summary>
        /// Gets or sets the series status of the <see cref="Collection"/>.
        /// </summary>
        /// <value><see langword="true"/> if the collection is a book series; otherwise, <see langword="false"/> or <see langword="null"/></value>
        [JsonPropertyName("IsSeries")]
        [JsonProperty("IsSeries")]
        public bool? IsSeries { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Collection"/> <see langword="class"/>.
        /// </summary>
        public Collection() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Collection"/> <see langword="class"/> by using the required properties.
        /// </summary>
        /// <param name="collectionID">Unique ID of the <see cref="Collection"/></param>
        /// <param name="collectionName">Name of the <see cref="Collection"/></param>
        public Collection(int collectionID, string collectionName)
        {
            CollectionID = collectionID;
            CollectionName = collectionName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Collection"/> <see langword="class"/> by using the required and optional properties.
        /// </summary>
        /// <param name="collectionID">Unique ID of the <see cref="Collection"/></param>
        /// <param name="collectionName">Name of the <see cref="Collection"/></param>
        /// <param name="isSeries">Series status of the <see cref="Collection"/></param>
        public Collection(int collectionID, string collectionName, bool? isSeries)
        {
            CollectionID = collectionID;
            CollectionName = collectionName;
            IsSeries = isSeries;
        }


        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Collection"/>.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Collection"/> to convert</param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see cref="Collection"/> representation of the <paramref name="data"/> <see cref="string"/></returns>
        /// <exception cref="ArgumentException">An error occurred during parsing</exception>
        /// <exception cref="ValidationException">The specified <paramref name="data"/> is not valid</exception>
        /// <example><code>
        /// Collection c1 = Collection.Parse("1;Collection name");
        /// Collection c2 = Collection.Parse("2$Collection name$true", "$");
        /// Collection c3 = Collection.Parse("3;Collection name;false", ";", false);
        /// </code></example>
        public static Collection Parse(string data, string splitString = ";", bool validate = true)
        {
            string[] splitData = data.Split(splitString);

            if (splitData.Length < 2)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var collectionID))
                throw new ArgumentException("The 'CollectionID' property cannot be parsed to an 'int' type", nameof(data));

            string collectionName = splitData[1];

            bool? isSeries = null;
            if (splitData.Length > 2 && bool.TryParse(splitData[2], out bool temp1)) isSeries = temp1;
            else if (splitData.Length > 2 && splitData[2] != "")
                throw new ArgumentException("The 'IsSeries' property cannot be parsed to a 'bool' type", nameof(data));

            Collection collection = new Collection(collectionID, collectionName, isSeries);
            if (validate) collection.Validate();

            return collection;
        }

        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Collection"/>. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Collection"/> to convert</param>
        /// <param name="collection"><see cref="Collection"/> representation of the <paramref name="data"/> if the parsing was successful; otherwise, <see langword="null"/></param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see langword="true"/> if the parsing was successful; otherwise, <see langword="false"/></returns>
        /// <example><code>
        /// bool Collection = Collection.TryParse("1;Collection name", out Collection c1);
        /// Collection c2 = null; Collection.TryParse("2$Collection name$true", out c2, "$");
        /// if (!Collection.TryParse("3;Collection name;false", out Collection c3, ";", true)) { }
        /// </code></example>
        public static bool TryParse(string data, out Collection collection, string splitString = ";", bool validate = true)
        {
            collection = null;

            try { collection = Parse(data, splitString, validate); return true; }
            catch { return false; }
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            if (IsSeries.HasValue)
                return $"[#{CollectionID}]{CollectionName} ({IsSeries})";
            else
                return $"[#{CollectionID}]{CollectionName}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not Collection collection) return false;
            else return Equals(collection);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(CollectionName, Books, IsSeries);
        }


        /// <inheritdoc/>
        public int CompareTo(Collection other)
        {
            int comparer = Comparer.Default.Compare(CollectionName, other.CollectionName);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(IsSeries, other.IsSeries);
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
        public bool Equals(Collection other)
        {
            if (other == null) return false;
            else if (ReferenceEquals(this, other)) return true;
            else if (CollectionName != other.CollectionName) return false;
            else if (!Books.SequenceEqual(other.Books)) return false;
            else if (IsSeries != other.IsSeries) return false;
            else return true;
        }
    }
}
