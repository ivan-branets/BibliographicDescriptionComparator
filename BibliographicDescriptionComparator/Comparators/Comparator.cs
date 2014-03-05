using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanticNetwork.Comparators
{
    public abstract class Comparator<T>
    {
        public Dictionary<T, int> Frequency { get; private set; }

        protected Comparator()
        {
            Frequency = new Dictionary<T, int>();
        }

        protected Comparator(string text)
            : this()
        {
            Explore(text);
        }

        public abstract void Explore(string text);

        public double Compare(Comparator<T> comparator)
        {
            var frequency1 = comparator.Frequency;
            var frequency2 = Frequency;

            var sum = frequency1.Count + frequency2.Count;

            if (frequency1.Count > frequency2.Count)
            {
                var tmp = frequency1;
                frequency1 = frequency2;
                frequency2 = tmp;
            }

            frequency2 = new Dictionary<T, int>(frequency2);

            double difference = 0;

            foreach (var f in frequency1)
            {
                if (frequency2.ContainsKey(f.Key))
                {
                    difference += Math.Pow(f.Value - frequency2[f.Key], 2);
                    frequency2.Remove(f.Key);
                }
                else
                    difference += f.Value;                
            }
            
            difference += frequency2.Sum(s => Math.Pow(s.Value, 1));

            return Math.Sqrt(difference) / sum;
        }
    }
}
