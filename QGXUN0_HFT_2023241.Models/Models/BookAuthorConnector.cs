using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QGXUN0_HFT_2023241.Models.Models
{
    /// <summary>
    /// Connector for the <see cref="Models.Book"></see> and <see cref="Models.Author"></see> instances
    /// </summary>
    public class BookAuthorConnector : IComparable<BookAuthorConnector>, IComparable<string>, IComparable
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int BookAuthorConnectorID { get; set; }

        /// <summary>
        /// ID of the <see cref="Models.Book"/>
        /// </summary>
        [Required] public int BookID { get; set; }

        /// <summary>
        /// ID of the <see cref="Models.Author"/>
        /// </summary>
        [Required] public int AuthorID { get; set; }

        /// <summary>
        /// Book instance
        /// </summary>
        public virtual Book Book { get; private set; }

        /// <summary>
        /// Author instance
        /// </summary>
        public virtual Author Author { get; private set; }


        /// <summary>
        /// Empty constructor
        /// </summary>
        public BookAuthorConnector() { }
        /// <summary>
        /// Constructor with required property values
        /// </summary>
        /// <param name="bookAuthorConnectorID">Unique key</param>
        /// <param name="bookID">ID of the book</param>
        /// <param name="authorID">ID of the author</param>
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
            if (obj == null || obj is not BookAuthorConnector) return false;
            else if (Book != (obj as BookAuthorConnector).Book) return false;
            else if (Author != (obj as BookAuthorConnector).Author) return false;
            else return true;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
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
    }
}
