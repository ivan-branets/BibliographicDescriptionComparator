using System;
using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes.NumericNodes
{
    public class PageRangeNode : Node
    {
        public NumericNode StartPage { get; set; }
        public NumericNode EndPage { get; set; }

        public PageRangeNode(string startPage, string endPage)
        {
            StartPage = new NumericNode(startPage);
            EndPage = new NumericNode(endPage);
        }

        public override double Compare(Node node)
        {
            var pageRange = node as PageRangeNode;
            if (pageRange == null)
                throw new Exception("Not page range node");

            return StartPage.Compare(pageRange.StartPage) + Math.Abs(EndPage.Compare(pageRange.EndPage));
        }

        public override string GetValue()
        {
            return string.Format("{0}-{1}", StartPage, EndPage);
        }
    }
}
