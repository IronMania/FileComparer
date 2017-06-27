using System.IO;

namespace FileComparer
{
    public interface IStreamComparer
    {
        bool Compare(Stream stream1, Stream stream2);
    }
}