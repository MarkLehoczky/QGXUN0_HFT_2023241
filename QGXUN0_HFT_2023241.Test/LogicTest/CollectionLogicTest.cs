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
    class CollectionLogicTest
    {
        private ICollectionLogic logic;
        private Mock<IRepository<Collection>> collectionMock;
        private Mock<IRepository<Book>> bookMock;
        private Mock<IRepository<BookCollectionConnector>> connectorMock;

        private Book b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15;
        private Author a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;
        private Collection c1, c2, c3, c4;
        private Publisher p1, p2, p3, p4;
        private BookCollectionConnector bc1, bc2, bc3, bc4, bc5, bc6, bc7, bc8, bc9, bc10, bc11, bc12;

        private IEnumerable<Book> books;
        private IEnumerable<Collection> collections;
        private IEnumerable<BookCollectionConnector> bookcollectionconnectors;


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

            bc1 = new BookCollectionConnector(1, 3, 3);
            bc2 = new BookCollectionConnector(2, 4, 2);
            bc3 = new BookCollectionConnector(3, 4, 3);
            bc4 = new BookCollectionConnector(4, 5, 1);
            bc5 = new BookCollectionConnector(5, 6, 2);
            bc6 = new BookCollectionConnector(6, 7, 2);
            bc7 = new BookCollectionConnector(7, 7, 3);
            bc8 = new BookCollectionConnector(8, 8, 1);
            bc9 = new BookCollectionConnector(9, 11, 1);
            bc10 = new BookCollectionConnector(10, 12, 3);
            bc11 = new BookCollectionConnector(11, 15, 1);
            bc12 = new BookCollectionConnector(12, 15, 3);

            books = new List<Book>(new[] { b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15 });
            collections = new List<Collection>(new[] { c1, c2, c3, c4 });
            bookcollectionconnectors = new List<BookCollectionConnector>(new[] { bc1, bc2, bc3, bc4, bc5, bc6, bc7, bc8, bc9, bc10, bc11, bc12 });
        }

        [SetUp]
        public void SetUp()
        {
            collectionMock = new Mock<IRepository<Collection>>();
            bookMock = new Mock<IRepository<Book>>();
            connectorMock = new Mock<IRepository<BookCollectionConnector>>();


            collectionMock.Setup(c => c.Read(It.IsNotIn(collections.Select(c => c.CollectionID)))).Throws(new InvalidOperationException());
            foreach (var item in collections.Select(c => new { id = c.CollectionID, collection = c }))
                collectionMock.Setup(c => c.Read(item.id)).Returns(item.collection);
            collectionMock.Setup(c => c.ReadAll()).Returns(collections.AsQueryable());
            collectionMock.Setup(c => c.Update(It.IsNotIn(collections))).Throws(new InvalidOperationException());
            collectionMock.Setup(c => c.Delete(It.IsNotIn(collections.Select(c => c.CollectionID)))).Throws(new InvalidOperationException());

            bookMock.Setup(b => b.Read(It.IsNotIn(books.Select(b => b.BookID)))).Throws(new InvalidOperationException());
            foreach (var item in books.Select(b => new { id = b.BookID, book = b }))
                bookMock.Setup(b => b.Read(item.id)).Returns(item.book);
            bookMock.Setup(b => b.ReadAll()).Returns(books.AsQueryable());
            bookMock.Setup(b => b.Update(It.IsNotIn(books))).Throws(new InvalidOperationException());
            bookMock.Setup(b => b.Delete(It.IsNotIn(books.Select(b => b.BookID)))).Throws(new InvalidOperationException());

            connectorMock.Setup(t => t.Read(It.IsNotIn(bookcollectionconnectors.Select(t => t.BookCollectionConnectorID)))).Throws(new InvalidOperationException());
            foreach (var item in bookcollectionconnectors.Select(t => new { id = t.BookCollectionConnectorID, connector = t }))
                connectorMock.Setup(t => t.Read(item.id)).Returns(item.connector);
            connectorMock.Setup(t => t.ReadAll()).Returns(bookcollectionconnectors.AsQueryable());
            connectorMock.Setup(t => t.Update(It.IsNotIn(bookcollectionconnectors))).Throws(new InvalidOperationException());
            connectorMock.Setup(t => t.Delete(It.IsNotIn(bookcollectionconnectors.Select(t => t.BookCollectionConnectorID)))).Throws(new InvalidOperationException());
            connectorMock.Setup(t => t.SaveChanges());


            logic = new CollectionLogic(collectionMock.Object, bookMock.Object, connectorMock.Object);

            c1.Books = new List<Book> { b5, b8, b11, b15 };
            c2.Books = new List<Book> { b4, b6, b7 };
            c3.Books = new List<Book> { b3, b4, b7, b12, b15 };
            c4.Books = new List<Book>();
        }


        [Test]
        public void CreateTest()
        {
            var incorrect = new Collection(0, "Lorem ipsum dolor sit amet, consectetur adipiscing dui.");
            var collection1 = new Collection(1, "First");
            var collection2 = new Collection(10, "Second");

            Assert.IsNull(logic.Create(null));
            Assert.IsNull(logic.Create(incorrect));
            Assert.That(logic.Create(collection1), Is.EqualTo(5));
            collectionMock.Setup(c => c.Read(5)).Returns(collection1);
            collectionMock.Setup(c => c.ReadAll()).Returns(collections.Append(collection1).AsQueryable());
            Assert.That(logic.Create(collection1), Is.EqualTo(5));

            Assert.IsNull(logic.Create(null, b1, b2));
            Assert.IsNull(logic.Create(incorrect, b1, b2));
            Assert.That(logic.Create(collection2, b1, b2), Is.EqualTo(10));

            collectionMock.Verify(c => c.Create(incorrect), Times.Never);
            collectionMock.Verify(c => c.Create(collection1), Times.Once);
            collectionMock.Verify(c => c.Create(collection2), Times.Once);
            collectionMock.Verify(c => c.Read(1), Times.Once);
            collectionMock.Verify(c => c.Read(5), Times.Never);
            collectionMock.Verify(c => c.Read(10), Times.Exactly(2));
        }


        [Test]
        public void ReadTest()
        {
            var notadded = new Collection(10, "First");

            Assert.IsNull(logic.Read(0));
            Assert.IsNull(logic.Read(notadded.CollectionID));
            Assert.That(logic.Read(1), Is.EqualTo(c1));
            Assert.AreEqual(collections.AsQueryable(), logic.ReadAll());

            collectionMock.Verify(c => c.Read(0), Times.Once);
            collectionMock.Verify(c => c.Read(1), Times.Once);
            collectionMock.Verify(c => c.Read(10), Times.Once);
            collectionMock.Verify(c => c.ReadAll(), Times.Once);
        }

        [Test]
        public void UpdateTest()
        {
            var notadded = new Collection(10, "First");
            var collection1 = c1; collection1.CollectionName = "Updated Unknown Collection";

            Assert.IsFalse(logic.Update(null));
            Assert.IsFalse(logic.Update(notadded));
            Assert.IsTrue(logic.Update(collection1));

            collectionMock.Verify(c => c.Update(collection1), Times.Once);
            collectionMock.Verify(c => c.Update(notadded), Times.Never);
        }

        [Test]
        public void DeleteTest()
        {
            collectionMock.Setup(c => c.Read(It.IsAny<int>())).Throws(new InvalidOperationException());
            var notadded = new Collection(0, "First");

            Assert.IsFalse(logic.Delete(null));
            Assert.IsFalse(logic.Delete(notadded));
            Assert.IsTrue(logic.Delete(c1));

            collectionMock.Verify(c => c.Delete(0), Times.Once);
            collectionMock.Verify(c => c.Delete(1), Times.Once);
            collectionMock.Verify(c => c.Delete(2), Times.Never);
        }


        [Test]
        public void LogicTest()
        {
            Assert.That(logic.Count, Is.EqualTo(4));
            Assert.IsFalse(logic.IsEmpty);

            Assert.AreEqual(new List<Collection> { c1, c2, c4 }, logic.GetAllNonSeries());
            Assert.AreEqual(new List<Collection> { c3 }, logic.GetAllSeries());
            Assert.AreEqual(new List<Collection> { c2, c3 }, logic.GetCollectionsBetweenYears(2010, 2013));
            Assert.AreEqual(new List<Collection> { c1, c2, c3 }, logic.GetCollectionsInYear(2015));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2000));

            Assert.IsNull(logic.GetPriceOfCollection(null));
            Assert.IsNull(logic.GetPriceOfCollection(c4));
            Assert.That(logic.GetPriceOfCollection(c2), Is.EqualTo(25.0).Within(0.0001));

            Assert.IsNull(logic.GetRatingOfCollection(null));
            Assert.IsNull(logic.GetRatingOfCollection(c4));
            Assert.That(logic.GetRatingOfCollection(c2), Is.EqualTo(4.5).Within(0.0001));

            Assert.IsNull(logic.SelectBookFromCollection(null, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c4, BookFilter.MostExpensive));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.MostExpensive), Is.EqualTo(b6));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.LeastExpensive), Is.EqualTo(b4));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.HighestRated), Is.EqualTo(b6));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.LowestRated), Is.EqualTo(b4));

            Assert.That(logic.SelectCollection(BookFilter.MostExpensive).Key, Is.EqualTo(30).Within(0.0001));
            Assert.That(logic.SelectCollection(BookFilter.MostExpensive).Value, Is.EqualTo(c3));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive).Key, Is.EqualTo(8).Within(0.0001));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive).Value, Is.EqualTo(c1));
            Assert.That(logic.SelectCollection(BookFilter.HighestRated).Key, Is.EqualTo(4.5).Within(0.0001));
            Assert.That(logic.SelectCollection(BookFilter.HighestRated).Value, Is.EqualTo(c2));
            Assert.That(logic.SelectCollection(BookFilter.LowestRated).Key, Is.EqualTo(3.0).Within(0.0001));
            Assert.That(logic.SelectCollection(BookFilter.LowestRated).Value, Is.EqualTo(c1));

            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series).Key, Is.EqualTo(30).Within(0.0001));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series).Value, Is.EqualTo(c3));

            Assert.IsNull(logic.ConvertCollectionToExtendedCollection(null));
            var convertedpublisher = logic.ConvertCollectionToExtendedCollection(c3);
            Assert.That(convertedpublisher.CollectionID, Is.EqualTo(3));
            Assert.That(convertedpublisher.IsSeries, Is.EqualTo(c3.IsSeries));
            Assert.That(convertedpublisher.Price, Is.EqualTo(30).Within(0.0001));
            Assert.That(convertedpublisher.Rating, Is.EqualTo(3.5).Within(0.0001));
            Assert.AreEqual(c3.Books, convertedpublisher.Books);
            Assert.AreEqual(new List<Author> { a1, a2, a4, a5, a7, a8, a9, a10 }, convertedpublisher.Authors);

            var extendedpublishers = logic.GetAllAsExtendedCollection();
            Assert.That(extendedpublishers.FirstOrDefault().CollectionID, Is.EqualTo(1));
            Assert.That(extendedpublishers.FirstOrDefault().IsSeries, Is.EqualTo(c1.IsSeries));
            Assert.That(extendedpublishers.FirstOrDefault().Price, Is.EqualTo(8).Within(0.0001));
            Assert.That(extendedpublishers.FirstOrDefault().Rating, Is.EqualTo(3.0).Within(0.0001));
            Assert.AreEqual(c1.Books, extendedpublishers.FirstOrDefault().Books);
            Assert.AreEqual(new List<Author> { a2, a3, a5, a8, a10 }, extendedpublishers.FirstOrDefault().Authors);
        }

        [Test]
        public void EmptyDBSetLogicTest()
        {
            collectionMock.Setup(c => c.ReadAll()).Returns(Enumerable.Empty<Collection>().AsQueryable());


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetAllNonSeries());
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetAllSeries());
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsBetweenYears(2010, 2013));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2015));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2000));

            Assert.IsNull(logic.GetPriceOfCollection(null));
            Assert.IsNull(logic.GetPriceOfCollection(c4));
            Assert.That(logic.GetPriceOfCollection(c2), Is.EqualTo(25.0).Within(0.0001));

            Assert.IsNull(logic.GetRatingOfCollection(null));
            Assert.IsNull(logic.GetRatingOfCollection(c4));
            Assert.That(logic.GetRatingOfCollection(c2), Is.EqualTo(4.5).Within(0.0001));

            Assert.IsNull(logic.SelectBookFromCollection(null, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c4, BookFilter.MostExpensive));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.MostExpensive), Is.EqualTo(b6));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.LeastExpensive), Is.EqualTo(b4));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.HighestRated), Is.EqualTo(b6));
            Assert.That(logic.SelectBookFromCollection(c2, BookFilter.LowestRated), Is.EqualTo(b4));

            Assert.That(logic.SelectCollection(BookFilter.MostExpensive), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.HighestRated), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LowestRated), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));

            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));

            Assert.IsNull(logic.ConvertCollectionToExtendedCollection(null));
            var convertedpublisher = logic.ConvertCollectionToExtendedCollection(c3);
            Assert.That(convertedpublisher.CollectionID, Is.EqualTo(3));
            Assert.That(convertedpublisher.IsSeries, Is.EqualTo(c3.IsSeries));
            Assert.That(convertedpublisher.Price, Is.EqualTo(30).Within(0.0001));
            Assert.That(convertedpublisher.Rating, Is.EqualTo(3.5).Within(0.0001));
            Assert.AreEqual(c3.Books, convertedpublisher.Books);
            Assert.AreEqual(new List<Author> { a1, a2, a4, a5, a7, a8, a9, a10 }, convertedpublisher.Authors);

            Assert.AreEqual(Enumerable.Empty<ExtendedCollection>(), logic.GetAllAsExtendedCollection());
        }

        [Test]
        public void EmptyOtherDBSetLogicTest()
        {
            foreach (var item in collections)
            {
                item.Books = new List<Book>();
            }


            Assert.That(logic.Count, Is.EqualTo(4));
            Assert.IsFalse(logic.IsEmpty);

            Assert.AreEqual(new List<Collection> { c1, c2, c4 }, logic.GetAllNonSeries());
            Assert.AreEqual(new List<Collection> { c3 }, logic.GetAllSeries());
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsBetweenYears(2010, 2013));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2015));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2000));

            Assert.IsNull(logic.GetPriceOfCollection(null));
            Assert.IsNull(logic.GetPriceOfCollection(c4));
            Assert.IsNull(logic.GetPriceOfCollection(c2));

            Assert.IsNull(logic.GetRatingOfCollection(null));
            Assert.IsNull(logic.GetRatingOfCollection(c4));
            Assert.IsNull(logic.GetRatingOfCollection(c2));

            Assert.IsNull(logic.SelectBookFromCollection(null, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c4, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.LeastExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.HighestRated));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.LowestRated));

            Assert.That(logic.SelectCollection(BookFilter.MostExpensive), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.HighestRated), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LowestRated), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));

            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));

            Assert.IsNull(logic.ConvertCollectionToExtendedCollection(null));
            var convertedpublisher = logic.ConvertCollectionToExtendedCollection(c3);
            Assert.That(convertedpublisher.CollectionID, Is.EqualTo(3));
            Assert.That(convertedpublisher.IsSeries, Is.EqualTo(c3.IsSeries));
            Assert.IsNull(convertedpublisher.Price);
            Assert.IsNull(convertedpublisher.Rating);
            Assert.IsEmpty(convertedpublisher.Books);
            Assert.AreEqual(Enumerable.Empty<Author>(), convertedpublisher.Authors);

            var extendedpublishers = logic.GetAllAsExtendedCollection();
            Assert.That(extendedpublishers.FirstOrDefault().CollectionID, Is.EqualTo(1));
            Assert.That(extendedpublishers.FirstOrDefault().IsSeries, Is.EqualTo(c1.IsSeries));
            Assert.IsNull(extendedpublishers.FirstOrDefault().Price);
            Assert.IsNull(extendedpublishers.FirstOrDefault().Rating);
            Assert.IsEmpty(extendedpublishers.FirstOrDefault().Books);
            Assert.AreEqual(Enumerable.Empty<Author>(), extendedpublishers.FirstOrDefault().Authors);
        }

        [Test]
        public void EmptyAllDBSetLogicTest()
        {
            collectionMock.Setup(c => c.ReadAll()).Returns(Enumerable.Empty<Collection>().AsQueryable());

            foreach (var item in collections)
            {
                item.Books = new List<Book>();
            }


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);


            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetAllNonSeries());
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetAllSeries());
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsBetweenYears(2010, 2013));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2015));
            Assert.AreEqual(Enumerable.Empty<Collection>(), logic.GetCollectionsInYear(2000));

            Assert.IsNull(logic.GetPriceOfCollection(null));
            Assert.IsNull(logic.GetPriceOfCollection(c4));
            Assert.IsNull(logic.GetPriceOfCollection(c2));

            Assert.IsNull(logic.GetRatingOfCollection(null));
            Assert.IsNull(logic.GetRatingOfCollection(c4));
            Assert.IsNull(logic.GetRatingOfCollection(c2));

            Assert.IsNull(logic.SelectBookFromCollection(null, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c4, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.MostExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.LeastExpensive));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.HighestRated));
            Assert.IsNull(logic.SelectBookFromCollection(c2, BookFilter.LowestRated));

            Assert.That(logic.SelectCollection(BookFilter.MostExpensive), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.HighestRated), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LowestRated), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));

            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));
            Assert.That(logic.SelectCollection(BookFilter.LeastExpensive, CollectionFilter.Series), Is.EqualTo(new KeyValuePair<double, Collection>(0, null)));

            Assert.IsNull(logic.ConvertCollectionToExtendedCollection(null));
            var convertedpublisher = logic.ConvertCollectionToExtendedCollection(c3);
            Assert.That(convertedpublisher.CollectionID, Is.EqualTo(3));
            Assert.That(convertedpublisher.IsSeries, Is.EqualTo(c3.IsSeries));
            Assert.IsNull(convertedpublisher.Price);
            Assert.IsNull(convertedpublisher.Rating);
            Assert.IsEmpty(convertedpublisher.Books);
            Assert.AreEqual(Enumerable.Empty<Author>(), convertedpublisher.Authors);

            Assert.AreEqual(Enumerable.Empty<ExtendedCollection>(), logic.GetAllAsExtendedCollection());
        }
    }
}
