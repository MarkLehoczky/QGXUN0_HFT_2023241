using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace QGXUN0_HFT_2023241.Models
{
    /// <summary>
    /// Contains a collection's name, books, and optionally whether it is a series
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int CollectionID { get; set; }

        /// <summary>
        /// Name of the collection
        /// </summary>
        [Required][StringLength(50)] public string CollectionName { get; set; }

        /// <summary>
        /// Books of the collection
        /// </summary>
        [Required] public virtual ICollection<Book> Books { get; set; }
        /// <summary>
        /// Connector for the <see cref="Book"></see> and <see cref="Collection"></see> instances
        /// </summary>
        [Required] public virtual ICollection<BookCollectionConnector> BookConnector { get; set; }

        /// <summary>
        /// <see langword="true"/> if the collection is a book series, otherwise <see langword="false"/>
        /// </summary>
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

            if (restrictionCheck)
            {
                StringLengthAttribute collectionNameAttribute = typeof(Collection).GetProperty("CollectionName").GetCustomAttributes<StringLengthAttribute>(false).FirstOrDefault();
                if (collectionName.Length == 0)
                    throw new ArgumentException("The 'CollectionName' property is empty", nameof(data));
                else if (collectionName.Length > collectionNameAttribute.MaximumLength)
                    throw new ArgumentException("The 'CollectionName' property's length is longer than the maximum length attribute", nameof(data));
            }

            if (hasIsSeries)
                return new Collection(collectionID, collectionName, isSeries);
            else
                return new Collection(collectionID, collectionName);
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
            return $"[#{CollectionID}]{CollectionName}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Collection) return false;
            return CollectionName == (obj as Collection).CollectionName;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
