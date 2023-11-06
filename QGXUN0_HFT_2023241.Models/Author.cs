using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace QGXUN0_HFT_2023241.Models
{
    /// <summary>
    /// Contains a author's name and books
    /// </summary>
    public class Author : IComparable<Author>, IComparable<string>, IComparable
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int AuthorID { get; set; }

        /// <summary>
        /// Name of the author
        /// </summary>
        [Required][StringLength(50)] public string AuthorName { get; set; }

        /// <summary>
        /// Books of the author
        /// </summary>
        [Required] public virtual ICollection<Book> Books { get; set; }
        /// <summary>
        /// Connector for the <see cref="Book"></see> and <see cref="Author"></see> instances
        /// </summary>
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
                author.Validate();

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
            if (obj == null || obj is not Author) return false;
            else if (AuthorName != (obj as Author).AuthorName) return false;
            else if (Books != (obj as Author).Books) return false;
            else return true;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }


        /// <inheritdoc/>
        public int CompareTo(Author other)
        {
            int comparer = Comparer.Default.Compare(AuthorName, other.AuthorName);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Books, other.Books);
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
                return Comparer.Default.Compare(ToString(), obj.ToString());
            else
                return CompareTo(obj as Book);
        }
    }
}
