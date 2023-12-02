using Moq;
using NUnit.Framework;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Test.LogicTest
{
    [TestFixture]
    class PublisherLogicTest
    {
        private IPublisherLogic logic;
        private Mock<IRepository<Publisher>> publisherMock;

        private Book b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15;
        private Author a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;
        private Collection c1, c2, c3;
        private Publisher p1, p2, p3, p4;

        private IEnumerable<Publisher> publishers;

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

            publishers = new List<Publisher>(new[] { p1, p2, p3, p4 });
        }

        [SetUp]
        public void SetUp()
        {
            publisherMock = new Mock<IRepository<Publisher>>();

            publisherMock.Setup(p => p.ReadAll()).Returns(publishers.AsQueryable());

            publisherMock.Setup(p => p.Read(It.IsNotIn(publishers.Select(p => p.PublisherID)))).Throws(new InvalidOperationException());

            foreach (var item in publishers.Select(p => new { id = p.PublisherID, publisher = p }))
                publisherMock.Setup(p => p.Read(item.id)).Returns(item.publisher);

            publisherMock.Setup(p => p.Update(It.IsNotIn(publishers))).Throws(new InvalidOperationException());

            publisherMock.Setup(p => p.Delete(It.IsNotIn(publishers.Select(p => p.PublisherID)))).Throws(new InvalidOperationException());

            logic = new PublisherLogic(publisherMock.Object);

            p1.Books = new List<Book> { b1, b2, b9 };
            p2.Books = new List<Book> { b3, b4, b6, b13 };
            p3.Books = new List<Book> { b7, b12, b15 };
            p4.Books = new List<Book>();
        }


        [Test]
        public void CreateTest()
        {
            var incorrect = new Publisher(1, "Lorem ipsum dolor sit amet, consectetur adipiscing dui.");
            var publisher1 = new Publisher(1, "First");
            var publisher2 = new Publisher(10, "Second");

            Assert.IsNull(logic.Create(null));
            Assert.IsNull(logic.Create(incorrect));
            Assert.That(logic.Create(publisher1), Is.EqualTo(5));
            Assert.That(logic.Create(publisher2), Is.EqualTo(10));
            publisherMock.Setup(p => p.Read(5)).Returns(publisher1);
            publisherMock.Setup(p => p.ReadAll()).Returns(publishers.Append(publisher1).AsQueryable());
            Assert.That(logic.Create(publisher1), Is.EqualTo(5));

            publisherMock.Verify(p => p.Create(incorrect), Times.Never);
            publisherMock.Verify(p => p.Create(publisher1), Times.Once);
            publisherMock.Verify(p => p.Create(publisher2), Times.Once);
            publisherMock.Verify(p => p.Read(1), Times.Once);
            publisherMock.Verify(p => p.Read(5), Times.Never);
            publisherMock.Verify(p => p.Read(10), Times.Once);
        }

        [Test]
        public void ReadTest()
        {
            var notadded = new Publisher(10, "First");

            Assert.IsNull(logic.Read(0));
            Assert.IsNull(logic.Read(notadded.PublisherID));
            Assert.That(logic.Read(1), Is.EqualTo(p1));
            Assert.AreEqual(publishers, logic.ReadAll());

            publisherMock.Verify(p => p.Read(0), Times.Once);
            publisherMock.Verify(p => p.Read(1), Times.Once);
            publisherMock.Verify(p => p.Read(10), Times.Once);
            publisherMock.Verify(p => p.ReadAll(), Times.Once);
        }

        [Test]
        public void UpdateTest()
        {
            var notadded = new Publisher(10, "First");
            var publisher1 = p1; publisher1.PublisherName = "Updated Normal Publisher";

            Assert.IsFalse(logic.Update(null));
            Assert.IsFalse(logic.Update(notadded));
            Assert.IsTrue(logic.Update(publisher1));

            publisherMock.Verify(p => p.Update(publisher1), Times.Once);
            publisherMock.Verify(p => p.Update(notadded), Times.Never);
        }

        [Test]
        public void DeleteTest()
        {
            publisherMock.Setup(p => p.Read(It.IsAny<int>())).Throws(new InvalidOperationException());
            var notadded = new Publisher(0, "First");

            Assert.IsFalse(logic.Delete(null));
            Assert.IsFalse(logic.Delete(notadded));
            Assert.IsTrue(logic.Delete(p1));

            publisherMock.Verify(p => p.Delete(0), Times.Once);
            publisherMock.Verify(p => p.Delete(1), Times.Once);
            publisherMock.Verify(p => p.Delete(2), Times.Never);
        }


        [Test]
        public void LogicTest()
        {
            Assert.That(logic.Count, Is.EqualTo(4));
            Assert.IsFalse(logic.IsEmpty);

            Assert.IsNull(logic.GetRatingOfPublisher(null));
            Assert.IsNull(logic.GetRatingOfPublisher(p1));
            Assert.That(logic.GetRatingOfPublisher(p2), Is.EqualTo(4.4).Within(0.0001));

            Assert.That(logic.GetHighestRatedPublisher().Key, Is.EqualTo(4.4).Within(0.0001));
            Assert.That(logic.GetHighestRatedPublisher().Value, Is.EqualTo(p2));
            Assert.That(logic.GetLowestRatedPublisher().Key, Is.EqualTo(3.25).Within(0.0001));
            Assert.That(logic.GetLowestRatedPublisher().Value, Is.EqualTo(p3));

            Assert.AreEqual(new List<Publisher> { p2, p3 }, logic.GetSeriesPublishers());
            Assert.AreEqual(new List<Publisher> { p3 }, logic.GetOnlySeriesPublishers());
            Assert.AreEqual(new List<Author> { a6, a4 }, logic.GetPermanentAuthors());

            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetAuthorsOfPublisher(null));
            Assert.AreEqual(new List<Author> { a1, a6 }, logic.GetAuthorsOfPublisher(p1));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(null));
            Assert.AreEqual(new List<Author> { a6 }, logic.GetPermanentAuthorsOfPublisher(p1));
            Assert.AreEqual(new List<Author> { a4 }, logic.GetPermanentAuthorsOfPublisher(p3));

            Assert.IsNull(logic.ConvertPublisherToExtendedPublisher(null));
            var convertedpublisher = logic.ConvertPublisherToExtendedPublisher(p3);
            Assert.That(convertedpublisher.PublisherID, Is.EqualTo(3));
            Assert.That(convertedpublisher.Website, Is.EqualTo(p3.Website));
            Assert.That(convertedpublisher.Rating, Is.EqualTo(3.25).Within(0.0001));
            Assert.AreEqual(p3.Books, convertedpublisher.Books);
            Assert.AreEqual(new List<Author> { a4, a5, a7, a8, a9, a10 }, convertedpublisher.Authors);

            var extendedpublishers = logic.GetAllAsExtendedPublisher();
            Assert.That(extendedpublishers.FirstOrDefault().PublisherID, Is.EqualTo(1));
            Assert.That(extendedpublishers.FirstOrDefault().Website, Is.EqualTo(p1.Website));
            Assert.IsNull(extendedpublishers.FirstOrDefault().Rating);
            Assert.AreEqual(p1.Books, extendedpublishers.FirstOrDefault().Books);
            Assert.AreEqual(new List<Author> { a1, a6 }, extendedpublishers.FirstOrDefault().Authors);
        }

        [Test]
        public void EmptyDBSetLogicTest()
        {
            publisherMock.Setup(p => p.ReadAll()).Returns(Enumerable.Empty<Publisher>().AsQueryable());


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.IsNull(logic.GetRatingOfPublisher(null));
            Assert.IsNull(logic.GetRatingOfPublisher(p1));
            Assert.That(logic.GetRatingOfPublisher(p2), Is.EqualTo(4.4).Within(0.0001));

            Assert.That(logic.GetHighestRatedPublisher(), Is.EqualTo(new KeyValuePair<double, Publisher>(0, null)));
            Assert.That(logic.GetLowestRatedPublisher(), Is.EqualTo(new KeyValuePair<double, Publisher>(0, null)));

            Assert.AreEqual(Enumerable.Empty<Publisher>(), logic.GetSeriesPublishers());
            Assert.AreEqual(Enumerable.Empty<Publisher>(), logic.GetOnlySeriesPublishers());
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthors());

            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetAuthorsOfPublisher(null));
            Assert.AreEqual(new List<Author> { a1, a6 }, logic.GetAuthorsOfPublisher(p1));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(null));
            Assert.AreEqual(new List<Author> { a6 }, logic.GetPermanentAuthorsOfPublisher(p1));
            Assert.AreEqual(new List<Author> { a4 }, logic.GetPermanentAuthorsOfPublisher(p3));

            Assert.IsNull(logic.ConvertPublisherToExtendedPublisher(null));
            var convertedpublisher = logic.ConvertPublisherToExtendedPublisher(p3);
            Assert.That(convertedpublisher.PublisherID, Is.EqualTo(3));
            Assert.That(convertedpublisher.Website, Is.EqualTo(p3.Website));
            Assert.That(convertedpublisher.Rating, Is.EqualTo(3.25).Within(0.0001));
            Assert.AreEqual(p3.Books, convertedpublisher.Books);
            Assert.AreEqual(new List<Author> { a4, a5, a7, a8, a9, a10 }, convertedpublisher.Authors);

            Assert.AreEqual(Enumerable.Empty<ExtendedPublisher>(), logic.GetAllAsExtendedPublisher());
        }

        [Test]
        public void EmptyOtherDBSetLogicTest()
        {
            foreach (var item in publishers)
            {
                item.Books = new List<Book>();
            }


            Assert.That(logic.Count, Is.EqualTo(4));
            Assert.IsFalse(logic.IsEmpty);

            Assert.IsNull(logic.GetRatingOfPublisher(null));
            Assert.IsNull(logic.GetRatingOfPublisher(p1));
            Assert.IsNull(logic.GetRatingOfPublisher(p2));

            Assert.That(logic.GetHighestRatedPublisher(), Is.EqualTo(new KeyValuePair<double, Publisher>(0, null)));
            Assert.That(logic.GetLowestRatedPublisher(), Is.EqualTo(new KeyValuePair<double, Publisher>(0, null)));

            Assert.AreEqual(Enumerable.Empty<Publisher>(), logic.GetSeriesPublishers());
            Assert.AreEqual(Enumerable.Empty<Publisher>(), logic.GetOnlySeriesPublishers());
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthors());

            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetAuthorsOfPublisher(null));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetAuthorsOfPublisher(p1));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(null));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(p1));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(p3));

            Assert.IsNull(logic.ConvertPublisherToExtendedPublisher(null));
            var convertedpublisher = logic.ConvertPublisherToExtendedPublisher(p3);
            Assert.That(convertedpublisher.PublisherID, Is.EqualTo(3));
            Assert.That(convertedpublisher.Website, Is.EqualTo(p3.Website));
            Assert.IsNull(convertedpublisher.Rating);
            Assert.IsEmpty(convertedpublisher.Books);
            Assert.AreEqual(Enumerable.Empty<Author>(), convertedpublisher.Authors);

            var extendedpublishers = logic.GetAllAsExtendedPublisher();
            Assert.That(extendedpublishers.FirstOrDefault().PublisherID, Is.EqualTo(1));
            Assert.That(extendedpublishers.FirstOrDefault().Website, Is.EqualTo(p1.Website));
            Assert.IsNull(extendedpublishers.FirstOrDefault().Rating);
            Assert.IsEmpty(extendedpublishers.FirstOrDefault().Books);
            Assert.AreEqual(Enumerable.Empty<Author>(), extendedpublishers.FirstOrDefault().Authors);
        }

        [Test]
        public void EmptyAllDBSetLogicTest()
        {
            publisherMock.Setup(p => p.ReadAll()).Returns(Enumerable.Empty<Publisher>().AsQueryable());

            foreach (var item in publishers)
            {
                item.Books = new List<Book>();
            }


            Assert.That(logic.Count, Is.EqualTo(0));
            Assert.IsTrue(logic.IsEmpty);

            Assert.IsNull(logic.GetRatingOfPublisher(null));
            Assert.IsNull(logic.GetRatingOfPublisher(p1));
            Assert.IsNull(logic.GetRatingOfPublisher(p2));

            Assert.That(logic.GetHighestRatedPublisher(), Is.EqualTo(new KeyValuePair<double, Publisher>(0, null)));
            Assert.That(logic.GetLowestRatedPublisher(), Is.EqualTo(new KeyValuePair<double, Publisher>(0, null)));

            Assert.AreEqual(Enumerable.Empty<Publisher>(), logic.GetSeriesPublishers());
            Assert.AreEqual(Enumerable.Empty<Publisher>(), logic.GetOnlySeriesPublishers());
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthors());

            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetAuthorsOfPublisher(null));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetAuthorsOfPublisher(p1));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(null));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(p1));
            Assert.AreEqual(Enumerable.Empty<Author>(), logic.GetPermanentAuthorsOfPublisher(p3));

            Assert.IsNull(logic.ConvertPublisherToExtendedPublisher(null));
            var convertedpublisher = logic.ConvertPublisherToExtendedPublisher(p3);
            Assert.That(convertedpublisher.PublisherID, Is.EqualTo(3));
            Assert.That(convertedpublisher.Website, Is.EqualTo(p3.Website));
            Assert.IsNull(convertedpublisher.Rating);
            Assert.IsEmpty(convertedpublisher.Books);
            Assert.AreEqual(Enumerable.Empty<Author>(), convertedpublisher.Authors);

            Assert.AreEqual(Enumerable.Empty<ExtendedPublisher>(), logic.GetAllAsExtendedPublisher());
        }
    }
}
