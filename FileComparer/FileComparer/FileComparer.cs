using System;
using System.IO;

namespace FileComparer
{
    public class FileComparer
    {
        private readonly IStreamComparer _comparer;

        public FileComparer(IStreamComparer streamComparer)
        {
            _comparer = streamComparer;
        }

        internal void CompareFiles(string filePath1, string filePath2)
        {
            Stream stream1 = null;
            Stream stream2 = null;
            try
            {
                stream1 = File.OpenRead(filePath1);
                stream2 = File.OpenRead(filePath2);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Could not find File: {e.FileName}");
                return;
            }
            finally
            {
                stream1?.Close();
                stream2?.Close();
            }
            

            var areStreamsEqual = _comparer.Compare(stream1, stream2);

            Console.WriteLine(areStreamsEqual ? "Files are the Same" : "Files are not the Same");

            stream1.Close();
            stream2.Close();
        }
    }
}