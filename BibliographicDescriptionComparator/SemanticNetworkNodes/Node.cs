namespace SemanticNetwork.SemanticNodes
{
    public abstract class Node
    {
        public abstract double Compare(Node node);
        public abstract string GetValue();
    }
}
