extern alias Core;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Core.DwC_A.Terms;
using Microsoft.AspNetCore.Html;
using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace DwC_A.Interactive.Formatters
{
    public class TermsFormatter
    {
        public static void Register(DefaultTerms defaultTerms, TextWriter writer)
        {
            var dictionary = LoadXmlDocumentation();

            var header = tr(new[]
            {
                td("Name"),
                td("Term"),
                td("Description")
            });
            var rows = new List<dynamic>();
            foreach(var field in typeof(Terms).GetFields())
            {
                var value = field.GetValue(null).ToString();
                var description = dictionary.ContainsKey(value) ? dictionary[value] : "";
                rows.Add(tr(new[]
                {
                    td(field.Name),
                    td(new HtmlString($"<a href=\"{value}\">{value}</a>")),
                    td(description)
                }));
            }

            var t = table(
                thead(header),
                tbody(rows));
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
