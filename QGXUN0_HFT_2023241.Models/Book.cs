﻿using System;
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
    public class Book
    {
        /// <summary>
        /// Unique key value
        /// </summary>
        /// <remarks>Database Key</remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int BookID { get; set; }

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
        [Required] public virtual ICollection<BookAuthorConnector> AuthorConnector { get; set; }

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
        /// ISBN number
        /// </summary>
        [Range(typeof(ulong), "9780000000000", "9799999999999")] public ulong? ISBN { get; set; }


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
        /// <param name="isbn">ISBN number</param>
        public Book(int bookID, string title, int year, int publisherID, ulong isbn)
        {
            BookID = bookID;
            Title = title;
            Year = year;
            PublisherID = publisherID;
            ISBN = isbn;
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
        /// Collection b3 = Collection.Parse("3;Title;2015;5;9785705329110", ";", true);
        /// </code></example>
        public static Book Parse(string data, string splitString, bool restrictionCheck)
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

            bool hasISBN = splitData.Length > 4;
            ulong isbn = 0;
            if (hasISBN && !ulong.TryParse(splitData[4], out isbn))
                throw new ArgumentException("The 'ISBN' property cannot be parsed to an 'ulong' type", nameof(data));

            if (restrictionCheck)
            {
                StringLengthAttribute titleAttribute = typeof(Book).GetProperty("Title").GetCustomAttributes<StringLengthAttribute>(false).FirstOrDefault();
                if (title.Length == 0)
                    throw new ArgumentException("The 'Title' property is empty", nameof(data));
                else if (title.Length > titleAttribute.MaximumLength)
                    throw new ArgumentException("The 'Title' property's length is longer than the maximum length attribute", nameof(data));

                RangeAttribute yearAttribute = typeof(Book).GetProperty("Year").GetCustomAttributes<RangeAttribute>(false).FirstOrDefault();
                if (year > DateTime.Today.Year)
                    throw new ArgumentException("The 'Year' property's value must be less or equal to the current year", nameof(data));
                else if (year < (int)yearAttribute.Minimum || year > (int)yearAttribute.Maximum)
                    throw new ArgumentException("The 'Year' property's value is out of the range attribute", nameof(data));

                RangeAttribute isbnAttribute = typeof(Book).GetProperty("ISBN").GetCustomAttributes<RangeAttribute>(false).FirstOrDefault();
                if (hasISBN && (isbn < ulong.Parse((string)isbnAttribute.Minimum) || isbn > ulong.Parse((string)isbnAttribute.Maximum)))
                    throw new ArgumentException("The 'ISBN' property's value is out of the range attribute", nameof(data));
            }

            if (hasISBN && hasPublisher)
                return new Book(bookID, title, year, publisherID, isbn);
            else if (hasPublisher)
                return new Book(bookID, title, year, publisherID);
            else
                return new Book(bookID, title, year);

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
        /// if (!Book.TryParse("3;Title;2015;5;9785705329110", out Book b3, ";", true)) { }
        /// </code></example>
        public static bool TryParse(string data, out Book book, string splitString, bool restrictionCheck)
        {
            book = null;

            try { book = Parse(data, splitString, restrictionCheck); return true; }
            catch { return false; }
        }


        ///<inheritdoc/>
        public override string ToString()
        {
            if (PublisherID != null && ISBN != null)
                return $"[#{BookID}]{Title}({ISBN}) - [#{PublisherID}]";
            else if (PublisherID != null)
                return $"[#{BookID}]{Title} - [#{PublisherID}]";
            else
                return $"[#{BookID}]{Title}";
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Book) return false;
            return Title == (obj as Book).Title;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}