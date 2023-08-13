using CerbiSharp.Tools.WebApiGenerator.Exceptions;
using NJsonSchema;

namespace CerbiSharp.Tools.WebApiGenerator.Generator.MapperVisitor
{
    internal class PrimitiveTypeMapper : ITypeMapper
    {
        public void MapType<T>(JsonSchema schema, T data)
        {
            if (data is PrimitiveTypeMapperDto mapper)
            {
                if (schema.Type == mapper.JsonObjectType && schema.Format.Equals(mapper.JsonFormat, StringComparison.CurrentCultureIgnoreCase))
                {
                    schema.Type = mapper.MapToType;
                    schema.Format = mapper.MapToFormat;
                }
            }
            else
            {
                throw new InvalidTypeDataTypeException($"{typeof(T).Name}");
            }
        }
    }
}
