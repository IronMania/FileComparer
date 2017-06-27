using System.Collections.Generic;
using System.Linq;

namespace FileComparer
{
    public class DuplicateCounter : IDuplicateCounter
    {
        private readonly IHashCalculator _calculator;
        private readonly Dictionary<string, Duplicates> _counter;

        public DuplicateCounter(IHashCalculator calculator)
        {
            _calculator = calculator;
            _counter = new Dictionary<string, Duplicates>();
        }

        public void Add(string filename)
        {
            var hash = _calculator.GetHashSum(filename);
            if (!_counter.ContainsKey(hash))
                _counter.Add(hash, new Duplicates(hash));
            _counter[hash].Add(filename);
        }

        public IList<Duplicates> GetAllDuplicates()
        {
            return _counter.Values.Where(counterValue => counterValue.IsDoublet).ToList();
        }
    }
}