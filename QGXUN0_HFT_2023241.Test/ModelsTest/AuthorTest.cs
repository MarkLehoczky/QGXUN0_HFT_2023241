using NUnit.Framework;
using QGXUN0_HFT_2023241.Models;
using System;

namespace QGXUN0_HFT_2023241.Test.ModelsTest
{
    [TestFixture]
    class AuthorTest
    {
        [Test]
        public void ConstructorTest()
        {
            var actual = new Author();

            Assert.That(actual.AuthorID, Is.EqualTo(0));
            Assert.IsNull(actual.AuthorName);
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.BookConnector);


            actual = new Author(1, "Name");

            Assert.That(actual.AuthorID, Is.EqualTo(1));
            Assert.That(actual.AuthorName, Is.EqualTo("Name"));
            Assert.IsEmpty(actual.Books);
            Assert.IsNull(actual.BookConnector);
        }

        [TestCaseSource(typeof(AuthorTestData), nameof(AuthorTestData.CorrectParseValues))]
        public void CorrectParseTest(string data, string splitString, bool validation, Author expected)
        {
            Author parse = Author.Parse(data, splitString, validation);
            bool successful = Author.TryParse(data, out Author tryparse, splitString, validation);

            Assert.That(parse, Is.EqualTo(expected));
            Assert.That(tryparse, Is.EqualTo(expected));
            Assert.IsTrue(successful);
        }

        [TestCaseSource(typeof(AuthorTestData), nameof(AuthorTestData.InCorrectParseValues))]
        public void InCorrectParseTest(string data, string splitString, bool validation, Type exception)
        {
            bool successful = Author.TryParse(data, out Author tryparse, splitString, validation);

            Assert.Throws(exception, () => { Author.Parse(data, splitString, validation); });
            Assert.IsNull(tryparse);
            Assert.IsFalse(successful);
        }

        [TestCaseSource(typeof(AuthorTestData), nameof(AuthorTestData.EqualsValues))]
        public bool EqualsTest(Author left, object right)
        {
            return left.Equals(right);
        }

        [TestCaseSource(typeof(AuthorTestData), nameof(AuthorTestData.ToStringValues))]
        public string ToStringTest(Author value)
        {
            return value.ToString();
        }

        [TestCaseSource(typeof(AuthorTestData), nameof(AuthorTestData.CompareToValues))]
        public int CompareToMethodTest(Author left, object right)
        {
            return left.CompareTo(right);
        }
    }
}
