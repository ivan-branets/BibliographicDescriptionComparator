using System;

namespace SemanticNetwork.Comparators
{
    public class ComplexComparator
    {
        private SymbolFrequencyComparator SymbolFrequencyComparator { get; set; }
        private SymbolBySymbolFrequencyComparator SymbolBySymbolFrequencyComparator { get; set; }

        public ComplexComparator(string text)
        {
            SymbolFrequencyComparator = new SymbolFrequencyComparator(text);
            SymbolBySymbolFrequencyComparator = new SymbolBySymbolFrequencyComparator(text);
        }

        public double Compare(ComplexComparator complexComparator)
        {
            var differenceSymbolFrequency =
                SymbolFrequencyComparator.Compare(complexComparator.SymbolFrequencyComparator);
            var differenceSymbolBySymbolFrequency =
                SymbolBySymbolFrequencyComparator.Compare(complexComparator.SymbolBySymbolFrequencyComparator);
            return (Math.Sqrt(Math.Pow(differenceSymbolFrequency, 4) + Math.Pow(differenceSymbolBySymbolFrequency, 3)));
        }
    }
}
