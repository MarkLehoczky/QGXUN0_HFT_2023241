using Moq;
using NUnit.Framework;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Test.LogicTest
{
    [TestFixture]
    class AuthorLogicTest
    {
        private IAuthorLogic logic;
        private Mock<IRepository<Author>> authorMock;

        private Book b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15;
        private Author a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;
        private Collection c1, c2, c3, c4;
        private Publisher p1, p2, p3, p4;

        private IEnumerable<Author> authors;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            b1 = new Book(1, "First Book", 2010, 1);
            b2 = new Book(2, "Second Book", 2011, 1);
            b3 = new Book(3, "Third Book", 2012, 2);
            b4 = new Book(4, "Fourth Book", 2013, 2, 10, 4.0);
            b5 = new Book(5, "Fifth Book", 2014);
            b6 = new Book(6, "Sixth Book", 2015, 2, 15, 5.0);
            b7 = new Book(7, "Seventh Book", 2015, 3);
            b8 = new Book(8, "Eight Book", 2015);
            b9 = new Book(9, "Ninth Book", 2016, 1);
            b10 = new Book(10, "Tenth Book", 2016);
            b11 = new Book(11, "Eleventh Book", 2017);
            b12 = new Book(12, "Twelfth Book", 2018, 3, 12, 3.5);
            b13 = new Book(13, "Thirteenth Book", 2018, 2, 7, 4.2);
            b14 = new Book(14, "Fourteenth Book", 2019);
            b15 = new Book(15, "Fifteenth Book", 2019, 3, 8, 3.0);

            a1 = new Author(1, "First Author");
            a2 = new Author(2, "Second Author");
            a3 = new Author(3, "Third Author");
            a4 = new Author(4, "Fourth Author");
            a5 = new Author(5, "Fifth Author");
            a6 = new Author(6, "Sixth Author");
            a7 = new Author(7, "Seventh Author");
            a8 = new Author(8, "Eight Author");
            a9 = new Author(9, "Ninth Author");
            a10 = new Author(10, "Tenth Author");

            c1 = new Collection(1, "Unknown Collection");
            c2 = new Collection(2, "Normal Collection", false);
            c3 = new Collection(3, "Series Collection", true);
            c4 = new Collection(4, "Empty Collection");

            p1 = new Publisher(1, "Normal Publisher");
            p2 = new Publisher(2, "Series Publisher");
            p3 = new Publisher(3, "Only Series Publisher", "www.website.com");
            p4 = new Publisher(4, "Empty Publisher");


            b1.Authors = new List<Author> { a1 };
            b2.Authors = new List<Author> { a1 };
            b3.Authors = new List<Author> { a1 };
            b4.Authors = new List<Author> { a2 };
            b5.Authors = new List<Author> { a2, a3 };
            b6.Authors = new List<Author> { a3 };
            b7.Authors = new List<Author> { a4, a5 };
            b8.Authors = new List<Author> { a5 };
            b9.Authors = new List<Author> { a6 };
            b10.Authors = new List<Author> { a7 };
            b11.Authors = new List<Author> { a8 };
            b12.Authors = new List<Author> { a7, a8, a9 };
            b13.Authors = new List<Author> { a9 };
            b14.Authors = new List<Author> { a10 };
            b15.Authors = new List<Author> { a10 };

            b3.Collections = new List<Collection> { c3 };
            b4.Collections = new List<Collection> { c2, c3 };
            b5.Collections = new List<Collection> { c1 };
            b6.Collections = new List<Collection> { c2 };
            b7.Collections = new List<Collection> { c2, c3 };
            b8.Collections = new List<Collection> { c1 };
            b11.Collections = new List<Collection> { c1 };
            b12.Collections = new List<Collection> { c3 };
            b15.Collections = new List<Collection> { c1, c3 };

            b1.Publisher = p1;
            b2.Publisher = p1;
            b3.Publisher = p2;
            b4.Publisher = p2;
            b6.Publisher = p2;
            b7.Publisher = p3;
            b9.Publisher = p1;
            b12.Publisher = p3;
            b13.Publisher = p2;
            b15.Publisher = p3;

            a1.Books = new List<Book> { b1, b2, b3 };
            a2.Books = new List<Book> { b4, b5 };
            a3.Books = new List<Book> { b5, b6 };
            a4.Books = new List<Book> { b7 };
            a5.Books = new List<Book> { b7, b8 };
            a6.Books = new List<Book> { b9 };
            a7.Books = new List<Book> { b10, b12 };
            a8.Books = new List<Book> { b11, b12 };
            a9.Books = new List<Book> { b12, b13 };
            a10.Books = new List<Book> { b14, b15 };

            c1.Books = new List<Book> { b5, b8, b11, b15 };
            c2.Books = new List<Book> { b4, b6, b7 };
            c3.Books = new List<Book> { b3, b4, b7, b12, b15 };

            p1.Books = new List<Book> { b1, b2, b9 };
            p2.Books = new List<Book> { b3, b4, b6, b13 };
            p3.Books = new List<Book> { b7, b12, b15 };

            authors = new List<Author>(new[] { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 });
        }

        [SetUp]
        public void SetUp()
        {
            authorMock = new Mock<IRepository<Author>>();

            authorMock.Setup(p => p.ReadAll()).Returns(authors.AsQueryable());

            authorMock.Setup(p => p.Read(It.IsNotIn(authors.Select(p => p.AuthorID)))).Throws(new InvalidOperationException());

            foreach (var item in authors.Select(p => new { id = p.AuthorID, publisher = p }))
                authorMock.Setup(p => p.Read(item.id)).Returns(item.publisher);

            authorMock.Setup(p => p.Update(It.IsNotIn(authors))).Throws(new InvalidOperationException());

            authorMock.Setup(p => p.Delete(It.IsNotIn(authors.Select(p => p.AuthorID)))).Throws(new InvalidOperationException());

            logic = new AuthorLogic(authorMock.Object);

            a1.Books = new List<Book> { b1, b2, b3 };
            a2.Books = new List<Book> { b4, b5 };
            a3.Books = new List<Book> { b5, b6 };
            a4.Books = new List<Book> { b7 };
            a5.Books = new List<Book> { b7, b8 };
            a6.Books = new List<Book> { b9 };
            a7.Books = new List<Book> { b10, b12 };
            a8.Books = new List<Book> { b11, b12 };
            a9.Books = new List<Book> { b12, b13 };
            a10.Books = new List<Book> { b14, b15 };
        }


        [Test]
        public void CreateTest()
        {
            var incorrect = new Author(1, "Lorem ipsum dolor sit amet, consectetur adipiscing dui.");
            var author1 = new Author(1, "First");
            var author2 = new Author(20, "Second");

            Assert.IsNull(logic.Create(null));
            Assert.IsNull(logic.Create(incorrect));
            Assert.That(logic.Create(author1), Is.EqualTo(11));
            Assert.That(logic.Create(author2), Is.EqualTo(20));
            authorMock.Setup(a => a.Read(11)).Returns(author1);
            authorMock.Setup(a => a.ReadAll()).Returns(authors.Append(author1).AsQueryable());
            Assert.That(logic.Create(author1), Is.EqualTo(11));

            authorMock.Verify(a => a.Create(incorrect), Times.Never);
            authorMock.Verify(a => a.Create(author1), Times.Once);
            authorMock.Verify(a => a.Create(author2), Times.Once);
            authorMock.Verify(a => a.Read(1), Times.Once);
            authorMock.Verify(a => a.Read(11), Times.Never);
            authorMock.Verify(a => a.Read(20), Times.Once);
        }

        [Test]
        public void ReadTest()
        {
            var notadded = new Author(20, "First");

            Assert.IsNull(logic.Read(0));
            Assert.IsNull(logic.Read(notadded.AuthorID));
            Assert.That(logic.Read(1), Is.EqualTo(a1));
            Assert.AreEqual(authors.AsQueryable(), logic.ReadAll());

            authorMock.Verify(a => a.Read(0), Times.Once);
            authorMock.Verify(a => a.Read(1), Times.Once);
            authorMock.Verify(a => a.Read(20), Times.Once);
            authorMock.Verify(a => a.ReadAll(), Times.Once);
        }

        [Test]
        public void UpdateTest()
        {
            var notadded = new Author(20, "First");
            var author1 = a1; author1.AuthorName = "Updated Normal Author";

            Assert.IsFalse(logic.Update(null));
            Assert.IsFalse(logic.Update(notadded));
            Assert.IsTrue(logic.Update(author1));

            authorMock.Verify(a => a.Update(author1), Times.Once);
            authorMock.Verify(a => a.Update(notadded), Times.Never);
        }

        [Test]
        public void DeleteTest()
        {
            authorMock.Setup(a => a.Read(It.IsAny<int>())).Throws(new InvalidOperationException());
            var notadded = new Author(0, "First");

            Assert.IsFalse(logic.Delete(null));
            Assert.IsFalse(logic.Delete(notadded));
            Assert.IsTrue(logic.Delete(a1));

            authorMock.Verify(a => a.Delete(0), Times.Once);
            authorMock.Verify(a => a.Delete(1), Times.Once);
            authorMock.Verify(a => a.Delete(2), Times.Never);
        }


        [Test]
        public void LogicTest()
        {
            Assert.That(logic.Count, Is.EqualTo(10));
            Assert.IsFalse(logic.IsEmpty);

            Assert.That(logic.GetHighestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(5.0, a3)));
            Assert.That(logic.GetLowestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(3.0, a10)));
            Assert.That(logic.SelectBookFromAuthor(null, BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.MostExpensive), Is.EqualTo(b12));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LeastExpensive), Is.EqualTo(b13));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.HighestRated), Is.EqualTo(b13));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LowestRated), Is.EqualTo(b12));

            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(null));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(a3));
            Assert.AreEqual(new List<Collection> { c3 }, logic.GetSeriesOfAuthor(a1));
        }

        [Test]
        public void EmptyDBSetLogicTest()
        {
            authorMock.Setup(a => a.ReadAll()).Returns(Enumerable.Empty<Author>().AsQueryable());


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.That(logic.GetHighestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(0, null)));
            Assert.That(logic.GetLowestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(0, null)));
            Assert.That(logic.SelectBookFromAuthor(null, BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.MostExpensive), Is.EqualTo(b12));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LeastExpensive), Is.EqualTo(b13));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.HighestRated), Is.EqualTo(b13));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LowestRated), Is.EqualTo(b12));

            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(null));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(a3));
            Assert.AreEqual(new List<Collection> { c3 }, logic.GetSeriesOfAuthor(a1));
        }

        [Test]
        public void EmptyOtherDBSetLogicTest()
        {
            foreach (var item in authors)
            {
                item.Books = null;
            }


            Assert.That(logic.Count, Is.EqualTo(10));
            Assert.IsFalse(logic.IsEmpty);

            Assert.That(logic.GetHighestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(0, null)));
            Assert.That(logic.GetLowestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(0, null)));
            Assert.That(logic.SelectBookFromAuthor(null, BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LeastExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.HighestRated), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LowestRated), Is.EqualTo(null));

            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(null));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(a3));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(a1));
        }

        [Test]
        public void EmptyAllDBSetLogicTest()
        {
            authorMock.Setup(a => a.ReadAll()).Returns(Enumerable.Empty<Author>().AsQueryable());

            foreach (var item in authors)
            {
                item.Books = null;
            }


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.That(logic.GetHighestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(0, null)));
            Assert.That(logic.GetLowestRatedAuthor(), Is.EqualTo(new KeyValuePair<double, Author>(0, null)));
            Assert.That(logic.SelectBookFromAuthor(null, BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LeastExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.HighestRated), Is.EqualTo(null));
            Assert.That(logic.SelectBookFromAuthor(a9, BookFilter.LowestRated), Is.EqualTo(null));

            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(null));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(a3));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetSeriesOfAuthor(a1));
        }
    }
}
