using DwC_A;
using System.Collections.Generic;

namespace DwC_A_dotnet.Interactive.Extensions
{
    public static class FileReaderExtensions
    {
        public static IEnumerable<dynamic> ToDynamic(this IFileReader fileReader)
        {
            foreach(var row in fileReader.DataRows)
            {
                yield return row.ToDynamic();
            }
        }
    }
}
