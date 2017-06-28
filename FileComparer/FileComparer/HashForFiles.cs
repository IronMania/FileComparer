using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileComparer
{
    public class HashForFiles : IHashCalculator
    {
        private readonly IHashFactory _factory;
        private HashAlgorithm _hashAlgorithm;

        public HashForFiles(IHashFactory factory)
        {
            _factory = factory;
        }

        public string GetHashSum(string file)
        {
            if (_hashAlgorithm == null)
            {
                GetHashAlgorithm();
            }

            using (var stream = File.OpenRead(file))
            {
                var hash = _hashAlgorithm.ComputeHash(stream);
                var sBuilder = new StringBuilder();

                foreach (var data in hash)
                    sBuilder.Append(data.ToString("x2"));

                return sBuilder.ToString();
            }
        }



        private void GetHashAlgorithm()
        {
            Console.WriteLine("Choose algorithm for comparing files");
            Console.WriteLine("1) MD5 (Default)");
            Console.WriteLine("2) Sha256");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.D1)
            {
                Console.WriteLine("Using MD5");
                
            }
            else if (key.Key == ConsoleKey.D2)
            {
                Console.WriteLine("Using SHA256");
                _hashAlgorithm = _factory.GetSha256();
                return;
            }
            else
            {
                Console.WriteLine("Using default MD5");
            }
            _hashAlgorithm = _factory.GetMd5();
        }
    }
}