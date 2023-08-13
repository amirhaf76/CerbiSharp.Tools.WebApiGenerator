using NJsonSchema;

namespace CerbiSharp.Tools.WebApiGenerator.Generator.MapperVisitor
{
    internal class PrimitiveTypeMapperDto
    {
        internal JsonObjectType MapToType { get; set; }
        internal JsonObjectType JsonObjectType { get; set; }

        internal string MapToFormat { get; set; } = string.Empty;
        internal string JsonFormat { get; set; } = string.Empty;
    }
}
