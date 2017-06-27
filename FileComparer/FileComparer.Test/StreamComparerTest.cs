using System.IO;
using NUnit.Framework;

namespace FileComparer.Test
{
    [TestFixture]
    public class StreamComparerTest
    {
        [SetUp]
        public void SetUp()
        {
            _comparer = new HashSumComparer();
        }

        private IStreamComparer _comparer;


        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [Test]
        public void Test_Compare_Different_Text()
        {
            //Assign

            using (var s = GenerateStreamFromString("Same Text1"))
            {
                using (var s2 = GenerateStreamFromString("Same Text"))
                {
                    //Act
                    var result = _comparer.Compare(s, s2);
                    //Assert
                    Assert.That(result, Is.False);
                }
            }
        }

        [Test]
        public void Test_Compare_Same_Text()
        {
            //Assign

            using (var s = GenerateStreamFromString("Same Text"))
            {
                using (var s2 = GenerateStreamFromString("Same Text"))
                {
                    //Act
                    var result = _comparer.Compare(s, s2);
                    //Assert
                    Assert.That(result, Is.True);
                }
            }
        }
    }
}