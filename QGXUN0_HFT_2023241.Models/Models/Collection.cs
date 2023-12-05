using Newtonsoft.Json;
using QGXUN0_HFT_2023241.Models.Attributes;
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
    /// Contains a collection's name, books, and optionally whether it is a series
    /// </summary>
    public class Collection : IComparable<Collection>, IComparable<string>, IComparable, IEquatable<Collection>
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [JsonPropertyName("CollectionID")]
        [JsonProperty("CollectionID")]
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionID { get; set; }

        /// <summary>
        /// Name of the collection
        /// </summary>
        [JsonPropertyName("CollectionName")]
        [JsonProperty("CollectionName")]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string CollectionName { get; set; }

        /// <summary>
        /// Books of the collection
        /// </summary>
        [JsonPropertyName("Books")]
        [JsonProperty("Books")]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        /// <summary>
        /// Connector for the <see cref="Book"></see> and <see cref="Collection"></see> instances
        /// </summary>
        [JsonPropertyName("BookConnector")]
        [JsonProperty("BookConnector")]
        public virtual ICollection<BookCollectionConnector> BookConnector { get; set; }

        /// <summary>
        /// <see langword="true"/> if the collection is a book series, otherwise <see langword="false"/>
        /// </summary>
        [JsonPropertyName("IsSeries")]
        [JsonProperty("IsSeries")]
        public bool? IsSeries { get; set; }


        /// <summary>
        /// Empty constructor
        /// </summary>
        public Collection() { }
        /// <summary>
        /// Constructor with required property values
        /// </summary>
        /// <param name="collectionID">Unique key</param>
        /// <param name="collectionName">Name of the collection</param>
        public Collection(int collectionID, string collectionName)
        {
            CollectionID = collectionID;
            CollectionName = collectionName;
        }
        /// <summary>
        /// Constructor with required and optional property values
        /// </summary>
        /// <param name="collectionID">Unique CollectionID key</param>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="isSeries">Check whether the bok collection is a series</param>
        public Collection(int collectionID, string collectionName, bool? isSeries)
        {
            CollectionID = collectionID;
            CollectionName = collectionName;
            IsSeries = isSeries;
        }


        /// <summary>
        /// Converts a <see cref="string"/> representation of a <see cref="Collection"/> instance to a <see cref="Collection"/> object
        /// </summary>
        /// <param name="data">Parsable data</param>
        /// <param name="splitString">Splitting string (default = ";")</param>
        /// <param name="restrictionCheck">Check for attribute restrictions (default = false)</param>
        /// <returns><see cref="Collection"/> representation of the <paramref name="data"/> <see cref="string"/></returns>
        /// <exception cref="ArgumentException">Error during parsing</exception>
        /// <example><code>
        /// Collection c1 = Collection.Parse("1;Collection name");
        /// Collection c2 = Collection.Parse("2$Collection name$true", "$");
        /// Collection c3 = Collection.Parse("3;Collection name;false", ";", true);
        /// </code></example>
        public static Collection Parse(string data, string splitString = ";", bool restrictionCheck = false)
        {
            string[] splitData = data.Split(splitString);

            if (splitData.Length < 2)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var collectionID))
                throw new ArgumentException("The 'CollectionID' property cannot be parsed to an 'int' type", nameof(data));

            string collectionName = splitData[1];

            bool isSeries = false;
            bool hasIsSeries = splitData.Length > 2;
            if (hasIsSeries && !bool.TryParse(splitData[2], out isSeries))
                throw new ArgumentException("The 'IsSeries' property cannot be parsed to an 'bool' type", nameof(data));

            Collection collection;

            if (hasIsSeries)
                collection = new Collection(collectionID, collectionName, isSeries);
            else
                collection = new Collection(collectionID, collectionName);

            if (restrictionCheck)
                collection.Validate();

            return collection;
        }

        /// <summary>
        /// Attempts to convert a <see cref="string"/> representation of a <see cref="Collection"/> instance to a <see cref="Collection"/> object
        /// </summary>
        /// <param name="data">Parsable data</param>
        /// <param name="collection"><see cref="Collection"/> representation of the <paramref name="data"/> <see cref="string"/> or <see langword="null"/></param>
        /// <param name="splitString">Splitting string (default = ";")</param>
        /// <param name="restrictionCheck">Check for attribute restrictions (default = false)</param>
        /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/></returns>
        /// <example><code>
        /// bool Collection = Collection.TryParse("1;Collection name", out Collection c1);
        /// Collection c2 = null; Collection.TryParse("2$Collection name$true", out c2, "$");
        /// if (!Collection.TryParse("3;Collection name;false", out Collection c3, ";", true)) { }
        /// </code></example>
        public static bool TryParse(string data, out Collection collection, string splitString = ";", bool restrictionCheck = false)
        {
            collection = null;

            try { collection = Parse(data, splitString, restrictionCheck); return true; }
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
