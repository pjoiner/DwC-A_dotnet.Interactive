extern alias Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.DwC_A;
using Core.DwC_A.Terms;

using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace DwC_A.Interactive.Formatters
{
    internal class RowFormatter
    {
        public static void Register(IEnumerable<IRow> rows, TextWriter writer)
        {
            var fieldMetaData = rows.FirstOrDefault()?.FieldMetaData;
            if (fieldMetaData == null)
                return;

            var headers = new List<dynamic>();
            foreach(var metaData in fieldMetaData)
            {
                headers.Add(td(Terms.ShortName(metaData.Term)));
            }
            var header = thead(tr(headers));
            var rowList = new List<dynamic>();
            foreach(var row in rows)
            {
                var cells = new List<dynamic>();
                foreach(var field in row.Fields)
                {
                    cells.Add(td(field));
                }
                rowList.Add(tr(cells));
            }
            var t = table(
                    header,
                    tbody(rowList));
            writer.Write(t);
        }
    }
}
