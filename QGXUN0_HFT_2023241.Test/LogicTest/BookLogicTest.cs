using Moq;
using NUnit.Framework;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Test.LogicTest
{
    [TestFixture]
    class BookLogicTest
    {
        private IBookLogic logic;
        private Mock<IRepository<Book>> bookMock;
        private Mock<IRepository<Author>> authorMock;
        private Mock<IRepository<BookAuthorConnector>> connectorMock;

        private Book b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15;
        private Author a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;
        private Collection c1, c2, c3, c4;
        private Publisher p1, p2, p3, p4;
        private BookAuthorConnector ba1, ba2, ba3, ba4, ba5, ba6, ba7, ba8, ba9, ba10, ba11, ba12, ba13, ba14, ba15, ba16, ba17, ba18, ba19;

        private IEnumerable<Book> books;
        private IEnumerable<Author> authors;
        private IEnumerable<BookAuthorConnector> bookauthorconnectors;


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

            p1.Books = new List<Book> { b1, b2, b9 };
            p2.Books = new List<Book> { b3, b4, b6, b13 };
            p3.Books = new List<Book> { b7, b12, b15 };

            ba1 = new BookAuthorConnector(1, 1, 1);
            ba2 = new BookAuthorConnector(2, 2, 1);
            ba3 = new BookAuthorConnector(3, 3, 1);
            ba4 = new BookAuthorConnector(4, 4, 2);
            ba5 = new BookAuthorConnector(5, 5, 2);
            ba6 = new BookAuthorConnector(6, 5, 3);
            ba7 = new BookAuthorConnector(7, 6, 3);
            ba8 = new BookAuthorConnector(8, 7, 4);
            ba9 = new BookAuthorConnector(9, 7, 5);
            ba10 = new BookAuthorConnector(10, 8, 5);
            ba11 = new BookAuthorConnector(11, 9, 6);
            ba12 = new BookAuthorConnector(12, 10, 7);
            ba13 = new BookAuthorConnector(13, 11, 8);
            ba14 = new BookAuthorConnector(14, 12, 7);
            ba15 = new BookAuthorConnector(15, 12, 8);
            ba16 = new BookAuthorConnector(16, 12, 9);
            ba17 = new BookAuthorConnector(17, 13, 9);
            ba18 = new BookAuthorConnector(18, 14, 10);
            ba19 = new BookAuthorConnector(19, 15, 10);


            books = new List<Book>(new[] { b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15 });
            authors = new List<Author>(new[] { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 });
            bookauthorconnectors = new List<BookAuthorConnector>(new[] { ba1, ba2, ba3, ba4, ba5, ba6, ba7, ba8, ba9, ba10, ba11, ba12, ba13, ba14, ba15, ba16, ba17, ba18, ba19 });
        }

        [SetUp]
        public void SetUp()
        {
            authorMock = new Mock<IRepository<Author>>();
            bookMock = new Mock<IRepository<Book>>();
            connectorMock = new Mock<IRepository<BookAuthorConnector>>();


            bookMock.Setup(b => b.ReadAll()).Returns(books.AsQueryable());
            bookMock.Setup(b => b.Delete(It.IsNotIn(books.Select(b => b.BookID)))).Throws(new InvalidOperationException());
            bookMock.Setup(b => b.Read(It.IsNotIn(books.Select(b => b.BookID)))).Throws(new InvalidOperationException());
            bookMock.Setup(b => b.Update(It.IsNotIn(books))).Throws(new InvalidOperationException());
            foreach (var item in books.Select(b => new { id = b.BookID, book = b }))
                bookMock.Setup(b => b.Read(item.id)).Returns(item.book);

            authorMock.Setup(a => a.ReadAll()).Returns(authors.AsQueryable());
            authorMock.Setup(a => a.Delete(It.IsNotIn(authors.Select(a => a.AuthorID)))).Throws(new InvalidOperationException());
            authorMock.Setup(a => a.Read(It.IsNotIn(authors.Select(a => a.AuthorID)))).Throws(new InvalidOperationException());
            authorMock.Setup(a => a.Update(It.IsNotIn(authors))).Throws(new InvalidOperationException());
            foreach (var item in authors.Select(a => new { id = a.AuthorID, author = a }))
                authorMock.Setup(a => a.Read(item.id)).Returns(item.author);

            connectorMock.Setup(t => t.ReadAll()).Returns(bookauthorconnectors.AsQueryable());
            connectorMock.Setup(t => t.Read(It.IsNotIn(bookauthorconnectors.Select(t => t.BookAuthorConnectorID)))).Throws(new InvalidOperationException());
            connectorMock.Setup(t => t.Update(It.IsNotIn(bookauthorconnectors))).Throws(new InvalidOperationException());
            connectorMock.Setup(t => t.Delete(It.IsNotIn(bookauthorconnectors.Select(t => t.BookAuthorConnectorID)))).Throws(new InvalidOperationException());
            foreach (var item in bookauthorconnectors.Select(t => new { id = t.BookAuthorConnectorID, connector = t }))
                connectorMock.Setup(t => t.Read(item.id)).Returns(item.connector);


            logic = new BookLogic(bookMock.Object, authorMock.Object, connectorMock.Object);


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
        }


        [Test]
        public void CreateTest()
        {
            var incorrect = new Book(0, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus placerat tortor nunc. Vivamus et cursus turpis, vitae consequat orci. Nam tempor libero sapien, id pulvinar tellus porttitor in. Proin aliquam.", 2023);
            var book1 = new Book(1, "First", 2023);
            var book2 = new Book(20, "Second", 2023); book2.Authors = new List<Author> { a7 };

            Assert.IsNull(logic.Create(null));
            Assert.IsNull(logic.Create(incorrect));
            Assert.IsNull(logic.Create(book1));
            Assert.That(logic.Create(book2), Is.EqualTo(20));
            bookMock.Setup(b => b.Read(20)).Returns(book2);
            bookMock.Setup(b => b.ReadAll()).Returns(books.Append(book2).AsQueryable());
            Assert.That(logic.Create(book2), Is.EqualTo(20));

            Assert.IsNull(logic.Create(null, a1, a2));
            Assert.IsNull(logic.Create(incorrect, a1, a2));

            bookMock.Verify(b => b.Create(incorrect), Times.Never);
            bookMock.Verify(b => b.Create(book1), Times.Never);
            bookMock.Verify(b => b.Create(book2), Times.Once);
            bookMock.Verify(b => b.Read(1), Times.Never);
            bookMock.Verify(b => b.Read(20), Times.Once);
            bookMock.Verify(b => b.Read(21), Times.Never);
        }

        [Test]
        public void ReadTest()
        {
            var notadded = new Book(20, "First", 2023);

            Assert.IsNull(logic.Read(0));
            Assert.IsNull(logic.Read(notadded.BookID));
            Assert.That(logic.Read(1), Is.EqualTo(b1));
            Assert.AreEqual(books.AsQueryable(), logic.ReadAll());

            bookMock.Verify(b => b.Read(0), Times.Once);
            bookMock.Verify(b => b.Read(1), Times.Once);
            bookMock.Verify(b => b.Read(20), Times.Once);
            bookMock.Verify(b => b.ReadAll(), Times.Once);
        }

        [Test]
        public void UpdateTest()
        {
            var notadded = new Book(20, "First", 2023);
            var book1 = b1; book1.Title = "Updated Second Book";

            Assert.IsFalse(logic.Update(null));
            Assert.IsFalse(logic.Update(notadded));
            Assert.IsTrue(logic.Update(book1));

            bookMock.Verify(b => b.Update(book1), Times.Once);
            bookMock.Verify(b => b.Update(notadded), Times.Never);
        }

        [Test]
        public void DeleteTest()
        {
            bookMock.Setup(b => b.Read(It.IsAny<int>())).Throws(new InvalidOperationException());
            var notadded = new Book(0, "First", 2023);

            Assert.IsFalse(logic.Delete(null));
            Assert.IsFalse(logic.Delete(notadded));
            Assert.IsTrue(logic.Delete(b1));

            bookMock.Verify(b => b.Delete(0), Times.Once);
            bookMock.Verify(b => b.Delete(1), Times.Once);
            bookMock.Verify(b => b.Delete(2), Times.Never);
        }


        [Test]
        public void LogicTest()
        {
            Assert.That(logic.Count, Is.EqualTo(15));
            Assert.IsFalse(logic.IsEmpty);

            Assert.AreEqual(new List<Book> { b6, b7, b8 }, logic.GetBooksInYear(2015));
            Assert.AreEqual(new List<Book> { b4, b5, b6, b7, b8, b9, b10, b11 }, logic.GetBooksBetweenYears(2013, 2017));
            Assert.AreEqual(new List<Book> { b1 }, logic.GetBooksWithTitle("first"));
            Assert.AreEqual(new List<Book> { b1 }, logic.GetBooksWithTitle("First"));
            Assert.AreEqual(new List<Book> { b1 }, logic.GetBooksWithTitle("FIRST"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("Wrong"));
            Assert.AreEqual(new Dictionary<string, IEnumerable<Book>> {
                    { "first", new List<Book> { b1 } },
                    { "second", new List<Book> { b2 } },
                    { "third", new List<Book> { b3 } },
                    { "teen", new List<Book> { b13, b14, b15 } },
                    { "wrong", Enumerable.Empty<Book>() },
                }, logic.GetBooksWithTitles("first", "second", "third", "teen", "wrong"));

            Assert.That(logic.SelectBook(BookFilter.MostExpensive), Is.EqualTo(b6));
            Assert.That(logic.SelectBook(BookFilter.LeastExpensive), Is.EqualTo(b13));
            Assert.That(logic.SelectBook(BookFilter.HighestRated), Is.EqualTo(b6));
            Assert.That(logic.SelectBook(BookFilter.LowestRated), Is.EqualTo(b15));
        }

        [Test]
        public void EmptyDBSetLogicTest()
        {
            bookMock.Setup(b => b.ReadAll()).Returns(Enumerable.Empty<Book>().AsQueryable());


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksInYear(2015));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksBetweenYears(2013, 2017));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("first"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("First"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("FIRST"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("Wrong"));
            Assert.AreEqual(new Dictionary<string, IEnumerable<Book>> {
                    { "first", Enumerable.Empty<Book>() },
                    { "second", Enumerable.Empty<Book>() },
                    { "third", Enumerable.Empty<Book>() },
                    { "teen", Enumerable.Empty<Book>() },
                    { "wrong", Enumerable.Empty<Book>() },
                }, logic.GetBooksWithTitles("first", "second", "third", "teen", "wrong"));

            Assert.That(logic.SelectBook(BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBook(BookFilter.LeastExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBook(BookFilter.HighestRated), Is.EqualTo(null));
            Assert.That(logic.SelectBook(BookFilter.LowestRated), Is.EqualTo(null));
        }

        [Test]
        public void EmptyOtherDBSetLogicTest()
        {
            foreach (var item in books)
            {
                item.Authors = new List<Author>();
                item.Publisher = null;
                item.PublisherID = null;
                item.Collections = new List<Collection>();
            }


            Assert.That(logic.Count, Is.EqualTo(15));
            Assert.IsFalse(logic.IsEmpty);

            Assert.AreEqual(new List<Book> { b6, b7, b8 }, logic.GetBooksInYear(2015));
            Assert.AreEqual(new List<Book> { b4, b5, b6, b7, b8, b9, b10, b11 }, logic.GetBooksBetweenYears(2013, 2017));
            Assert.AreEqual(new List<Book> { b1 }, logic.GetBooksWithTitle("first"));
            Assert.AreEqual(new List<Book> { b1 }, logic.GetBooksWithTitle("First"));
            Assert.AreEqual(new List<Book> { b1 }, logic.GetBooksWithTitle("FIRST"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("Wrong"));
            Assert.AreEqual(new Dictionary<string, IEnumerable<Book>> {
                    { "first", new List<Book> { b1 } },
                    { "second", new List<Book> { b2 } },
                    { "third", new List<Book> { b3 } },
                    { "teen", new List<Book> { b13, b14, b15 } },
                    { "wrong", Enumerable.Empty<Book>() },
                }, logic.GetBooksWithTitles("first", "second", "third", "teen", "wrong"));

            Assert.That(logic.SelectBook(BookFilter.MostExpensive), Is.EqualTo(b6));
            Assert.That(logic.SelectBook(BookFilter.LeastExpensive), Is.EqualTo(b13));
            Assert.That(logic.SelectBook(BookFilter.HighestRated), Is.EqualTo(b6));
            Assert.That(logic.SelectBook(BookFilter.LowestRated), Is.EqualTo(b15));
        }

        [Test]
        public void EmptyAllDBSetLogicTest()
        {
            bookMock.Setup(b => b.ReadAll()).Returns(Enumerable.Empty<Book>().AsQueryable());

            foreach (var item in books)
            {
                item.Authors = new List<Author>();
                item.Publisher = null;
                item.PublisherID = null;
                item.Collections = new List<Collection>();
            }


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksInYear(2015));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksBetweenYears(2013, 2017));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("first"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("First"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("FIRST"));
            Assert.AreEqual(Enumerable.Empty<Book>(), logic.GetBooksWithTitle("Wrong"));
            Assert.AreEqual(new Dictionary<string, IEnumerable<Book>> {
                    { "first", Enumerable.Empty<Book>() },
                    { "second", Enumerable.Empty<Book>() },
                    { "third", Enumerable.Empty<Book>() },
                    { "teen", Enumerable.Empty<Book>() },
                    { "wrong", Enumerable.Empty<Book>() },
                }, logic.GetBooksWithTitles("first", "second", "third", "teen", "wrong"));

            Assert.That(logic.SelectBook(BookFilter.MostExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBook(BookFilter.LeastExpensive), Is.EqualTo(null));
            Assert.That(logic.SelectBook(BookFilter.HighestRated), Is.EqualTo(null));
            Assert.That(logic.SelectBook(BookFilter.LowestRated), Is.EqualTo(null));
        }
    }
}
