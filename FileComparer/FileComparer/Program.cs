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
                    "Arguments do not Match. Pass files as Arguments to the program. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            var loader = new FileComparer(new HashSumComparer());

            loader.CompareFiles(args[0], args[1]);

            Console.ReadKey();
        }
    }
}