using DwC_A;
using DwC_A.Terms;
using System.Collections.Generic;
using System.Dynamic;

namespace DwC_A_dotnet.Interactive.Extensions
{
    public static class RowExtensions
    {
        public static dynamic ToDynamic(this IRow row)
        {
            dynamic dynamicObj = new ExpandoObject();

            var underlyingDictionary = dynamicObj as IDictionary<string, object>;
            foreach(var field in row.FieldMetaData)
            {
                underlyingDictionary.Add(Terms.ShortName(field.Term), row[field.Term]);
            }

            return dynamicObj;
        }
    }
}
