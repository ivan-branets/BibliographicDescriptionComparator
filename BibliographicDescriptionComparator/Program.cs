using System;
using System.Linq;
using System.Xml.Linq;
using SemanticNetwork.SemanticNetworkNodes;

namespace SemanticNetwork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var root = XElement.Load("Input.xml");

            var biblDescList = root.Elements().Select(b =>
                    new BibliographicDescriptionNode(b.Element("title").Value,
                                                     b.Element("autors").Value.Split(new[] { "; ", ", ", ";", "," }, StringSplitOptions.RemoveEmptyEntries),
                                                     b.Element("publisher").Value, b.Element("city").Value,
                                                     b.Element("year").Value, b.Element("pageCount").Value)).ToList();


            var table = new XElement("table");
            table.Add(new XAttribute("cellpadding", "4px"));

            for (var i = 0; i < biblDescList.Count - 1; i++)
            {
                for (var j = i + 1; j < biblDescList.Count; j++)
                {
                    biblDescList[i].Compare(biblDescList[j], table);
                }
            }

            table.Save("Output.html");
        }
    }
}
