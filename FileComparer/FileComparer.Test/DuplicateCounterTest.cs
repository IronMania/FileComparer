using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FileComparer.Test
{
    [TestFixture]
    public class DuplicateCounterTest
    {
        private Mock<IHashCalculator> _mock;
        private DuplicateCounter _serviceUnderTest;

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IHashCalculator>();
            _serviceUnderTest = new DuplicateCounter(_mock.Object);
        }
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
    }
}
