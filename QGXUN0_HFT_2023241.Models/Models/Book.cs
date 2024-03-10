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
    /// Specifies the unique ID, title, authors, release year, publisher, collections, price and rating of a <see cref="Book"/>.
    /// </summary>
    public class Book : IComparable<Book>, IComparable<string>, IComparable, IEquatable<Book>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="Book"/>.
        /// </summary>
        /// <remarks>The value of this property is used as the <see langword="unique key"/> in the database</remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("BookID")]
        [JsonProperty("BookID")]
        [Required]
        [Key]
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the title of the <see cref="Book"/>.
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        [JsonPropertyName("Title")]
        [JsonProperty("Title")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the authors of the <see cref="Book"/>.
        /// </summary>
        [JsonPropertyName("Authors")]
        [JsonProperty("Authors")]
        [Required]
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
        /// <summary>
        /// Gets or sets the connector for the authors of the <see cref="Book"/>.
        /// </summary>
        [JsonPropertyName("AuthorConnector")]
        [JsonProperty("AuthorConnector")]
        public virtual ICollection<BookAuthorConnector> AuthorConnector { get; set; }

        /// <summary>
        /// Gets or sets the release year of the <see cref="Book"/>.
        /// </summary>
        /// <value>Between 1950 and 2050</value>
        [JsonPropertyName("Year")]
        [JsonProperty("Year")]
        [Range(1950, 2050)]
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the ID of the <see cref="Publisher"/> of the <see cref="Book"/>.
        /// </summary>
        [JsonPropertyName("PublisherID")]
        [JsonProperty("PublisherID")]
        public int? PublisherID { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the <see cref="Book"/>.
        /// </summary>
        [JsonPropertyName("Publisher")]
        [JsonProperty("Publisher")]
        public virtual Publisher Publisher { get; set; }

        /// <summary>
        /// Gets or sets the collections of the <see cref="Collection"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
        /// <summary>
        /// Gets or sets the connector for the collections of the <see cref="Book"/>.
        /// </summary>
        [JsonPropertyName("CollectionConnector")]
        [JsonProperty("CollectionConnector")]
        public virtual ICollection<BookCollectionConnector> CollectionConnector { get; set; }

        /// <summary>
        /// Gets or sets the price of the <see cref="Book"/>.
        /// </summary>
        [JsonPropertyName("Price")]
        [JsonProperty("Price")]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the rating of the <see cref="Book"/>.
        /// </summary>
        /// <value>Between 0.0 and 5.0</value>
        [JsonPropertyName("Rating")]
        [JsonProperty("Rating")]
        [Range(1.0, 5.0)]
        public double? Rating { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> <see langword="class"/>.
        /// </summary>
        public Book() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> <see langword="class"/> by using the required properties.
        /// </summary>
        /// <param name="bookID">Unique ID of the <see cref="Book"/></param>
        /// <param name="title">Title of the <see cref="Book"/></param>
        /// <param name="year">Release year of the <see cref="Book"/></param>
        public Book(int bookID, string title, int year)
        {
            BookID = bookID;
            Title = title;
            Year = year;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> <see langword="class"/> by using the required and optional properties.
        /// </summary>
        /// <param name="bookID">Unique ID of the <see cref="Book"/></param>
        /// <param name="title">Title of the <see cref="Book"/></param>
        /// <param name="year">Release year of the <see cref="Book"/></param>
        /// <param name="publisherID">ID of the <see cref="Models.Publisher"> of the <see cref="Book"/></see></param>
        public Book(int bookID, string title, int year, int? publisherID)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> <see langword="class"/> by using the required and optional properties.
        /// </summary>
        /// <param name="bookID">Unique ID of the <see cref="Book"/></param>
        /// <param name="title">Title of the <see cref="Book"/></param>
        /// <param name="year">Release year of the <see cref="Book"/></param>
        /// <param name="publisherID">ID of the <see cref="Models.Publisher"> of the <see cref="Book"/></see></param>
        /// <param name="price">Price of the <see cref="Book"/></param>
        public Book(int bookID, string title, int year, int? publisherID, double? price)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
            Price = price;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> <see langword="class"/> by using the required and optional properties.
        /// </summary>
        /// <param name="bookID">Unique ID of the <see cref="Book"/></param>
        /// <param name="title">Title of the <see cref="Book"/></param>
        /// <param name="year">Release year of the <see cref="Book"/></param>
        /// <param name="publisherID">ID of the <see cref="Models.Publisher"> of the <see cref="Book"/></see></param>
        /// <param name="price">Price of the <see cref="Book"/></param>
        /// <param name="rating">Rating of the <see cref="Book"/></param>
        public Book(int bookID, string title, int year, int? publisherID, double? price, double? rating)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
            Price = price;
            Rating = rating;
        }


        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Book"/>.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Book"/> to convert</param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see cref="Book"/> representation of the <paramref name="data"/> <see cref="string"/></returns>
        /// <exception cref="ArgumentException">An error occurred during parsing</exception>
        /// <exception cref="ValidationException">The specified <paramref name="data"/> is not valid</exception>
        /// <example><code>
        /// Book b1 = Book.Parse("1;Title;2015");
        /// Book b2 = Book.Parse("2$Title$2015$5", "$");
        /// Book b3 = Book.Parse("3;Title;2015;5;15.99", ";", false);
        /// </code></example>
        public static Book Parse(string data, string splitString = ";", bool validate = true)
        {
            string[] splitData = data.Split(splitString);

            if (splitData.Length < 3)
                throw new ArgumentException("Not enough value after splitting the string, or the splitting was unsuccessful", nameof(data));

            if (!int.TryParse(splitData[0], out var bookID))
                throw new ArgumentException("The 'BookID' property cannot be parsed to an 'int' type", nameof(data));

            string title = splitData[1];

            if (!int.TryParse(splitData[2], out var year))
                throw new ArgumentException("The 'Year' property cannot be parsed to an 'int' type", nameof(data));

            int? publisherID = null;
            if (splitData.Length > 3 && int.TryParse(splitData[3], out int temp1)) publisherID = temp1;
            else if (splitData.Length > 3 && splitData[3] != "")
                throw new ArgumentException("The 'PublisherID' property cannot be parsed to an 'int' type", nameof(data));

            double? price = null;
            if (splitData.Length > 4 && double.TryParse(splitData[4], out double temp2)) price = temp2;
            else if (splitData.Length > 4 && splitData[4] != "")
                throw new ArgumentException("The 'Price' property cannot be parsed to an 'double' type", nameof(data));

            double? rating = null;
            if (splitData.Length > 5 && double.TryParse(splitData[5], out double temp3)) rating = temp3;
            else if (splitData.Length > 5 && splitData[5] != "publisherID")
                throw new ArgumentException("The 'Rating' property cannot be parsed to an 'double' type", nameof(data));

            Book book = new Book(bookID, title, year, publisherID, price, rating);
            if (validate) book.Validate();

            return book;
        }

        /// <summary>
        /// Converts the <see cref="string"/> representation of a <see cref="Book"/>. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="data">A <see cref="string"/> containing a <see cref="Book"/> to convert</param>
        /// <param name="book"><see cref="Book"/> representation of the <paramref name="data"/> if the parsing was successful; otherwise, <see langword="null"/></param>
        /// <param name="splitString">Specifies a <see cref="string"/> instance which determines where to split the specified <paramref name="data"/> (default = ";")</param>
        /// <param name="validate">Determines whether to validate the attributes (default = true)</param>
        /// <returns><see langword="true"/> if the parsing was successful; otherwise, <see langword="false"/></returns>
        /// <example><code>
        /// bool Book = Book.TryParse("1;Title;2015", out Book b1);
        /// Book b2 = null; Book.TryParse("2$Title$2015$5", out b2, "$");
        /// if (!Book.TryParse("3;Title;2015;5;15.99", out Book b3, ";", false)) { }
        /// </code></example>
        public static bool TryParse(string data, out Book book, string splitString = ";", bool validate = true)
        {
            book = null;

            try { book = Parse(data, splitString, validate); return true; }
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
            if (obj is not Book book) return false;
            else return Equals(book);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Authors, Year, Publisher, Price, Rating);
        }


        /// <inheritdoc/>
        public int CompareTo(Book other)
        {
            int comparer = Comparer.Default.Compare(Title, other.Title);
            if (comparer != 0) return comparer;

            comparer = Comparer.Default.Compare(Year, other.Year);
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
                return CompareTo(obj.ToString());
            else
                return CompareTo(obj as Book);
        }

        /// <inheritdoc/>
        public bool Equals(Book other)
        {
            if (other == null) return false;
            else if (ReferenceEquals(this, other)) return true;
            else if (Title != other.Title) return false;
            else if (!Authors.SequenceEqual(other.Authors)) return false;
            else if (Year != other.Year) return false;
            else if ((Publisher == null && other.Publisher != null) || (Publisher != null && other.Publisher == null)) return false;
            else if (Publisher != null && other.Publisher != null && !Publisher.Equals(other.Publisher)) return false;
            else if (Price != other.Price) return false;
            else if (Rating != other.Rating) return false;
            else return true;
        }
    }
}
