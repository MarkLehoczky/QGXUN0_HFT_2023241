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
    /// Contains a book's title, authors, release year, and optionally the publisher, collections, ISBN number
    /// </summary>
    public class Book : IComparable<Book>, IComparable<string>, IComparable
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Required][Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int BookID { get; set; }

        /// <summary>
        /// Title of the book
        /// </summary>
        [Required][StringLength(200)] public string Title { get; set; }

        /// <summary>
        /// Authors groups of the book
        /// </summary>
        [Required] public virtual ICollection<Author> Authors { get; set; }
        /// <summary>
        /// Connector for the <see cref="Book"></see> and <see cref="Author"></see> instances
        /// </summary>
        public virtual ICollection<BookAuthorConnector> AuthorConnector { get; set; }

        /// <summary>
        /// Release year of the book
        /// </summary>
        [Required][Range(1950, 2050)] public int Year { get; set; }

        /// <summary>
        /// ID of the <see cref="Models.Publisher"></see>
        /// </summary>
        public int? PublisherID { get; set; }

        /// <summary>
        /// <see cref="Models.Publisher"></see> of the book
        /// </summary>
        public virtual Publisher Publisher { get; set; }

        /// <summary>
        /// <see cref="Collection"></see> groups which contains the book
        /// </summary>
        public virtual ICollection<Collection> Collections { get; set; }
        /// <summary>
        /// Connector for the <see cref="Book"></see> and <see cref="Models.Collection"></see> instances
        /// </summary>
        public virtual ICollection<BookCollectionConnector> CollectionConnector { get; set; }

        /// <summary>
        /// Price of the book
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Rating of the book
        /// </summary>
        [Range(1.0, 5.0)] public double? Rating { get; set; }


        /// <summary>
        /// Empty constructor
        /// </summary>
        public Book() { }
        /// <summary>
        /// Constructor with required property values
        /// </summary>
        /// <param name="bookID">Unique key</param>
        /// <param name="title">Title</param>
        /// <param name="year">Release year</param>
        public Book(int bookID, string title, int year)
        {
            BookID = bookID;
            Title = title;
            Year = year;
        }
        /// <summary>
        /// Constructor with required and optional property values
        /// </summary>
        /// <param name="bookID">Unique bookID key</param>
        /// <param name="title">Title</param>
        /// <param name="year">Release year</param>
        /// <param name="publisherID">ID of the <see cref="Models.Publisher"></param>
        public Book(int bookID, string title, int year, int publisherID)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
        }
        /// <summary>
        /// Constructor with required and optional property values
        /// </summary>
        /// <param name="bookID">Unique bookID key</param>
        /// <param name="title">Title</param>
        /// <param name="year">Release year</param>
        /// <param name="publisherID">ID of the <see cref="Models.Publisher"></see></param>
        /// <param name="price">price</param>
        public Book(int bookID, string title, int year, int publisherID, double price)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
            Price = price;
        }
        /// <summary>
        /// Constructor with required and optional property values
        /// </summary>
        /// <param name="bookID">Unique bookID key</param>
        /// <param name="title">Title</param>
        /// <param name="year">Release year</param>
        /// <param name="publisherID">ID of the <see cref="Models.Publisher"></see></param>
        /// <param name="price">price</param>
        /// <param name="rating">rating</param>
        public Book(int bookID, string title, int year, int publisherID, double price, double rating)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
            Price = price;
            Rating = rating;
        }


        /// <summary>
        /// Converts a <see cref="string"/> representation of a <see cref="Collection"/> instance to a <see cref="Collection"/> object
        /// </summary>
        /// <param name="data">Parsable data</param>
        /// <param name="splitString">Splitting string (default = ";")</param>
        /// <param name="restrictionCheck">Check for attribute restrictions (default = false)</param>
        /// <returns><see cref="Collection"/> representation of the <paramref name="data"/> <see cref="string"/></returns>
        /// <exception cref="ArgumentException">Error during parsing</exception>
        /// <example><code>
        /// Collection b1 = Collection.Parse("1;Title;2015");
        /// Collection b2 = Collection.Parse("2$Title$2015$5", "$");
        /// Collection b3 = Collection.Parse("3;Title;2015;5;15.99", ";", true);
        /// </code></example>
        public static Book Parse(string data, string splitString = ";", bool restrictionCheck = false)
        {
            string[] splitData = data.Split(splitString);

            if (splitData.Length < 3)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var bookID))
                throw new ArgumentException("The 'BookID' property cannot be parsed to an 'int' type", nameof(data));

            string title = splitData[1];

            if (!int.TryParse(splitData[2], out var year))
                throw new ArgumentException("The 'Year' property cannot be parsed to an 'int' type", nameof(data));

            bool hasPublisher = splitData.Length > 3;
            int publisherID = 0;
            if (hasPublisher && !int.TryParse(splitData[3], out publisherID))
                throw new ArgumentException("The 'PublisherID' property cannot be parsed to an 'int' type", nameof(data));

            bool hasPrice = splitData.Length > 4;
            double price = 0;
            if (hasPrice && !double.TryParse(splitData[4], out price))
                throw new ArgumentException("The 'Price' property cannot be parsed to an 'double' type", nameof(data));

            bool hasRating = splitData.Length > 5;
            double rating = 0;
            if (hasRating && !double.TryParse(splitData[5], out rating))
                throw new ArgumentException("The 'Rating' property cannot be parsed to an 'double' type", nameof(data));

            Book book;

            if (hasRating && hasPrice && hasPublisher)
                book = new Book(bookID, title, year, publisherID, price, rating);
            else if (hasPrice && hasPublisher)
                book = new Book(bookID, title, year, publisherID, price);
            else if (hasPublisher)
                book = new Book(bookID, title, year, publisherID);
            else
                book = new Book(bookID, title, year);

            if (restrictionCheck)
                book.Validate();

            return book;

        }

        /// <summary>
        /// Attempts to convert a <see cref="string"/> representation of a <see cref="Book"/> instance to a <see cref="Book"/> object
        /// </summary>
        /// <param name="data">Parsable data</param>
        /// <param name="book"><see cref="Book"/> representation of the <paramref name="data"/> <see cref="string"/></param>
        /// <param name="splitString">Splitting string (default = ";")</param>
        /// <param name="restrictionCheck">Check for attribute restrictions (default = false)</param>
        /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/></returns>
        /// <example><code>
        /// bool Book = Book.TryParse("1;Title;2015", out Book b1);
        /// Book b2 = null; Book.TryParse("2$Title$2015$5", out b2, "$");
        /// if (!Book.TryParse("3;Title;2015;5;15.99", out Book b3, ";", true)) { }
        /// </code></example>
        public static bool TryParse(string data, out Book book, string splitString = ";", bool restrictionCheck = false)
        {
            book = null;

            try { book = Parse(data, splitString, restrictionCheck); return true; }
            catch { return false; }
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            if (PublisherID.HasValue && Price.HasValue && Rating.HasValue)
                return $"[#{BookID}]{Title}({Year}) - [#{PublisherID.Value}]({Rating.Value}) - ${Price.Value}";
            else if (PublisherID.HasValue && Price.HasValue)
                return $"[#{BookID}]{Title}({Year}) - [#{PublisherID.Value}] - ${Price.Value}";
            else if (PublisherID.HasValue)
                return $"[#{BookID}]{Title}({Year}) - [#{PublisherID.Value}]";
            else
                return $"[#{BookID}]{Title}({Year})";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Book) return false;
            else if (Title != (obj as Book).Title) return false;
            else if (Authors != (obj as Book).Authors) return false;
            else if (Year != (obj as Book).Year) return false;
            else if (Publisher != (obj as Book).Publisher) return false;
            else if (Price != (obj as Book).Price) return false;
            else if (Rating != (obj as Book).Rating) return false;
            else return true;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }


        /// <inheritdoc/>
        public int CompareTo(Book other)
        {
            int comparer = Comparer.Default.Compare(Title, other.Title);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Year, other.Year);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Authors, other.Authors);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Publisher, other.Publisher);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Price, other.Price);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Rating, other.Rating);
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
