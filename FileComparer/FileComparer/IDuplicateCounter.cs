using System.Collections.Generic;

namespace FileComparer
{
    /// <summary>
    /// Counter for duplicate files
    /// </summary>
    public interface IDuplicateCounter
    {
        /// <summary>
        /// Adds the file to the Counter
        /// </summary>
        /// <param name="filename"></param>
        void Add(string filename);
        /// <summary>
        /// Gets all added Duplicates
        /// </summary>
        /// <returns>list of duplicates</returns>
        IList<Duplicates> GetAllDuplicates();
    }
}