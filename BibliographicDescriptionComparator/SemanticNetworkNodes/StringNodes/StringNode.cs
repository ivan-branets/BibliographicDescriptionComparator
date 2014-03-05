using System;
using SemanticNetwork.Comparators;
using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.StringNodes
{
    public class StringNode : Node
    {
        public string Value { get; set; }

        public StringNode(string publisher)
        {
            Value = publisher;
            ComplexComparator = new ComplexComparator(Value);
        }

        public ComplexComparator ComplexComparator { get; set; }

        public override double Compare(Node node)
        {
            var stringNode = node as StringNode;
            if (stringNode == null)
                throw new Exception("Not string node");

            return ComplexComparator.Compare(stringNode.ComplexComparator);
        }

        public override string GetValue()
        {
            return Value;
        }
    }
}
