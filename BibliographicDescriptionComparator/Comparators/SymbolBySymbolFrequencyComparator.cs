using System.Threading.Tasks;

namespace SemanticNetwork.Comparators
{
    public class SymbolBySymbolFrequencyComparator : Comparator<string>
    {
        public SymbolBySymbolFrequencyComparator(string text)
            : base(text)
        { }

        public override void Explore(string text)
        {
            Frequency.Clear();

            if (text.Length == 0) return;

            Parallel.For(0, text.Length - 1, (i, loopStale) =>
                                             {
                                                 var key = text.Substring(i, 2);
                                                 lock (Frequency)
                                                 {
                                                     if (!Frequency.ContainsKey(key))
                                                         Frequency.Add(key, 0);
                                                 }
                                                 Frequency[key]++;
                                             });
        }
    }
}
