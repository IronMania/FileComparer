using System.Linq;
using Moq;
using NUnit.Framework;

namespace FileComparer.Test
{
    [TestFixture]
    public class DuplicateCounterTest
    {
        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IHashCalculator>();
            _serviceUnderTest = new DuplicateCounter(_mock.Object);
        }

        private Mock<IHashCalculator> _mock;
        private DuplicateCounter _serviceUnderTest;

        [Test]
        public void TestAddingSingleFileWillNotReturnADoublet()
        {
            //Assign
            _mock.Setup(calculator => calculator.GetHashSum(It.IsAny<string>())).Returns("TestHash");
            //Act
            _serviceUnderTest.Add("File1");
            //Assert
            Assert.That(_serviceUnderTest.GetAllDuplicates().ToList().Count, Is.EqualTo(0));
        }

        [Test]
        public void TestAddingTwoDifferentFilesDoesNotCreateDuplicate()
        {
            //Assign
            const string file1 = "File1";
            const string file2 = "File2";
            _mock.Setup(calculator => calculator.GetHashSum(It.Is<string>(s => s.Equals(file1)))).Returns("TestHash");
            _mock.Setup(calculator => calculator.GetHashSum(It.Is<string>(s => s.Equals(file2)))).Returns("TestHash2");
            _serviceUnderTest.Add("File1");
            //Act

            _serviceUnderTest.Add("File2");
            //Assert
            var duplicate = _serviceUnderTest.GetAllDuplicates().Count;
            Assert.That(duplicate, Is.EqualTo(0));
        }

        [Test]
        public void TestAddingTwoFilesWithSameHashHashAllFilesNames()
        {
            //Assign
            _mock.Setup(calculator => calculator.GetHashSum(It.IsAny<string>())).Returns("TestHash");
            _serviceUnderTest.Add("File1");
            //Act

            _serviceUnderTest.Add("File2");
            //Assert
            var duplicate = _serviceUnderTest.GetAllDuplicates().FirstOrDefault();
            Assert.That(duplicate.Doublets[0], Is.EqualTo("File1"));
            Assert.That(duplicate.Doublets[1], Is.EqualTo("File2"));
        }

        [Test]
        public void TestAddingTwoFilesWithSameHashReturnsDoublet()
        {
            //Assign
            _mock.Setup(calculator => calculator.GetHashSum(It.IsAny<string>())).Returns("TestHash");
            _serviceUnderTest.Add("File1");
            //Act

            _serviceUnderTest.Add("File2");
            //Assert
            Assert.That(_serviceUnderTest.GetAllDuplicates().ToList().Count, Is.EqualTo(1));
        }
    }
}