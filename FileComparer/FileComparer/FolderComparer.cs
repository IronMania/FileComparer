using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileComparer
{
    public class FolderComparer
    {
        private readonly IDuplicateCounter _counter;

        public FolderComparer(IDuplicateCounter calculator)
        {
            _counter = calculator;
        }

        public void Compare(IEnumerable<string> folders, List<string> pattern)
        {
            foreach (var folder in folders)
                AddHashsToCounter(folder,pattern);

            var doublets = _counter.GetAllDuplicates();
            if (doublets.Count == 0)
            {
                Console.WriteLine("No Duplicates found!");
                return;
            }
            Console.WriteLine("Found fallowing Duplicates:");
            foreach (var doublet in doublets)
                doublet.Print();
        }

        private void AddHashsToCounter(string folder, List<string> patterns)
        {
            if (!Directory.Exists(folder))
            {
                Console.WriteLine($"Could not find folder: {folder}");
                return;
            }
            foreach (var p in patterns)
            {
                var files = Directory.EnumerateFiles(folder, p, SearchOption.AllDirectories);
                foreach (var file in files)
                    _counter.Add(file);
            }
            

        }
    }
}