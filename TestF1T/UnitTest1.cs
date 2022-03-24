using NUnit.Framework;

namespace TestF1T
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            F1TMock.Program.Main();
            Assert.Pass();
        }
    }
}