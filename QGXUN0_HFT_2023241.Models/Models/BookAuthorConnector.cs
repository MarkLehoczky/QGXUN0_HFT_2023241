using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QGXUN0_HFT_2023241.Models.Models
{
    /// <summary>
    /// Specifies the <see cref="Models.Book"/> and <see cref="Models.Author"/> connection of a <see cref="BookAuthorConnector"/>
    /// </summary>
    public class BookAuthorConnector : IComparable<BookAuthorConnector>, IComparable<string>, IComparable, IEquatable<BookAuthorConnector>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Publisher"/>
        /// </summary>
        /// <remarks>The value of this property is used as the <see langword="unique key"/> in the database</remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("BookAuthorConnectorID")]
        [JsonProperty("BookAuthorConnectorID")]
        [Required]
        [Key]
        public int BookAuthorConnectorID { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Models.Book"/>
        /// </summary>
        [JsonPropertyName("BookID")]
        [JsonProperty("BookID")]
        [Required]
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Models.Author"/>
        /// </summary>
        [JsonPropertyName("AuthorID")]
        [JsonProperty("AuthorID")]
        [Required]
        public int AuthorID { get; set; }

        /// <summary>
        /// Gets the instance of the <see cref="Models.Book"/>
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Book Book { get; private set; }

        /// <summary>
        /// Gets the instance of the <see cref="Models.Author"/>
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Author Author { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/>.
        /// </summary>
        public BookAuthorConnector() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/> by using the required properties.
        /// </summary>
        /// <param name="bookAuthorConnectorID">Unique ID of the <see cref="BookAuthorConnector"/></param>
        /// <param name="bookID">Unique ID of a <see cref="Models.Book"/> instance of the <see cref="BookAuthorConnector"/></param>
        /// <param name="authorID">Unique ID of a <see cref="Models.Author"/> instance of the <see cref="BookAuthorConnector"/></param>
        public BookAuthorConnector(int bookAuthorConnectorID, int bookID, int authorID)
        {
            BookAuthorConnectorID = bookAuthorConnectorID;
            BookID = bookID;
            AuthorID = authorID;
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            return $"[#{BookAuthorConnectorID}] {Book} - {Author}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not BookAuthorConnector connector) return false;
            else return Equals(connector);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Book, Author);
        }


        /// <inheritdoc/>
        public int CompareTo(BookAuthorConnector other)
        {
            int comparer = Comparer.Default.Compare(Book, other.Book);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Author, other.Author);
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
            if (obj is not BookAuthorConnector)
                return Comparer.Default.Compare(ToString(), obj.ToString());
            else
                return CompareTo(obj as BookAuthorConnector);
        }

        /// <inheritdoc/>
        public bool Equals(BookAuthorConnector other)
        {
            if (other == null) return false;
            else if (!Book.Equals(other.Book)) return false;
            else if (!Author.Equals(other.Author)) return false;
            else return true;
        }
    }
}
