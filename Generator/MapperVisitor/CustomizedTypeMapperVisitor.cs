using NJsonSchema;
using NJsonSchema.Visitors;

namespace CerbiSharp.Tools.WebApiGenerator.Generator.MapperVisitor
{
    internal class CustomizedTypeMapperVisitor<T> : JsonSchemaVisitorBase
    {
        private readonly IEnumerable<T> _typeMapperDtos;
        private readonly ITypeMapper _typeMapper;

        public CustomizedTypeMapperVisitor(ITypeMapper typeMapper, IEnumerable<T> typeMapperDtos)
        {
            _typeMapperDtos = typeMapperDtos;
            _typeMapper = typeMapper;
        }

        protected override JsonSchema VisitSchema(JsonSchema schema, string path, string typeNameHint)
        {
            foreach (var mapperDto in _typeMapperDtos)
            {
                _typeMapper.MapType(schema, mapperDto);
            }
            return schema;
        }
    }
}
