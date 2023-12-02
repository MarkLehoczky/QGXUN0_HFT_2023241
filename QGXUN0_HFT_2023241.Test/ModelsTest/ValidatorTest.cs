using NUnit.Framework;
using QGXUN0_HFT_2023241.Models.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace QGXUN0_HFT_2023241.Test.ModelsTest
{
    [TestFixture]
    class ValidatorTest
    {
        internal class TestClass
        {
            [Required] public int? Required { get; set; }
            [StringLength(16)] public string StringLength { get; set; }
            [System.ComponentModel.DataAnnotations.Range(1, 10)] public int Range { get; set; }

            public TestClass(int? required, string stringLength, int range)
            {
                Required = required;
                StringLength = stringLength;
                Range = range;
            }
        }

        [Combinatorial]
        public void ValidTest(
            [Values(int.MinValue, -1, 0, 1, int.MaxValue)] int? required,
            [Values("", "text", "0123456789ABCDEF")] string stringLength,
            [Values(1, 5, 10)] int range)
        {
            var actual = new TestClass(required, stringLength, range);
            Assert.IsTrue(actual.IsValid());
            Assert.DoesNotThrow(() => actual.Validate());
        }

        [TestCase(null, "text", 5)]
        [TestCase(1, "0123456789ABCDEFG", 5)]
        [TestCase(1, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5)]
        [TestCase(1, "Lorem ipsum dolor sit amet, consectetur adipiscing dui.", 5)]
        [TestCase(1, "text", -1)]
        [TestCase(1, "text", 0)]
        [TestCase(1, "text", 11)]
        [TestCase(1, "text", 12)]
        [TestCase(1, "text", int.MinValue)]
        [TestCase(1, "text", int.MaxValue)]
        public void InValidTest(int? required, string stringLength, int range)
        {
            var actual = new TestClass(required, stringLength, range);
            Assert.IsFalse(actual.IsValid());
            Assert.Throws(typeof(ValidationException), () => actual.Validate());
        }

        [TestCase(null, "text", 5, typeof(RequiredAttribute))]
        [TestCase(1, "0123456789ABCDEFG", 5, typeof(StringLengthAttribute))]
        [TestCase(1, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5, typeof(StringLengthAttribute))]
        [TestCase(1, "text", -1, typeof(System.ComponentModel.DataAnnotations.RangeAttribute))]
        [TestCase(1, "text", 0, typeof(System.ComponentModel.DataAnnotations.RangeAttribute))]
        [TestCase(1, "text", 11, typeof(System.ComponentModel.DataAnnotations.RangeAttribute))]
        [TestCase(1, "text", 12, typeof(System.ComponentModel.DataAnnotations.RangeAttribute))]
        [TestCase(1, "text", int.MinValue, typeof(System.ComponentModel.DataAnnotations.RangeAttribute))]
        [TestCase(1, "text", int.MaxValue, typeof(System.ComponentModel.DataAnnotations.RangeAttribute))]
        public void IgnoredValidTest(int? required, string stringLength, int range, Type ignoreType)
        {
            var actual = new TestClass(required, stringLength, range);
            Assert.IsTrue(actual.IsValid(ignoreType));
            Assert.DoesNotThrow(() => actual.Validate(ignoreType));
        }

        [TestCase(null, "text", 5)]
        [TestCase(null, "0123456789ABCDEFG", 5)]
        [TestCase(null, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5)]
        [TestCase(1, "text", -1)]
        [TestCase(1, "text", 0)]
        [TestCase(1, "text", 11)]
        [TestCase(1, "text", 12)]
        [TestCase(1, "text", int.MinValue)]
        [TestCase(1, "text", int.MaxValue)]
        public void IgnoredInValidTest(int? required, string stringLength, int range)
        {
            var actual = new TestClass(required, stringLength, range);
            Assert.IsFalse(actual.IsValid(typeof(StringLengthAttribute)));
            Assert.Throws(typeof(ValidationException), () => actual.Validate(typeof(StringLengthAttribute)));
        }

        [Test]
        public void CustomTest()
        {
            TestClass actual = null;
            Assert.IsFalse(actual.IsValid());
            Assert.Throws(typeof(ArgumentNullException), () => { actual.Validate(); });
        }
    }
}
