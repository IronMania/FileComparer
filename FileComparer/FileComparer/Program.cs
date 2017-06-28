using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FileComparer.Md5;
using NDesk.Options;

namespace FileComparer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var showHelp = false;
            var pattern = new List<string>();

            string hashAlgorithm = null;
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
                    "a|algorithm=", "MD5 or Sha256. MD5 is default.",
                    v => { hashAlgorithm = v; }
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

            var container = CreateContainer(hashAlgorithm);
            var loader = container.Resolve<FolderComparer>();
            loader.Compare(directories, pattern);

            Console.WriteLine("Finished. Press any Key to exit.");
            Console.ReadKey();
        }

        private static WindsorContainer CreateContainer(string hashAlgorithm)
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This(),
                FromAssembly.Containing<Installer>(),
                FromAssembly.Containing<Sha256.Installer>()
            );
            container.Register(Classes.FromThisAssembly()
                .Pick()
                .WithServiceAllInterfaces()
                .WithServiceSelf()
                .ConfigureFor<HashForFiles>(
                    registration => registration.DependsOn(Dependency.OnComponent("hashAlgorithm", hashAlgorithm)))
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