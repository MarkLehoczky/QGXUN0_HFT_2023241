using NUnit.Framework;
using QGXUN0_HFT_2023241.Models;
using System;

namespace QGXUN0_HFT_2023241.Test.ModelsTest
{
    [TestFixture]
    class BookTest
    {
        [Test]
        public void ConstructorTest()
        {
            var actual = new Book();

            Assert.That(actual.BookID, Is.EqualTo(0));
            Assert.IsNull(actual.Title);
            Assert.IsNull(actual.Authors);
            Assert.IsNull(actual.AuthorConnector);
            Assert.That(actual.Year, Is.EqualTo(0));
            Assert.IsNull(actual.PublisherID);
            Assert.IsNull(actual.Publisher);
            Assert.IsNull(actual.Collections);
            Assert.IsNull(actual.CollectionConnector);
            Assert.IsNull(actual.Price);
            Assert.IsNull(actual.Rating);


            actual = new Book(1, "Title", 2023);

            Assert.That(actual.BookID, Is.EqualTo(1));
            Assert.That(actual.Title, Is.EqualTo("Title"));
            Assert.IsNull(actual.Authors);
            Assert.IsNull(actual.AuthorConnector);
            Assert.That(actual.Year, Is.EqualTo(2023));
            Assert.IsNull(actual.PublisherID);
            Assert.IsNull(actual.Publisher);
            Assert.IsNull(actual.Collections);
            Assert.IsNull(actual.CollectionConnector);
            Assert.IsNull(actual.Price);
            Assert.IsNull(actual.Rating);


            actual = new Book(1, "Title", 2023, 1);

            Assert.That(actual.BookID, Is.EqualTo(1));
            Assert.That(actual.Title, Is.EqualTo("Title"));
            Assert.IsNull(actual.Authors);
            Assert.IsNull(actual.AuthorConnector);
            Assert.That(actual.Year, Is.EqualTo(2023));
            Assert.That(actual.PublisherID, Is.EqualTo(1));
            Assert.IsNull(actual.Publisher);
            Assert.IsNull(actual.Collections);
            Assert.IsNull(actual.CollectionConnector);
            Assert.IsNull(actual.Price);
            Assert.IsNull(actual.Rating);


            actual = new Book(1, "Title", 2023, 1, 15.99);

            Assert.That(actual.Title, Is.EqualTo("Title"));
            Assert.IsNull(actual.Authors);
            Assert.IsNull(actual.AuthorConnector);
            Assert.That(actual.Year, Is.EqualTo(2023));
            Assert.That(actual.PublisherID, Is.EqualTo(1));
            Assert.IsNull(actual.Publisher);
            Assert.IsNull(actual.Collections);
            Assert.IsNull(actual.CollectionConnector);
            Assert.That(actual.Price, Is.EqualTo(15.99).Within(0.01));
            Assert.IsNull(actual.Rating);


            actual = new Book(1, "Title", 2023, 1, 15.99, 4.7);

            Assert.That(actual.Title, Is.EqualTo("Title"));
            Assert.IsNull(actual.Authors);
            Assert.IsNull(actual.AuthorConnector);
            Assert.That(actual.Year, Is.EqualTo(2023));
            Assert.That(actual.PublisherID, Is.EqualTo(1));
            Assert.IsNull(actual.Publisher);
            Assert.IsNull(actual.Collections);
            Assert.IsNull(actual.CollectionConnector);
            Assert.That(actual.Price, Is.EqualTo(15.99).Within(0.01));
            Assert.That(actual.Rating, Is.EqualTo(4.7).Within(0.01));
        }

        [TestCaseSource(typeof(BookTestData), nameof(BookTestData.CorrectParseValues))]
        public void CorrectParseTest(string data, string splitString, bool validation, Book expected)
        {
            Book parse = Book.Parse(data, splitString, validation);
            bool successful = Book.TryParse(data, out Book tryparse, splitString, validation);

            Assert.That(parse, Is.EqualTo(expected));
            Assert.That(tryparse, Is.EqualTo(expected));
            Assert.IsTrue(successful);
        }

        [TestCaseSource(typeof(BookTestData), nameof(BookTestData.InCorrectParseValues))]
        public void InCorrectParseTest(string data, string splitString, bool validation, Type exception)
        {
            bool successful = Book.TryParse(data, out Book tryparse, splitString, validation);

            Assert.Throws(exception, () => { Book.Parse(data, splitString, validation); });
            Assert.IsNull(tryparse);
            Assert.IsFalse(successful);
        }

        [TestCaseSource(typeof(BookTestData), nameof(BookTestData.EqualsValues))]
        public bool EqualsTest(Book left, object right)
        {
            return left.Equals(right);
        }

        [TestCaseSource(typeof(BookTestData), nameof(BookTestData.ToStringValues))]
        public string ToStringTest(Book value)
        {
            return value.ToString();
        }

        [TestCaseSource(typeof(BookTestData), nameof(BookTestData.CompareToValues))]
        public int CompareToMethodTest(Book left, object right)
        {
            return left.CompareTo(right);
        }
    }
}
