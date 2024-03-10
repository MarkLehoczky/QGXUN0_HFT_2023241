using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QGXUN0_HFT_2023241.Models.Models
{
    /// <summary>
    /// Specifies the <see cref="Models.Book"/> and <see cref="Models.Collection"/> connection of a <see cref="BookCollectionConnector"/>
    /// </summary>
    public class BookCollectionConnector : IComparable<BookCollectionConnector>, IComparable<string>, IComparable, IEquatable<BookCollectionConnector>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Publisher"/>
        /// </summary>
        /// <remarks>The value of this property is used as the <see langword="unique key"/> in the database</remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("BookCollectionConnectorID")]
        [JsonProperty("BookCollectionConnectorID")]
        [Required]
        [Key]
        public int BookCollectionConnectorID { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Models.Book"/>
        /// </summary>
        [JsonPropertyName("BookID")]
        [JsonProperty("BookID")]
        [Required]
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Models.Collection"/>
        /// </summary>
        [JsonPropertyName("CollectionID")]
        [JsonProperty("CollectionID")]
        [Required]
        public int CollectionID { get; set; }

        /// <summary>
        /// Gets the instance of the <see cref="Models.Book"/>
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Book Book { get; private set; }

        /// <summary>
        /// Gets the instance of the <see cref="Models.Collection"/>
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Collection Collection { get; private set; }

        /// <summary>
        /// Position of the book in a series
        /// </summary>
        [JsonPropertyName("PositionInSeries")]
        [JsonProperty("PositionInSeries")]
        public int? PositionInSeries { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/>.
        /// </summary>
        public BookCollectionConnector() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/> by using the required properties.
        /// </summary>
        /// <param name="bookCollectionConnectorID">Unique ID of the <see cref="BookCollectionConnector"/></param>
        /// <param name="bookID">Unique ID of a <see cref="Models.Book"/> instance of the <see cref="BookCollectionConnector"/></param>
        /// <param name="collectionID">Unique ID of a <see cref="Models.Collection"/> instance of the <see cref="BookCollectionConnector"/></param>
        public BookCollectionConnector(int bookCollectionConnectorID, int bookID, int collectionID)
        {
            BookCollectionConnectorID = bookCollectionConnectorID;
            BookID = bookID;
            CollectionID = collectionID;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BookCollectionConnector"/> <see langword="class"/> by using the required and optional properties.
        /// </summary>
        /// <param name="bookCollectionConnectorID">Unique ID of the <see cref="BookCollectionConnector"/></param>
        /// <param name="bookID">Unique ID of a <see cref="Models.Book"/> instance of the <see cref="BookCollectionConnector"/></param>
        /// <param name="collectionID">Unique ID of a <see cref="Models.Collection"/> instance of the <see cref="BookCollectionConnector"/></param>
        /// <param name="positionInSeries">Position of the <see cref="Models.Book"/> in the series of the <see cref="BookCollectionConnector"/></param>
        public BookCollectionConnector(int bookCollectionConnectorID, int bookID, int collectionID, int? positionInSeries)
        {
            BookCollectionConnectorID = bookCollectionConnectorID;
            BookID = bookID;
            CollectionID = collectionID;
            PositionInSeries = positionInSeries;
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            return $"[#{BookCollectionConnectorID}] {Book} - {Collection}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not BookCollectionConnector connector) return false;
            else return Equals(connector);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Book, Collection);
        }


        /// <inheritdoc/>
        public int CompareTo(BookCollectionConnector other)
        {
            int comparer = Comparer.Default.Compare(Book, other.Book);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Collection, other.Collection);
            return comparer;
        }
        /// <inheritdoc/>
        public int CompareTo(string other)
        {
            return CompareTo(other as object);
        }
        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            if (obj is not BookCollectionConnector)
                return Comparer.Default.Compare(ToString(), obj.ToString());
            else
                return CompareTo(obj as BookCollectionConnector);
        }

        /// <inheritdoc/>
        public bool Equals(BookCollectionConnector other)
        {
            if (other == null) return false;
            else if (!Book.Equals(other.Book)) return false;
            else if (!Collection.Equals(other.Collection)) return false;
            else return true;
        }
    }
}
