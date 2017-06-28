using System;
using System.Collections.Generic;
using NDesk.Options;

namespace FileComparer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var showHelp = false;
            var pattern = new List<string>();
            var p = new OptionSet
            {
                {
                    "p|pattern=", "the {pattern} which should be searched. multiple patterns: p=*.cs|*.md", v =>
                    {
                        Console.WriteLine($"Using pattern: {v}");
                        var patterns = v.Split('|');
                        pattern.AddRange(patterns);
                    }
                },

                {
                    "h|help|?", "show this message and exit",
                    v => showHelp = v != null
                }
            };
            var directories = p.Parse(args);

            if (showHelp || directories.Count == 0)
            {
                ShowHelp(p);
                return;
            }

            if (pattern.Count == 0)
            {
                pattern.Add("*.*");
            }

            var loader = new FolderComparer(new DuplicateCounter(new HashForFiles()));

            loader.Compare(directories, pattern);

            Console.ReadKey();
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: FileComparer.exe [Options] C:\\Folder1 ...");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}