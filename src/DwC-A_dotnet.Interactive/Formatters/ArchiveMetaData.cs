extern alias Core;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Core.DwC_A.Meta;
using Core.DwC_A;

using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace DwC_A.Interactive.Formatters
{
    internal class ArchiveMetaData
    {
        public static void RegisterForArchiveReader(ArchiveReader archiveReader, TextWriter writer)
        {
            Register(archiveReader.MetaData, writer);
        }

        public static void Register(Archive archive, TextWriter writer)
        {
            var header = tr(new[]
            {
                td("File Type"),
                td("File Name"),
                td("Row Type")
            });
            var rows = new List<dynamic>();
            rows.Add(tr(new[]
            {
                td(b("CoreFile")),
                td(Path.GetFileName(archive.Core.Files.FirstOrDefault())),
                td(archive.Core.RowType)
            }));
            rows.AddRange(archive.Extension
                    .Select(e =>
                        tr(new[]
                        {
                            td(b("Extension:")),
                            td(Path.GetFileName(e.Files.FirstOrDefault())),
                            td(e.RowType)
                        })));
            rows.Add(tr(new[]
            {
                td(b("Metadata:")),
                td(archive.Metadata),
                td("")
            }));
            var t = table(
                        thead(header),
                        tbody(rows));
            writer.Write(t);
        }
    }
}
