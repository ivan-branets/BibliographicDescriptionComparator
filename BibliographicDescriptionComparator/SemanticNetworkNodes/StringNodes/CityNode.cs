using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.StringNodes
{
    public class CityNode : StringNode
    {
        public CityNode(string city) : base(city)
        {
        }

        public override double Compare(Node node)
        {
            return base.Compare(node) / 10;
        }
    }
}
