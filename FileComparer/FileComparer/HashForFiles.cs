using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileComparer
{
    public class HashForFiles : IHashCalculator
    {
        private readonly MD5 _md5;

        public HashForFiles()
        {
            _md5 = MD5.Create();
        }

        public string GetHashSum(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var hash = _md5.ComputeHash(stream);
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