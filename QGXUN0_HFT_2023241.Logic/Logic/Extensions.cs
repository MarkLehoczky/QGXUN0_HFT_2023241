using QGXUN0_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Logic
{
    /// <summary>
    /// Filter options for an <see cref="IEnumerable{Book}"/> collection
    /// </summary>
    public enum BookFilter { MostExpensive, LeastExpensive, HighestRated, LowestRated }

    /// <summary>
    /// Filter options for an <see cref="Collection"/>
    /// </summary>
    public enum CollectionFilter { Series, NonSeries, Collection }


    /// <summary>
    /// An extended version of the <see cref="Collection"/> model
    /// </summary>
    public class ExtendedCollection : Collection
    {
        /// <summary>
        /// Authors of the collection
        /// </summary>
        public virtual ICollection<Author> Authors { get; set; }

        /// <summary>
        /// Total price of the collection
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Average rating of the collection
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        /// Constructor with the required parameters
        /// </summary>
        /// <param name="collection">collection</param>
        /// <param name="authors">authors of the collection</param>
        /// <param name="price">price of the collection</param>
        /// <param name="rating">rating of the collection</param>
        public ExtendedCollection(Collection collection, IEnumerable<Author> authors, double? price, double? rating)
        {
            base.CollectionID = collection.CollectionID;
            base.CollectionName = collection.CollectionName;
            base.Books = collection.Books;
            base.BookConnector = collection.BookConnector;
            base.IsSeries = collection.IsSeries;
            this.Authors = authors.ToList();
            this.Price = price;
            this.Rating = rating;
        }
    }

    /// <summary>
    /// An extended version of the <see cref="Publisher"/> model
    /// </summary>
    public class ExtendedPublisher : Publisher
    {
        /// <summary>
        /// Authors of the publisher
        /// </summary>
        public virtual ICollection<Author> Authors { get; set; }

        /// <summary>
        /// Average rating of the publisher
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        /// Constructor with the required parameters
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <param name="authors">authors of the publisher</param>
        /// <param name="rating">rating of the publisher</param>
        public ExtendedPublisher(Publisher publisher, IEnumerable<Author> authors, double? rating)
        {
            base.PublisherID = publisher.PublisherID;
            base.PublisherName = publisher.PublisherName;
            base.Website = publisher.Website;
            base.Books = publisher.Books;
            this.Authors = authors.ToList();
            this.Rating = rating;
        }
    }
}
