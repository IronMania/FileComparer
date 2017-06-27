using System;
using System.IO;

namespace FileComparer
{
    public class FolderComparer
    {
        private readonly IDuplicateCounter _counter;

        public FolderComparer(IDuplicateCounter calculator)
        {
            _counter = calculator;
        }

        public void Compare(string[] folders)
        {
            foreach (var folder in folders)
                AddHashsToCounter(folder);

            var doublets = _counter.GetAllDuplicates();
            if (doublets.Count == 0)
            {
                Console.WriteLine("No Duplicates found!");
                return;
            }
            Console.WriteLine("Found fallowing Duplicates:");
            foreach (var doublet in doublets)
            {
                doublet.Print();
            }
        }

        private void AddHashsToCounter(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Console.WriteLine($"Could not find folder: {folder}");
                return;
            }
            var files = Directory.EnumerateFiles(folder);
            foreach (var file in files)
                _counter.Add(file);
        }
    }
}