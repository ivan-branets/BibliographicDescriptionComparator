using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.StringNodes
{
    public class AutorNode : StringNode
    {
        public AutorNode(string autor) : base(autor)
        {
        }

        public override double Compare(Node node)
        {
            return base.Compare(node) / 10;
        }
    }
}
