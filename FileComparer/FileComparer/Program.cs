using System;
using System.Collections.Generic;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using Castle.Windsor.Installer;
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
                pattern.Add("*.*");

            var container = CreateContainer();

            var loader =
                container.Resolve<FolderComparer>();
            loader.Compare(directories, pattern);

            Console.WriteLine("Finished. Press any Key to exit.");
            Console.ReadKey();
        }

        private static WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Install(FromAssembly.This(),
                FromAssembly.Containing<Md5.Installer>(),
                FromAssembly.Containing<Sha256.Installer>()
            );

            return container;
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