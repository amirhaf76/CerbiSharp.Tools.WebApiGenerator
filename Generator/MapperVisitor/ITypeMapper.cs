using NJsonSchema;

namespace CerbiSharp.Tools.WebApiGenerator.Generator.MapperVisitor
{
    internal interface ITypeMapper
    {
        void MapType<T>(JsonSchema schema, T data);
    }
}
