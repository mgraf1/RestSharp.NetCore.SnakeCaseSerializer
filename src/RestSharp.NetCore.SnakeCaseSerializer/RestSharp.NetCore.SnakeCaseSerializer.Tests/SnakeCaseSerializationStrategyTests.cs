using System;
using System.Linq;
using NUnit.Framework;

namespace RestSharp.SnakeCaseSerializer.NetCore.Tests
{
    [TestFixture]
    public class SnakeCaseSerializationStrategyTests
    {
        private SnakeCaseSerializationStrategy strategyUnderTest;

        [SetUp]
        public void SetUp()
        {
            this.strategyUnderTest = new SnakeCaseSerializationStrategy();
            SimpleJson.CurrentJsonSerializerStrategy = this.strategyUnderTest;
        }

        [TearDown]
        public void TearDown()
        {
            this.strategyUnderTest = null;
        }

        [Test]
        public void Should_SerializeProperties_As_SnakeCase()
        {
            var json = SimpleJson.SerializeObject(new
            {
                Title = "Sir",
                FirstName = "Digby",
                LastName = "Chicken Ceasar"
            });
            const string expected =
                "{\"title\":\"Sir\",\"first_name\":\"Digby\",\"last_name\":\"Chicken Ceasar\"}";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void Should_HandleNumbers_In_PropertyName()
        {
            var json = SimpleJson.SerializeObject(new
            {
                Number1Property = "test"
            });
            const string expected = "{\"number_1_property\":\"test\"}";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void WhenIgnoringNullValues_Should_Not_Serialize_Null_Properties()
        {
            this.strategyUnderTest.IgnoreNullProperties = true;

            var json = SimpleJson.SerializeObject(new TestData
            {
                SomeProperty = "test",
                SomeOtherProperty = null
            });
            const string expected = "{\"some_property\":\"test\"}";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void WhenNotIgnoringNullValues_Should_Serialize_Null_Properties()
        {
            this.strategyUnderTest.IgnoreNullProperties = false;

            var json = SimpleJson.SerializeObject(new TestData
            {
                SomeProperty = "test",
                SomeOtherProperty = null
            });
            const string expected = "{\"some_property\":\"test\",\"some_other_property\":null}";

            Assert.AreEqual(expected, json);
        }

        private class TestData
        {
            public string SomeProperty { get; set; }

            public string SomeOtherProperty { get; set; }
        }
    }
}
