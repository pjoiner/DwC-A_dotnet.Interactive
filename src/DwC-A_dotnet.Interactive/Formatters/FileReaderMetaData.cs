extern alias Core;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using System.IO;
using Core.DwC_A;
using Core.DwC_A.Terms;

using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace DwC_A.Interactive.Formatters
{
    internal class FileReaderMetaData
    {
        public static void Register(IFileReader fileReader, TextWriter writer)
        {
            writer.Write(new HtmlString($"<b>FileName</b>: {Path.GetFileName(fileReader.FileName)}<br/>"));
            writer.Write(new HtmlString($"<b>RowType</b>: {fileReader.FileMetaData.RowType}"));
            var header = tr(new[]{
                td("Index"),
                td("Name"),
                td("Term"),
                td("Vocabulary"),
                td("Default"),
                td("Delimiter")
            });
            var rows = new List<dynamic>();
            foreach (var field in fileReader.FileMetaData.Fields)
            {
                var name = span(Terms.ShortName(field.Term));
                if(field.IndexSpecified && fileReader.FileMetaData.Id.Index == field.Index)
                {
                    name = span(name, sup(b("*")));
                }
                var row = tr(new[]
                {
                    td(field.IndexSpecified ? field.Index.ToString() : ""),
                    td(name),
                    td(a[href:field.Term](field.Term)),
                    td(field.Vocabulary),
                    td(field.Default),
                    td(field.DelimitedBy)
                });
                rows.Add(row);
            }
            var t = table(
                thead(header),
                tbody(rows),
                tfoot(tr(td(span(sup(b("*")), "Indicates index column"))))
            );
            writer.Write(t);
        }
    }
}
