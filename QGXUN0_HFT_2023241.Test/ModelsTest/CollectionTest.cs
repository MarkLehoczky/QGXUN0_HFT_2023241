using NUnit.Framework;
using QGXUN0_HFT_2023241.Models;
using System;

namespace QGXUN0_HFT_2023241.Test.ModelsTest
{
    [TestFixture]
    class CollectionTest
    {
        [Test]
        public void ConstructorTest()
        {
            var actual = new Collection();

            Assert.That(actual.CollectionID, Is.EqualTo(0));
            Assert.IsNull(actual.CollectionName);
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.BookConnector);
            Assert.IsNull(actual.IsSeries);


            actual = new Collection(1, "Name");

            Assert.That(actual.CollectionID, Is.EqualTo(1));
            Assert.That(actual.CollectionName, Is.EqualTo("Name"));
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.BookConnector);
            Assert.IsNull(actual.IsSeries);


            actual = new Collection(1, "Name", false);

            Assert.That(actual.CollectionID, Is.EqualTo(1));
            Assert.That(actual.CollectionName, Is.EqualTo("Name"));
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.BookConnector);
            Assert.IsFalse(actual.IsSeries);

            actual = new Collection(1, "Name", true);

            Assert.That(actual.CollectionID, Is.EqualTo(1));
            Assert.That(actual.CollectionName, Is.EqualTo("Name"));
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.BookConnector);
            Assert.IsTrue(actual.IsSeries);
        }

        [TestCaseSource(typeof(CollectionTestData), nameof(CollectionTestData.CorrectParseValues))]
        public void CorrectParseTest(string data, string splitString, bool validation, Collection expected)
        {
            Collection parse = Collection.Parse(data, splitString, validation);
            bool successful = Collection.TryParse(data, out Collection tryparse, splitString, validation);

            Assert.That(parse, Is.EqualTo(expected));
            Assert.That(tryparse, Is.EqualTo(expected));
            Assert.IsTrue(successful);
        }

        [TestCaseSource(typeof(CollectionTestData), nameof(CollectionTestData.InCorrectParseValues))]
        public void InCorrectParseTest(string data, string splitString, bool validation, Type exception)
        {
            bool successful = Collection.TryParse(data, out Collection tryparse, splitString, validation);

            Assert.Throws(exception, () => { Collection.Parse(data, splitString, validation); });
            Assert.IsNull(tryparse);
            Assert.IsFalse(successful);
        }

        [TestCaseSource(typeof(CollectionTestData), nameof(CollectionTestData.EqualsValues))]
        public bool EqualsTest(Collection left, object right)
        {
            return left.Equals(right);
        }

        [TestCaseSource(typeof(CollectionTestData), nameof(CollectionTestData.ToStringValues))]
        public string ToStringTest(Collection value)
        {
            return value.ToString();
        }

        [TestCaseSource(typeof(CollectionTestData), nameof(CollectionTestData.CompareToValues))]
        public int CompareToMethodTest(Collection left, object right)
        {
            return left.CompareTo(right);
        }
    }
}
