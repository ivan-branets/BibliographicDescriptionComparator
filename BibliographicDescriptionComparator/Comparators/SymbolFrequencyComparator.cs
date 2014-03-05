using System.Threading.Tasks;

namespace SemanticNetwork.Comparators
{
    public class SymbolFrequencyComparator : Comparator<char>
    {
        public SymbolFrequencyComparator(string text)
            : base(text)
        {
        }

        public override void Explore(string text)
        {
            Parallel.ForEach(text, c =>
                                       {
                                           lock (Frequency)
                                           {
                                               if (!Frequency.ContainsKey(c))
                                                   Frequency.Add(c, 0);
                                           }
                                            Frequency[c]++;
                                       });
        }
    }
}
