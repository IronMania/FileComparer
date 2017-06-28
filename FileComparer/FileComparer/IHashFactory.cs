using System.Security.Cryptography;

namespace FileComparer
{
    /// <summary>
    /// factory for obtaining hashalgorithms
    /// </summary>
    public interface IHashFactory
    {
        HashAlgorithm GetMd5();
        HashAlgorithm GetSha256();
    }
}