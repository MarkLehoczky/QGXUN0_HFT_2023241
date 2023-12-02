using NUnit.Framework;
using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QGXUN0_HFT_2023241.Test.ModelsTest
{
    /// <summary>
    /// Test case data values for the <see cref="Author"/> model
    /// </summary>
    public static class AuthorTestData
    {
        /// <summary>
        /// Test case data values for the <see cref="Author.Parse(string, string, bool)"/> and <see cref="Author.TryParse(string, out Author, string, bool)"/> methods which passes
        /// </summary>
        public static IEnumerable<TestCaseData> CorrectParseValues
        {
            get
            {
                yield return new TestCaseData("1;Name", ";", false, new Author(1, "Name"));
                yield return new TestCaseData("1$Name", "$", false, new Author(1, "Name"));
                yield return new TestCaseData("1;Name", ";", true, new Author(1, "Name"));
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Author.Parse(string, string, bool)"/> and <see cref="Author.TryParse(string, out Author, string, bool)"/> methods which fails
        /// </summary>
        public static IEnumerable<TestCaseData> InCorrectParseValues
        {
            get
            {
                yield return new TestCaseData("", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;", ";", true, typeof(ValidationException));
                yield return new TestCaseData(";Name", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1$Name", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Name", "$", true, typeof(ArgumentException));
                yield return new TestCaseData("1;Lorem ipsum dolor sit amet, consectetur adipiscing dui.", ";", true, typeof(ValidationException));
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Author.Equals(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> EqualsValues
        {
            get
            {
                yield return new TestCaseData(new Author(1, "Name"), 0).Returns(false);
                yield return new TestCaseData(new Author(1, "Name"), null).Returns(false);
                yield return new TestCaseData(new Author(1, "Name"), new Author(1, "Name")).Returns(true);
                yield return new TestCaseData(new Author(1, "Name"), new Author(2, "Name")).Returns(true);
                yield return new TestCaseData(new Author(1, "Name"), new Author(1, "Other Name")).Returns(false);
                yield return new TestCaseData(
                    new Author { Books = new List<Book> { new Book(1, "First", 2023) } },
                    new Author()
                    ).Returns(false);
                yield return new TestCaseData(
                    new Author { Books = new List<Book> { new Book(1, "First", 2023) } },
                    new Author { Books = new List<Book> { new Book(1, "First", 2023) } }
                    ).Returns(true);
                yield return new TestCaseData(
                    new Author { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } },
                    new Author { Books = new List<Book> { new Book(1, "First", 2023) } }
                    ).Returns(false);
                yield return new TestCaseData(
                    new Author { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } },
                    new Author { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } }
                    ).Returns(true);
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Author.ToString()"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> ToStringValues
        {
            get
            {
                yield return new TestCaseData(new Author(1, "Name")).Returns("[#1]Name");
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Author.CompareTo(Author)"/>, <see cref="Author.CompareTo(string)"/> and <see cref="Author.CompareTo(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> CompareToValues
        {
            get
            {
                yield return new TestCaseData(new Author(1, "Name"), new Author(1, "Name")).Returns(0);
                yield return new TestCaseData(new Author(1, "A"), new Author(1, "B")).Returns(-1);
                yield return new TestCaseData(new Author(1, "B"), new Author(1, "A")).Returns(1);

                yield return new TestCaseData(new Author(1, "Name"), "1;Name").Returns(0);
                yield return new TestCaseData(new Author(1, "A"), "1;B").Returns(-1);
                yield return new TestCaseData(new Author(1, "B"), "1;A").Returns(1);
            }
        }
    }

    /// <summary>
    /// Test case data values for the <see cref="Book"/> model
    /// </summary>
    public static class BookTestData
    {
        /// <summary>
        /// Test case data values for the <see cref="Book.Parse(string, string, bool)"/> and <see cref="Book.TryParse(string, out Book, string, bool)"/> methods which passes
        /// </summary>
        public static IEnumerable<TestCaseData> CorrectParseValues
        {
            get
            {
                yield return new TestCaseData("1;Title;2023", ";", false, new Book(1, "Title", 2023));
                yield return new TestCaseData("1;Title;2023", ";", true, new Book(1, "Title", 2023));
                yield return new TestCaseData("1$Title$2023$1", "$", false, new Book(1, "Title", 2023, 1));
                yield return new TestCaseData("1$Title$2023$1", "$", true, new Book(1, "Title", 2023, 1));
                yield return new TestCaseData("1;Title;2023;1;15.99", ";", false, new Book(1, "Title", 2023, 1, 15.99));
                yield return new TestCaseData("1;Title;2023;1;15.99;4.7", ";", true, new Book(1, "Title", 2023, 1, 15.99, 4.7));
            }
        }

        /// <summary>
        /// Test case data values for the <see cref="Book.Parse(string, string, bool)"/> and <see cref="Book.TryParse(string, out Book, string, bool)"/> methods which fails
        /// </summary>
        public static IEnumerable<TestCaseData> InCorrectParseValues
        {
            get
            {
                yield return new TestCaseData("", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;;2023", ";", true, typeof(ValidationException));
                yield return new TestCaseData("1;Title", ";", false, typeof(ArgumentException));
                yield return new TestCaseData(";Title;2023", ";", false, typeof(ArgumentException));
                yield return new TestCaseData(";Title;2023", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1$Title$2023$1", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Title;2023", "$", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Title;1900", ";", true, typeof(ValidationException));
                yield return new TestCaseData("1;Title;2100", ";", true, typeof(ValidationException));
                yield return new TestCaseData("1;Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus placerat tortor nunc. Vivamus et cursus turpis, vitae consequat orci. Nam tempor libero sapien, id pulvinar tellus porttitor in. Proin aliquam.;2023", ";", true, typeof(ValidationException));
            }
        }

        /// <summary>
        /// Test case data values for the <see cref="Book.Equals(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> EqualsValues
        {
            get
            {
                yield return new TestCaseData(new Book(1, "Title", 2023), 0).Returns(false);
                yield return new TestCaseData(new Book(1, "Title", 2023), null).Returns(false);
                yield return new TestCaseData(new Book(1, "Title", 2023), new Book(1, "Title", 2023)).Returns(true);
                yield return new TestCaseData(new Book(1, "Title", 2023), new Book(1, "Title", 2022)).Returns(false);
                yield return new TestCaseData(new Book(1, "Title", 2023), new Book(1, "Other title", 2023)).Returns(false);
                yield return new TestCaseData(new Book(1, "Title", 2023, 1), new Book(1, "Title", 2023, 1, 15.99)).Returns(false);
                yield return new TestCaseData(
                    new Book { Title = "1", Authors = new List<Author> { new Author(1, "First") } },
                    new Book { Title = "1" }
                    ).Returns(false);
                yield return new TestCaseData(
                    new Book { Title = "2", Authors = new List<Author> { new Author(1, "First") } },
                    new Book { Title = "2", Authors = new List<Author> { new Author(1, "First") } }
                    ).Returns(true);
                yield return new TestCaseData(
                    new Book { Title = "3", Authors = new List<Author> { new Author(1, "First"), new Author(2, "Second") } },
                    new Book { Title = "3", Authors = new List<Author> { new Author(1, "First") } }
                    ).Returns(false);
                yield return new TestCaseData(
                    new Book { Title = "4", Authors = new List<Author> { new Author(1, "First"), new Author(2, "Second") } },
                    new Book { Title = "4", Authors = new List<Author> { new Author(1, "First"), new Author(2, "Second") } }
                    ).Returns(true);
            }
        }

        /// <summary>
        /// Test case data values for the <see cref="Book.ToString()"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> ToStringValues
        {
            get
            {
                yield return new TestCaseData(new Book(1, "Title", 2023)).Returns("[#1]Title(2023)");
                yield return new TestCaseData(new Book(1, "Title", 2023, 1)).Returns("[#1]Title(2023) - [#1]");
                yield return new TestCaseData(new Book(1, "Title", 2023, 1, 15.99)).Returns("[#1]Title(2023) - [#1] - $15.99");
                yield return new TestCaseData(new Book(1, "Title", 2023, 1, 15.99, 4.7)).Returns("[#1]Title(2023) - [#1](4.7) - $15.99");
            }
        }

        /// <summary>
        /// Test case data values for the <see cref="Book.CompareTo(Book)"/>, <see cref="Book.CompareTo(string)"/> and <see cref="Book.CompareTo(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> CompareToValues
        {
            get
            {
                yield return new TestCaseData(new Book(1, "Title", 2023), new Book(1, "Title", 2023)).Returns(0);
                yield return new TestCaseData(new Book(1, "B", 2023), new Book(1, "A", 2023)).Returns(1);
                yield return new TestCaseData(new Book(1, "Title", 2023), new Book(1, "Title", 2022)).Returns(1);
                yield return new TestCaseData(new Book(1, "Title", 2023, 1), new Book(1, "Title", 2023, 1)).Returns(0);
                yield return new TestCaseData(new Book(1, "Title", 2023, 1, 15.99), new Book(1, "Title", 2023, 1, 15.99)).Returns(0);
                yield return new TestCaseData(new Book(1, "Title", 2023, 1, 17.99), new Book(1, "Title", 2023, 1, 15.99)).Returns(1);

                yield return new TestCaseData(new Book(1, "Title", 2023), "1;Title;2022").Returns(1);
                yield return new TestCaseData(new Book(1, "B", 2023), "1;A;2023").Returns(1);
            }
        }
    }

    /// <summary>
    /// Test case data values for the <see cref="Collection"/> model
    /// </summary>
    public class CollectionTestData
    {
        /// <summary>
        /// Test case data values for the <see cref="Collection.Parse(string, string, bool)"/> and <see cref="Collection.TryParse(string, out Collection, string, bool)"/> methods which passes
        /// </summary>
        public static IEnumerable<TestCaseData> CorrectParseValues
        {
            get
            {
                yield return new TestCaseData("1;Name", ";", false, new Collection(1, "Name"));
                yield return new TestCaseData("1$Name$false", "$", false, new Collection(1, "Name", false));
                yield return new TestCaseData("1;Name;true", ";", true, new Collection(1, "Name", true));
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Collection.Parse(string, string, bool)"/> and <see cref="Collection.TryParse(string, out Collection, string, bool)"/> methods which fails
        /// </summary>
        public static IEnumerable<TestCaseData> InCorrectParseValues
        {
            get
            {
                yield return new TestCaseData("", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;", ";", true, typeof(ValidationException));
                yield return new TestCaseData(";Name", ";", false, typeof(ArgumentException));
                yield return new TestCaseData(";Name;true", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1$Name$false", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Name", "$", true, typeof(ArgumentException));
                yield return new TestCaseData("1;Lorem ipsum dolor sit amet, consectetur adipiscing dui.", ";", true, typeof(ValidationException));
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Collection.Equals(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> EqualsValues
        {
            get
            {
                yield return new TestCaseData(new Collection(1, "Name"), 0).Returns(false);
                yield return new TestCaseData(new Collection(1, "Name"), null).Returns(false);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Name")).Returns(true);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(2, "Name")).Returns(true);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Name", true)).Returns(false);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Other Name")).Returns(false);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Name", false)).Returns(false);
                yield return new TestCaseData(new Collection(1, "Name", true), new Collection(1, "Name", true)).Returns(true);
                yield return new TestCaseData(new Collection(1, "Name", false), new Collection(1, "Name", false)).Returns(true);
                yield return new TestCaseData(new Collection(1, "Name", false), new Collection(1, "Name", true)).Returns(false);
                yield return new TestCaseData(new Collection(1, "Name", true), new Collection(1, "Name", false)).Returns(false);
                yield return new TestCaseData(
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023) } },
                    new Collection()
                    ).Returns(false);
                yield return new TestCaseData(
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023) } },
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023) } }
                    ).Returns(true);
                yield return new TestCaseData(
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } },
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023) } }
                    ).Returns(false);
                yield return new TestCaseData(
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } },
                    new Collection { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } }
                    ).Returns(true);
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Collection.ToString()"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> ToStringValues
        {
            get
            {
                yield return new TestCaseData(new Collection(1, "Name")).Returns("[#1]Name");
                yield return new TestCaseData(new Collection(1, "Name", true)).Returns("[#1]Name (True)");
                yield return new TestCaseData(new Collection(1, "Name", false)).Returns("[#1]Name (False)");
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Collection.CompareTo(Collection)"/>, <see cref="Collection.CompareTo(string)"/> and <see cref="Collection.CompareTo(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> CompareToValues
        {
            get
            {
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Name")).Returns(0);
                yield return new TestCaseData(new Collection(1, "A"), new Collection(1, "B")).Returns(-1);
                yield return new TestCaseData(new Collection(1, "B"), new Collection(1, "A")).Returns(1);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Name", false)).Returns(-1);
                yield return new TestCaseData(new Collection(1, "Name"), new Collection(1, "Name", true)).Returns(-1);
                yield return new TestCaseData(new Collection(1, "Name", true), new Collection(1, "Name", false)).Returns(1);
                yield return new TestCaseData(new Collection(1, "Name", false), new Collection(1, "Name", true)).Returns(-1);

                yield return new TestCaseData(new Collection(1, "Name"), "1;Name").Returns(0);
                yield return new TestCaseData(new Collection(1, "A"), "1;B").Returns(-1);
                yield return new TestCaseData(new Collection(1, "B"), "1;A").Returns(1);
                yield return new TestCaseData(new Collection(1, "Name"), "1;Name;false").Returns(-1);
                yield return new TestCaseData(new Collection(1, "Name"), "1;Name;true").Returns(-1);
                yield return new TestCaseData(new Collection(1, "Name", true), "1;Name;false").Returns(1);
                yield return new TestCaseData(new Collection(1, "Name", false), "1;Name;true").Returns(-1);
                yield return new TestCaseData(new Collection(1, "Name", false), "1;Name;false").Returns(0);
                yield return new TestCaseData(new Collection(1, "Name", true), "1;Name;true").Returns(0);
            }
        }
    }

    /// <summary>
    /// Test case data values for the <see cref="Publisher"/> model
    /// </summary>
    public static class PublisherTestData
    {
        /// <summary>
        /// Test case data values for the <see cref="Publisher.Parse(string, string, bool)"/> and <see cref="Publisher.TryParse(string, out Publisher, string, bool)"/> methods which passes
        /// </summary>
        public static IEnumerable<TestCaseData> CorrectParseValues
        {
            get
            {
                yield return new TestCaseData("1;Name", ";", false, new Publisher(1, "Name"));
                yield return new TestCaseData("1$Name$www.example.com", "$", false, new Publisher(1, "Name", "www.example.com"));
                yield return new TestCaseData("1;Name", ";", true, new Publisher(1, "Name"));
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Publisher.Parse(string, string, bool)"/> and <see cref="Publisher.TryParse(string, out Publisher, string, bool)"/> methods which fails
        /// </summary>
        public static IEnumerable<TestCaseData> InCorrectParseValues
        {
            get
            {
                yield return new TestCaseData("", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;", ";", true, typeof(ValidationException));
                yield return new TestCaseData(";Name", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Name;", ";", true, typeof(ValidationException));
                yield return new TestCaseData(";Name;www.example.com", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Name;", ";", true, typeof(ValidationException));
                yield return new TestCaseData("1$Name$www.example.com", ";", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Name;www.example.com", "$", false, typeof(ArgumentException));
                yield return new TestCaseData("1;Lorem ipsum dolor sit amet, consectetur adipiscing dui.;www.example.com", ";", true, typeof(ValidationException));
                yield return new TestCaseData("1;Name;Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas efficitur vitae est at posuere. Integer luctus, quam et luctus accumsan, leo est consequat massa, et consequat turpis diam eget ligula. Interdum et malesuada fames ac ante ipsum primis in donec.", ";", true, typeof(ValidationException));
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Publisher.Equals(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> EqualsValues
        {
            get
            {
                yield return new TestCaseData(new Publisher(1, "Name"), 0).Returns(false);
                yield return new TestCaseData(new Publisher(1, "Name"), null).Returns(false);
                yield return new TestCaseData(new Publisher(1, "Name"), new Publisher(1, "Name")).Returns(true);
                yield return new TestCaseData(new Publisher(1, "Name"), new Publisher(2, "Name")).Returns(true);
                yield return new TestCaseData(new Publisher(1, "Name"), new Publisher(1, "Other Name")).Returns(false);
                yield return new TestCaseData(new Publisher(1, "Name"), new Publisher(1, "Name", "www.example.com")).Returns(false);
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com"), new Publisher(1, "Name", "www.example.com")).Returns(true);
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com"), new Publisher(1, "Name", "www.other.com")).Returns(false);
                yield return new TestCaseData(
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023) } },
                    new Publisher()
                    ).Returns(false);
                yield return new TestCaseData(
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023) } },
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023) } }
                    ).Returns(true);
                yield return new TestCaseData(
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } },
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023) } }
                    ).Returns(false);
                yield return new TestCaseData(
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } },
                    new Publisher { Books = new List<Book> { new Book(1, "First", 2023), new Book(2, "Second", 2023) } }
                    ).Returns(true);
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Publisher.ToString()"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> ToStringValues
        {
            get
            {
                yield return new TestCaseData(new Publisher(1, "Name")).Returns("[#1]Name");
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com")).Returns("[#1]Name (www.example.com)");
            }
        }
        /// <summary>
        /// Test case data values for the <see cref="Publisher.CompareTo(Publisher)"/>, <see cref="Publisher.CompareTo(string)"/> and <see cref="Publisher.CompareTo(object)"/> methods
        /// </summary>
        public static IEnumerable<TestCaseData> CompareToValues
        {
            get
            {
                yield return new TestCaseData(new Publisher(1, "Name"), new Publisher(1, "Name")).Returns(0);
                yield return new TestCaseData(new Publisher(1, "A"), new Publisher(1, "B")).Returns(-1);
                yield return new TestCaseData(new Publisher(1, "B"), new Publisher(1, "A")).Returns(1);
                yield return new TestCaseData(new Publisher(1, "Name"), new Publisher(1, "Name", "www.example.com")).Returns(-1);
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com"), new Publisher(1, "Name")).Returns(1);
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com"), new Publisher(1, "Name", "www.example.com")).Returns(0);
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com"), new Publisher(1, "Name", "www.other.com")).Returns(-1);

                yield return new TestCaseData(new Publisher(1, "Name"), "1;Name").Returns(0);
                yield return new TestCaseData(new Publisher(1, "A"), "1;B").Returns(-1);
                yield return new TestCaseData(new Publisher(1, "B"), "1;A").Returns(1);
                yield return new TestCaseData(new Publisher(1, "Name"), "1;Namewww.example.com").Returns(-1);
                yield return new TestCaseData(new Publisher(1, "Name", "www.example.com"), "1;Name").Returns(1);
            }
        }
    }
}
