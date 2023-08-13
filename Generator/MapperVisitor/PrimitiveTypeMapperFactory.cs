using NJsonSchema;

namespace CerbiSharp.Tools.WebApiGenerator.Generator.MapperVisitor
{
    internal class PrimitiveTypeMapperFactory
    {
        internal static PrimitiveTypeMapperDto CreateDecimalTypeMapperDto()
        {
            return new PrimitiveTypeMapperDto
            {
                JsonObjectType = JsonObjectType.Number,
                JsonFormat = PrimitiveTypeJsonFormat.DECIMAL,
                MapToType = JsonObjectType.Number,
                MapToFormat = PrimitiveTypeMapperFormat.DECIMAL,
            };
        }
        internal static PrimitiveTypeMapperDto CreateLongTypeMapperDto()
        {
            return new PrimitiveTypeMapperDto
            {
                JsonFormat = PrimitiveTypeJsonFormat.LONG,
                MapToFormat = PrimitiveTypeMapperFormat.INT64,
                JsonObjectType = JsonObjectType.Number,
                MapToType = JsonObjectType.Integer,
            };
        }
        internal static PrimitiveTypeMapperDto CreateIntTypeMapperDto()
        {
            return new PrimitiveTypeMapperDto
            {
                JsonObjectType = JsonObjectType.Number,
                JsonFormat = PrimitiveTypeJsonFormat.INT,
                MapToFormat = PrimitiveTypeMapperFormat.INT32,
                MapToType = JsonObjectType.Integer,
            };
        }
    }
}
