using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileComparer
{
    public class HashForFiles : IHashCalculator
    {
        private readonly HashAlgorithm _hashAlgorithm;

        public HashForFiles(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public HashForFiles()
        {
            _hashAlgorithm = MD5.Create();
        }

        public string GetHashSum(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var hash = _hashAlgorithm.ComputeHash(stream);
                var sBuilder = new StringBuilder();

                foreach (var data in hash)
                {
                    sBuilder.Append(data.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}