using NJsonSchema;

namespace CerbiSharp.Tools.WepApiGenerator.NameGenerators
{
    public class LegacyTypeNameGenerator : ITypeNameGenerator
    {
        private readonly DefaultTypeNameGenerator _defaultTypeNameGenerator;

        public LegacyTypeNameGenerator()
        {
            _defaultTypeNameGenerator = new DefaultTypeNameGenerator();
        }

        public string Generate(JsonSchema schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            return _defaultTypeNameGenerator.Generate(schema, $"Legacy{typeNameHint}", reservedTypeNames);
        }
    }
}