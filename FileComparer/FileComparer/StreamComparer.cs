using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FileComparer
{
    public class HashSumComparer : IStreamComparer
    {
        public bool Compare(Stream stream1, Stream stream2)
        {
            using (var md5 = MD5.Create())
            {
                var hash1 = md5.ComputeHash(stream1);
                var hash2 = md5.ComputeHash(stream2);
                return !hash1.Where((t, i) => t != hash2[i]).Any();
            }
        }
    }
}