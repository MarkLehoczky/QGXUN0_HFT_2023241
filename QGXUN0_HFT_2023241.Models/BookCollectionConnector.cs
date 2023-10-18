using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QGXUN0_HFT_2023241.Models
{
    /// <summary>
    /// Connector for the <see cref="Models.Book"></see> and <see cref="Models.Collection"></see> instances
    /// </summary>
    public class BookCollectionConnector
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int BookCollectionConnectorID { get; set; }

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


        ///<inheritdoc/>
        public override string ToString()
        {
            return $"{Book};{Collection}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not BookCollectionConnector) return false;
            return Book == (obj as BookCollectionConnector).Book && Collection == (obj as BookCollectionConnector).Collection;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}