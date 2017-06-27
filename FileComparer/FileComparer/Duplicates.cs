using System;
using System.Collections.Generic;

namespace FileComparer
{
    public class Duplicates
    {
        private readonly string _hash;

        public Duplicates(string hash)
        {
            _hash = hash;
            Doublets = new List<string>();
        }

        public IList<string> Doublets { get; }

        public bool IsDoublet => Doublets.Count > 1;

        public void Add(string fileName)
        {
            Doublets.Add(fileName);
        }

        public void Print()
        {
            Console.WriteLine($"{Environment.NewLine}Hash: {_hash}");
            var fileCounter = 1;
            foreach (var file in Doublets)
            {
                Console.WriteLine($"File{fileCounter}: {file}");
                fileCounter++;
            }
        }
    }
}