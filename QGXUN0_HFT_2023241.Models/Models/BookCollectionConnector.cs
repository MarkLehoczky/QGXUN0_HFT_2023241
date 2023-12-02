using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QGXUN0_HFT_2023241.Models.Models
{
    /// <summary>
    /// Connector for the <see cref="Models.Book"></see> and <see cref="Models.Collection"></see> instances
    /// </summary>
    public class BookCollectionConnector : IComparable<BookCollectionConnector>, IComparable<string>, IComparable
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int BookCollectionConnectorID { get; set; }

        /// <summary>
        /// ID of the <see cref="Models.Book"/>
        /// </summary>
        [Required] public int BookID { get; set; }

        /// <summary>
        /// ID of the <see cref="Models.Collection"/>
        /// </summary>
        [Required] public int CollectionID { get; set; }

        /// <summary>
        /// Book instance
        /// </summary>
        public virtual Book Book { get; private set; }

        /// <summary>
        /// Collection instance
        /// </summary>
        public virtual Collection Collection { get; private set; }

        /// <summary>
        /// Position of the book in a series
        /// </summary>
        public int? PositionInSeries { get; set; }


        /// <summary>
        /// Empty constructor
        /// </summary>
        public BookCollectionConnector() { }
        /// <summary>
        /// Constructor with required property values
        /// </summary>
        /// <param name="bookCollectionConnectorID">Unique key</param>
        /// <param name="bookID">ID of the book</param>
        /// <param name="collectionID">ID of the collection</param>
        public BookCollectionConnector(int bookCollectionConnectorID, int bookID, int collectionID)
        {
            BookCollectionConnectorID = bookCollectionConnectorID;
            BookID = bookID;
            CollectionID = collectionID;
        }
        /// <summary>
        /// Constructor with required and optional property values
        /// </summary>
        /// <param name="bookCollectionConnectorID">Unique key</param>
        /// <param name="bookID">ID of the book</param>
        /// <param name="collectionID">ID of the collection</param>
        /// <param name="positionInSeries">position of the book in the series</param>
        public BookCollectionConnector(int bookCollectionConnectorID, int bookID, int collectionID, int positionInSeries)
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
            if (obj == null || obj is not BookCollectionConnector) return false;
            else if (Book != (obj as BookCollectionConnector).Book) return false;
            else if (Collection != (obj as BookCollectionConnector).Collection) return false;
            else return true;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
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
    }
}