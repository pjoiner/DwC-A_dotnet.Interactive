using DwC_A.Config;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace DwC_A.Interactive.Formatters
{
    internal class GeneratorConfigFormatter
    {
        public static void Register(IGeneratorConfiguration config, TextWriter writer)
        {
            var mainHeader = thead(tr(
                    th("Option"),
                    th("Value")
                ));
            var mainBody = MainRows(config);
            var mainTable = table(
                    mainHeader,
                    mainBody
                );

            var propertyHeader = thead(tr(
                    th("Name"),
                    th("Type"),
                    th("Include"),
                    th("Term")
                ));
            var propertyRows = new List<dynamic>();
            foreach(var property in config.Properties)
            {
                propertyRows.Add(tr(new[]
                    {
                        td(property.Value.PropertyName),
                        td(property.Value.TypeName),
                        td(property.Value.Include),
                        td(property.Key)
                    }));
            }
            var propertyBody = tbody(propertyRows);
            var propertyTable = table(
                    propertyHeader,
                    propertyBody
                );

            writer.Write( 
                div(
                    div(mainTable), 
                    div(h3("Properties")),
                    div(propertyTable)
                    )
                );
        }

        private static dynamic MainRows(IGeneratorConfiguration config)
        {
            return tbody(new[]
            {
                MainRow(nameof(config.Namespace), config.Namespace),
                MainRow(nameof(config.MapMethod), config.MapMethod),
                MainRow(nameof(config.Output), config.Output),
                MainRow(nameof(config.PascalCase), config.PascalCase),
                MainRow(nameof(config.TermAttribute), config.TermAttribute),
                AddUsings(config)
            });
        }

        private static dynamic MainRow(string configName, object configValue)
        {
            return tr(
                td(configName),
                td(configValue.ToString())
            );
        }

        private static dynamic AddUsings(IGeneratorConfiguration config)
        {
            return tr(
                td("Usings"),
                td(config.Usings.Select(u => ul(u)))
                );
        }
    }
}
