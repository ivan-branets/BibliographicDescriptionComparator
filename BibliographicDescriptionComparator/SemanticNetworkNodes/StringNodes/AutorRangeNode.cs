using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.StringNodes
{
    public class AutorRangeNode : Node
    {
        public List<AutorNode> Value { get; set; }

        public AutorRangeNode(IEnumerable<string> autors)
        {
            Value = new List<AutorNode>();
            autors.ToList().ForEach(a => Value.Add(new AutorNode(a)));
        }

        public override double Compare(Node node)
        {
            if (!(node is AutorRangeNode)) 
                throw new Exception("Not autors node");

            var autors1 = (node as AutorRangeNode).Value;
            var autors2 = Value;

            double autorsCount = autors1.Count + autors2.Count;

            if (autors1.Count > autors2.Count)
            {
                var tmp = autors1;
                autors1 = autors2;
                autors2 = tmp;
            }

            autors2 = new List<AutorNode>(autors2);

            var differentAutots1 = 0;

            autors1.ForEach(a =>
                                {
                                    var removableAutor = autors2.FirstOrDefault(ra => ra == a);
                                    if (removableAutor != null)
                                        autors2.Remove(removableAutor);
                                    else
                                        differentAutots1++;
                                });

            return Math.Sqrt((differentAutots1 + autors2.Count) / autorsCount) / 10;
        }

        public override string GetValue()
        {
            var strBuilder = new StringBuilder();
            Value.ForEach(a => strBuilder.AppendFormat("{0}, ", a.GetValue()));
            if (strBuilder.Length <= 2) return "";
            return strBuilder.ToString().Substring(0, strBuilder.Length - 2);
        }
    }
}
