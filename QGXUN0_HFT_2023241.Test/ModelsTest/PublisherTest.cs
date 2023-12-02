using NUnit.Framework;
using QGXUN0_HFT_2023241.Models;
using System;

namespace QGXUN0_HFT_2023241.Test.ModelsTest
{
    [TestFixture]
    class PublisherTest
    {
        [Test]
        public void ConstructorTest()
        {
            var actual = new Publisher();

            Assert.That(actual.PublisherID, Is.EqualTo(0));
            Assert.IsNull(actual.PublisherName);
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.Website);


            actual = new Publisher(1, "Name");

            Assert.That(actual.PublisherID, Is.EqualTo(1));
            Assert.That(actual.PublisherName, Is.EqualTo("Name"));
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.Website);


            actual = new Publisher(1, "Name", "www.example.com");

            Assert.That(actual.PublisherID, Is.EqualTo(1));
            Assert.That(actual.PublisherName, Is.EqualTo("Name"));
            Assert.IsEmpty(actual.Books);
            Assert.That(actual.Website, Is.EqualTo("www.example.com"));
        }

        [TestCaseSource(typeof(PublisherTestData), nameof(PublisherTestData.CorrectParseValues))]
        public void CorrectParseTest(string data, string splitString, bool validation, Publisher expected)
        {
            Publisher parse = Publisher.Parse(data, splitString, validation);
            bool successful = Publisher.TryParse(data, out Publisher tryparse, splitString, validation);

            Assert.That(parse, Is.EqualTo(expected));
            Assert.That(tryparse, Is.EqualTo(expected));
            Assert.IsTrue(successful);
        }

        [TestCaseSource(typeof(PublisherTestData), nameof(PublisherTestData.InCorrectParseValues))]
        public void InCorrectParseTest(string data, string splitString, bool validation, Type exception)
        {
            bool successful = Publisher.TryParse(data, out Publisher tryparse, splitString, validation);

            Assert.Throws(exception, () => { Publisher.Parse(data, splitString, validation); });
            Assert.IsNull(tryparse);
            Assert.IsFalse(successful);
        }

        [TestCaseSource(typeof(PublisherTestData), nameof(PublisherTestData.EqualsValues))]
        public bool EqualsTest(Publisher left, object right)
        {
            return left.Equals(right);
        }

        [TestCaseSource(typeof(PublisherTestData), nameof(PublisherTestData.ToStringValues))]
        public string ToStringTest(Publisher value)
        {
            return value.ToString();
        }

        [TestCaseSource(typeof(PublisherTestData), nameof(PublisherTestData.CompareToValues))]
        public int CompareToMethodTest(Publisher left, object right)
        {
            return left.CompareTo(right);
        }
    }
}
