using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QGXUN0_HFT_2023241.Models
{
    /// <summary>
    /// Connector for the <see cref="Models.Book"></see> and <see cref="Models.Author"></see> instances
    /// </summary>
    public class BookAuthorConnector
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
            return $"{Book};{Author}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not BookAuthorConnector) return false;
            return Book == (obj as BookAuthorConnector).Book && Author == (obj as BookAuthorConnector).Author;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
