using System;
using System.Linq;
using NUnit.Framework;

namespace RestSharp.SnakeCaseSerializer.NetCore.Tests
{
    [TestFixture]
    public class SnakeCaseSerializationStrategyTests
    {
        public SnakeCaseSerializationStrategyTests()
        {
            SimpleJson.CurrentJsonSerializerStrategy = new SnakeCaseSerializationStrategy();
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
    }
}
