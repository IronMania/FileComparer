using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NDesk.Options;

namespace FileComparer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var showHelp = false;
            var pattern = new List<string>();
            var container = new WindsorContainer();
            container.Register(Classes.FromThisAssembly().Pick().WithServiceAllInterfaces().WithServiceSelf());
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
                },
                {
                    "a|algorithm=", "MD5 or Sha256 are supported. MD5 is default",
                    v =>
                    {
                        var algorithm = HashAlgorithm.Create(v.ToLower());
                        if (algorithm != null)
                        {
                            container.Register(Component.For<HashAlgorithm>().Instance(algorithm));
                        }
                        else
                        {
                            Console.WriteLine($"Algorithm {v} cannot be found. Using default MD5");
                        }
                    }
                }
            };
            var directories = p.Parse(args);

            if (showHelp || directories.Count == 0)
            {
                ShowHelp(p);
                return;
            }

            if (pattern.Count == 0)
                pattern.Add("*.*");
            var loader = container.Resolve<FolderComparer>();

            loader.Compare(directories, pattern);
            Console.WriteLine("Finished. Press any Key to exit.");
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