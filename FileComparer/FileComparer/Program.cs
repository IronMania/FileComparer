using System;

namespace FileComparer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(
                    "Arguments do not Match. Pass folders as Arguments to the program. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            var loader = new FolderComparer(new DuplicateCounter(new HashForFiles()));

            loader.Compare(args);

            Console.ReadKey();
        }
    }
}