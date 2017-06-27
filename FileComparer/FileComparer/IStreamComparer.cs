using System.IO;

namespace FileComparer
{

    /// <summary>
    /// Compares Streams for Equality of content
    /// </summary>
    public interface IStreamComparer
    {
        /// <summary>
        /// compares the Stream if the content is the same.
        /// </summary>
        /// <param name="stream1"></param>
        /// <param name="stream2"></param>
        /// <returns>true if they are the same. else false</returns>
        bool Compare(Stream stream1, Stream stream2);
    }
}