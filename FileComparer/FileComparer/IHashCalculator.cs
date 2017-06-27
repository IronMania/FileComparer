namespace FileComparer
{
    /// <summary>
    /// Calculates hash for a file
    /// </summary>
    public interface IHashCalculator
    {
        /// <summary>
        /// Calculates hash for files
        /// </summary>
        /// <param name="file">path to the file</param>
        /// <returns>hash</returns>
        string GetHashSum(string file);
    }
}