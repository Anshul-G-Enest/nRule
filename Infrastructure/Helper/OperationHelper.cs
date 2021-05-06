using Rule.WebAPI.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rule.WebAPI.Infrastructure.Helper
{
    public class OperationHelper
    {
        readonly Dictionary<TypeGroup, HashSet<Type>> TypeGroups;

        public OperationHelper()
        {
            TypeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(Single), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>) } }
            };
        }

        public List<FilterOperation> SupportedOperations(Type type)
        {
            var supportedOperations = ExtractSupportedOperationsFromAttribute(type);

            if (type.IsArray)
            {
                //The 'In' operation is supported by all types, as long as it's an array...
                supportedOperations.Add(FilterOperation.In);
            }

            var underlyingNullableType = Nullable.GetUnderlyingType(type);
            if (underlyingNullableType != null)
            {
                var underlyingNullableTypeOperations = SupportedOperations(underlyingNullableType);
                supportedOperations.AddRange(underlyingNullableTypeOperations);
            }

            return supportedOperations;
        }


        private List<FilterOperation> ExtractSupportedOperationsFromAttribute(Type type)
        {
            var typeName = type.Name;
            if (type.IsArray)
            {
                typeName = type.GetElementType().Name;
            }

            var typeGroup = TypeGroups.FirstOrDefault(i => i.Value.Any(v => v.Name == typeName)).Key;
            var fieldInfo = typeGroup.GetType().GetField(typeGroup.ToString());
            var attrs = fieldInfo.GetCustomAttributes(false);
            var attr = attrs.FirstOrDefault(a => a is SupportedOperationsAttribute);
            return (attr as SupportedOperationsAttribute).SupportedOperations;
        }
    }
}
