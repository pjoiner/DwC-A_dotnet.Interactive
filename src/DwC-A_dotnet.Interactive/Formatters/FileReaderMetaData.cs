using DwC_A;
using DwC_A.Terms;
using Microsoft.AspNetCore.Html;
using Microsoft.DotNet.Interactive.Formatting;
using System.Collections.Generic;
using System.IO;

namespace DwC_A_dotnet.Interactive.Formatters
{
    public class FileReaderMetaData
    {
        public static void Register(IFileReader fileReader, TextWriter writer)
        {
            writer.Write(new HtmlString($"<b>FileName</b>: {Path.GetFileName(fileReader.FileName)}<br/>"));
            writer.Write(new HtmlString($"<b>RowType</b>: {fileReader.FileMetaData.RowType}"));
            var header = PocketViewTags.tr(new[]{
                PocketViewTags.td("Index"),
                PocketViewTags.td("Name"),
                PocketViewTags.td("Term"),
                PocketViewTags.td("Vocabulary")
            });
            var rows = new List<dynamic>();
            foreach (var field in fileReader.FileMetaData.Fields)
            {
                var row = PocketViewTags.tr(new[]
                    {
            PocketViewTags.td(field.Index),
            PocketViewTags.td(Terms.ShortName(field.Term)),
            PocketViewTags.td(field.Term),
            PocketViewTags.td(field.Vocabulary)
            });
                rows.Add(row);
            }
            var t = PocketViewTags.table(
                PocketViewTags.thead(header),
                PocketViewTags.tbody(rows)
            );
            writer.Write(t);
        }
    }
}
