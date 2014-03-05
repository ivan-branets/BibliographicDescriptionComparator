using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.NumericNodes
{
    public class NumericNode : Node
    {
        public NumericNode(string number)
        {
            var normalizeStr = new StringBuilder();
            number.ToList().ForEach(s => { if (char.IsDigit(s)) normalizeStr.Append(s); });

            int value;

            if (!int.TryParse(normalizeStr.ToString(), out value))
                throw new Exception("Can not parse number");

            Value = value;
        }

        public int Value { get; set; }

        public override double Compare(Node node)
        {
            var year = node as NumericNode;
            if (year == null)
                throw new Exception("Not numeric node");

            var difference = CompareDifference(year);
            var digitDifference = CompareDigitDifference(year);

            double digitsCount = GetDigits(Value).Count + GetDigits(year.Value).Count;

            var min = difference < digitDifference ? difference : digitDifference;

            return Math.Sqrt(min / digitsCount) / 10;
        }

        public override string GetValue()
        {
            return Value.ToString();
        }

        private int CompareDifference(NumericNode numberNode)
        {
            return Math.Abs(numberNode.Value - Value);
        }

        private static List<byte> GetDigits(int number)
        {
            var digits = new List<byte>();
            var value = number;

            do
            {
                digits.Add((byte)(value % 10));
                value /= 10;
            }
            while (value > 0);

            return digits;
        }

        private int CompareDigitDifference(NumericNode numberNode)
        {
            var digitsY1 = GetDigits(Value);
            var digitsY2 = GetDigits(numberNode.Value);

            if (digitsY1.Count > digitsY2.Count)
            {
                var tmp = digitsY1;
                digitsY1 = digitsY2;
                digitsY2 = tmp;
            }

            var differentDigits1 = 0;

            digitsY1.ForEach(d =>
                                 {
                                     if (digitsY2.Contains(d)) digitsY2.Remove(d);
                                     else differentDigits1++;
                                 });

            return differentDigits1 + digitsY2.Count;
        }
    }
}
