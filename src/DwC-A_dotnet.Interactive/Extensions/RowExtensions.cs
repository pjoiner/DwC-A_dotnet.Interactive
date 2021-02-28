extern alias Core;
using System.Collections.Generic;
using System.Dynamic;
using Core.DwC_A;
using Core.DwC_A.Terms;

namespace DwC_A.Interactive.Extensions
{
    public static class RowExtensions
    {
        public static dynamic ToDynamic(this IRow row)
        {
            dynamic dynamicObj = new ExpandoObject();

            var underlyingDictionary = dynamicObj as IDictionary<string, object>;
            foreach (var field in row.FieldMetaData)
            {
                underlyingDictionary.Add(Terms.ShortName(field.Term), row[field.Term]);
            }

            return dynamicObj;
        }
    }
}
