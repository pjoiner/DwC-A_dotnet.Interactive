extern alias Core;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.DotNet.Interactive.Formatting;
using System.IO;
using Core.DwC_A.Meta;

namespace DwC_A.Interactive.Formatters
{
    public class ArchiveMetaData
    {
        public static void Register(Archive archive, TextWriter writer)
        {
            var header = PocketViewTags.tr(new[]
            {
                PocketViewTags.td("File Type"),
                PocketViewTags.td("File Name"),
                PocketViewTags.td("Row Type")
            });
            var rows = new List<dynamic>();
            rows.Add(PocketViewTags.tr(new[]
            {
                PocketViewTags.td(new HtmlString("<b>CoreFile:</b>")),
                PocketViewTags.td(Path.GetFileName(archive.Core.Files.FirstOrDefault())),
                PocketViewTags.td(archive.Core.RowType)
            }));
            rows.AddRange(archive.Extension
                    .Select(e =>
                        PocketViewTags.tr(new[]
                            {
                PocketViewTags.td(new HtmlString("<b>Extension:</b>")),
                PocketViewTags.td(Path.GetFileName(e.Files.FirstOrDefault())),
                PocketViewTags.td(e.RowType)
                            })));
            var t = PocketViewTags.table(
                PocketViewTags.thead(header),
                PocketViewTags.tbody(rows));
            writer.Write(t);
        }
    }
}
