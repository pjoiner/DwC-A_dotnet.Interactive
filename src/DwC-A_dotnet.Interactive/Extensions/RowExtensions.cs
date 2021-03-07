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
            var usedFieldNames = new List<string>();
            dynamic dynamicObj = new ExpandoObject();

            var underlyingDictionary = dynamicObj as IDictionary<string, object>;
            foreach (var field in row.FieldMetaData)
            {
                var fieldName = Terms.ShortName(field.Term);
                int i = 1;
                while(usedFieldNames.Contains(fieldName))
                {
                    fieldName += i.ToString();
                }
                underlyingDictionary.Add(fieldName, row[field.Term]);
                usedFieldNames.Add(fieldName);
            }

            return dynamicObj;
        }
    }
}
