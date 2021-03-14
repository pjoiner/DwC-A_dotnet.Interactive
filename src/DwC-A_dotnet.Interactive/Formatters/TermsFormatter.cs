extern alias Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using Core.DwC_A.Terms;
using Microsoft.AspNetCore.Html;
using Microsoft.DotNet.Interactive.Formatting;

namespace DwC_A.Interactive.Formatters
{
    public class TermsFormatter
    {
        public static void Register(DefaultTerms defaultTerms, TextWriter writer)
        {
            var dictionary = LoadXmlDocumentation();

            var header = PocketViewTags.tr(new[]
            {
                PocketViewTags.td("Name"),
                PocketViewTags.td("Term"),
                PocketViewTags.td("Description")
            });
            var rows = new List<dynamic>();
            foreach(var field in typeof(Terms).GetFields())
            {
                rows.Add(PocketViewTags.tr(new[]
                {
                    PocketViewTags.td(new HtmlString($"{field.Name}")),
                    PocketViewTags.td(field.GetValue(null)),
                    PocketViewTags.td(dictionary[field.GetValue(null).ToString()])
                }));
            }

            var t = PocketViewTags.table(
                PocketViewTags.thead(header),
                PocketViewTags.tbody(rows));
            writer.Write(t);
        }

        public static IDictionary<string, string> LoadXmlDocumentation()
        {
            var dictionary = new Dictionary<string, string>();
            var assembly = Assembly.GetAssembly(typeof(Terms));
            var fileName = Path.ChangeExtension(assembly.Location, "xml");

            var document = new XmlDocument();
            document.Load(fileName);
            var nodes = document.SelectNodes("//*[contains(@name, 'F:DwC_A.Terms.Terms')]");
            foreach(XmlNode node in nodes)
            {
                var value = node.SelectSingleNode("value").InnerText.Trim();
                var description = node.SelectSingleNode("summary").InnerText.Trim();
                dictionary.Add(value, description);
            }
            return dictionary;
        }
    }
}
