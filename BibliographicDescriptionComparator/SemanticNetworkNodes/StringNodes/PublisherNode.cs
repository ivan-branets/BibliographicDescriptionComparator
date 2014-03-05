using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.StringNodes
{
    public class PublisherNode : StringNode
    {
        public PublisherNode(string publisher) : base(publisher)
        {
        }

        public override double Compare(Node node)
        {
            return base.Compare(node) / 10;
        }
    }
}
