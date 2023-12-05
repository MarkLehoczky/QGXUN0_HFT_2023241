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
    /// Contains a author's name and books
    /// </summary>
    public class Author : IComparable<Author>, IComparable<string>, IComparable, IEquatable<Author>
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [JsonPropertyName("AuthorID")]
        [JsonProperty("AuthorID")]
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorID { get; set; }

        /// <summary>
        /// Name of the author
        /// </summary>
        [JsonPropertyName("AuthorName")]
        [JsonProperty("AuthorName")]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string AuthorName { get; set; }

        /// <summary>
        /// Books of the author
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        /// <summary>
        /// Connector for the <see cref="Book"></see> and <see cref="Author"></see> instances
        /// </summary>
        [JsonPropertyName("BookConnector")]
        [JsonProperty("BookConnector")]
        public virtual ICollection<BookAuthorConnector> BookConnector { get; set; }


        /// <summary>
        /// Empty constructor
        /// </summary>
        public Author() { }
        /// <summary>
        /// Constructor with required property values
        /// </summary>
        /// <param name="authorID">Unique key</param>
        /// <param name="authorName">Name of the author</param>
        public Author(int authorID, string authorName)
        {
            AuthorID = authorID;
            AuthorName = authorName;
        }


        /// <summary>
        /// Converts a <see cref="string"/> representation of a <see cref="Author"/> instance to a <see cref="Author"/> object
        /// </summary>
        /// <param name="data">Parsable data</param>
        /// <param name="splitString">Splitting string (default = ";")</param>
        /// <param name="restrictionCheck">Check for attribute restrictions (default = false)</param>
        /// <returns><see cref="Author"/> representation of the <paramref name="data"/> <see cref="string"/></returns>
        /// <exception cref="ArgumentException">Error during parsing</exception>
        /// <example><code>
        /// Author a1 = Author.Parse("1;Author name");
        /// Author a2 = Author.Parse("2$Author name", "$");
        /// Author a3 = Author.Parse("3;Author name", ";", true);
        /// </code></example>
        public static Author Parse(string data, string splitString = ";", bool restrictionCheck = false)
        {
            string[] splitData = data.Split(splitString);
            if (splitData.Length < 2)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var authorID))
                throw new ArgumentException("The 'AuthorID' property cannot be parsed to an 'int' type", nameof(data));

            string authorName = splitData[1];

            Author author = new Author(authorID, authorName);

            if (restrictionCheck)
                author.Validate(typeof(RequiredAttribute));

            return author;
        }

        /// <summary>
        /// Attempts to convert a <see cref="string"/> representation of a <see cref="Author"/> instance to a <see cref="Author"/> object
        /// </summary>
        /// <param name="data">Parsable data</param>
        /// <param name="author"><see cref="Author"/> representation of the <paramref name="data"/> <see cref="string"/> or <see langword="null"/></param>
        /// <param name="splitString">Splitting string (default = ";")</param>
        /// <param name="restrictionCheck">Check for attribute restrictions (default = false)</param>
        /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/></returns>
        /// <example><code>
        /// bool Author = Author.TryParse("1;Author name", out Collection a1);
        /// Author a2 = null; Author.TryParse("2$Author name$true", out a2, "$");
        /// if (!Author.TryParse("3;Author name;false", out Author a3, ";", true)) { }
        /// </code></example>
        public static bool TryParse(string data, out Author author, string splitString = ";", bool restrictionCheck = false)
        {
            author = null;

            try { author = Parse(data, splitString, restrictionCheck); return true; }
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
