using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SemanticNetwork.SemanticNetworkNodes.NumericNodes;
using SemanticNetwork.SemanticNetworkNodes.StringNodes;
using SemanticNetwork.SemanticNodes;

namespace SemanticNetwork.SemanticNetworkNodes
{
    public class BibliographicDescriptionNode : Node
    {
        public TitleNode TitleNode { get; set; }
        public AutorRangeNode AutorRangeNode { get; set; }
        public PublisherNode PublisherNode { get; set; }
        public CityNode CityNode { get; set; }
        public YearNode YearNode { get; set; }
        public PageCountNode PageCountNode { get; set; }

        public BibliographicDescriptionNode(string title, IEnumerable<string> autors, string publisher, string city, string year, string pageCount)
        {
            TitleNode = new TitleNode(title);
            AutorRangeNode = new AutorRangeNode(autors);
            PublisherNode = new PublisherNode(publisher);
            CityNode = new CityNode(city);
            YearNode = new YearNode(year);
            PageCountNode = new PageCountNode(pageCount);
        }

        public BibliographicDescriptionNode(IDictionary<string, string> dictionary)
            : this(dictionary["title"],
                dictionary["autors"].Split(new [] {",", ", "}, StringSplitOptions.RemoveEmptyEntries),
                dictionary["publisher"], dictionary["city"], dictionary["year"], dictionary["pageCount"])
        {
            
        }

        public double Compare(Node node, XElement table)
        {
            var bibliographicDescriptionNode = node as BibliographicDescriptionNode;
            if (bibliographicDescriptionNode == null)
                throw new Exception("Not Bibliographic DescriptionNode");

            var nodes1 = new Node[] { TitleNode, AutorRangeNode, PublisherNode, CityNode, YearNode, PageCountNode };
            var nodes2 = new Node[]
                             {
                                 bibliographicDescriptionNode.TitleNode, bibliographicDescriptionNode.AutorRangeNode,
                                 bibliographicDescriptionNode.PublisherNode, bibliographicDescriptionNode.CityNode,
                                 bibliographicDescriptionNode.YearNode, bibliographicDescriptionNode.PageCountNode
                             };

            double difference = 0;

            var header = new XElement("tr");
            header.Add();
            table.Add(header);
            var headerCol = new XElement("td", "---------------------------------------------------");
            headerCol.Add(new XAttribute("colspan", "3"));
            header.Add(headerCol);


            for (var i = 0; i < nodes1.Length; i++)
            {
                var n1 = nodes1[i];
                var n2 = nodes2[i];

                var nodeDifference = n1.Compare(n2);
                difference += nodeDifference;

                var row = new XElement("tr");
                table.Add(row);

                row.Add(new XElement("td", n1.GetValue()));
                row.Add(new XElement("td", n2.GetValue()));
                row.Add(new XElement("td", nodeDifference));
            }

            var summary = new XElement("tr");
            table.Add(summary);

            summary.Add(new XElement("td", new XElement("b", "Total Difference:")));
            summary.Add(new XElement("td", ""));
            summary.Add(new XElement("td", new XElement("b", difference)));

            return difference;
        }

        public override double Compare(Node node)
        {
            var bdn = node as BibliographicDescriptionNode;
            if (bdn == null)
                throw new Exception("Not Bibliographic DescriptionNode");

            double difference = 0;

            difference += TitleNode.Compare(bdn.TitleNode);

            if (difference < 0.05 || difference > 0.25)
                return difference;

            difference += AutorRangeNode.Compare(bdn.AutorRangeNode);
            difference += PublisherNode.Compare(bdn.PublisherNode);
            difference += CityNode.Compare(bdn.CityNode);
            difference += YearNode.Compare(bdn.YearNode);
            difference += PageCountNode.Compare(bdn.PageCountNode);

            return difference;
        }

        public override string GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
