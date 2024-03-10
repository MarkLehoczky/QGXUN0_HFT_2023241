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
    /// Specifies the unique ID and name of an <see cref="Author"/>.
    /// </summary>
    public class Author : IComparable<Author>, IComparable<string>, IComparable, IEquatable<Author>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Author"/>.
        /// </summary>
        /// <remarks>The value of this property is used as the <see langword="unique key"/> in the database</remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("AuthorID")]
        [JsonProperty("AuthorID")]
        [Required]
        [Key]
        public int AuthorID { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="Author"/>.
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [JsonPropertyName("AuthorName")]
        [JsonProperty("AuthorName")]
        [Required]
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the books of the <see cref="Author"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        /// <summary>
        /// Gets or sets the connector for the books of the <see cref="Author"/>.
        /// </summary>
        [JsonPropertyName("BookConnector")]
        [JsonProperty("BookConnector")]
        public virtual ICollection<BookAuthorConnector> BookConnector { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> <see langword="class"/>.
        /// </summary>
        public Author() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> <see langword="class"/> by using the required properties.
        /// </summary>
        /// <param name="authorID">Unique ID of the <see cref="Author"/></param>
        /// <param name="authorName">Name of the <see cref="Author"/></param>
        public Author(int authorID, string authorName)
        {
            AuthorID = authorID;
            AuthorName = authorName;
        }


        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Author"/>.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Author"/> to convert</param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see cref="Author"/> representation of the <paramref name="data"/> <see cref="string"/></returns>
        /// <exception cref="ArgumentException">An error occurred during parsing</exception>
        /// <exception cref="ValidationException">The specified <paramref name="data"/> is not valid</exception>
        /// <example><code>
        /// Author a1 = Author.Parse("1;Author name");
        /// Author a2 = Author.Parse("2$Author name", "$");
        /// Author a3 = Author.Parse("3;Author name", ";", false);
        /// </code></example>
        public static Author Parse(string data, string splitString = ";", bool validate = true)
        {
            string[] splitData = data.Split(splitString);
            if (splitData.Length < 2)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var authorID))
                throw new ArgumentException("The 'AuthorID' property cannot be parsed to an 'int' type", nameof(data));

            string authorName = splitData[1];

            Author author = new Author(authorID, authorName);
            if (validate) author.Validate();

            return author;
        }

        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Author"/>. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Author"/> to convert</param>
        /// <param name="author"><see cref="Author"/> representation of the <paramref name="data"/> if the parsing was successful; otherwise, <see langword="null"/></param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see langword="true"/> if the parsing was successful; otherwise, <see langword="false"/></returns>
        /// <example><code>
        /// bool Author = Author.TryParse("1;Author name", out Author a1);
        /// Author a2 = null; Author.TryParse("2$Author name$true", out a2, "$");
        /// if (!Author.TryParse("3;Author name;false", out Author a3, ";", false)) { }
        /// </code></example>
        public static bool TryParse(string data, out Author author, string splitString = ";", bool validate = true)
        {
            author = null;

            try { author = Parse(data, splitString, validate); return true; }
            catch { return false; }
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            return $"[#{AuthorID}]{AuthorName}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not Author author) return false;
            else return Equals(author);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AuthorName);
        }


        /// <inheritdoc/>
        public int CompareTo(Author other)
        {
            int comparer = Comparer.Default.Compare(AuthorName, other.AuthorName);
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
        public bool Equals(Author other)
        {
            if (other == null) return false;
            else if (ReferenceEquals(this, other)) return true;
            else if (AuthorName != other.AuthorName) return false;
            else if (!Books.SequenceEqual(other.Books)) return false;
            else return true;
        }
    }
}
