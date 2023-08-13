using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;
using System.Text;

namespace CerbiSharp.Tools.WebApiGenerator.Generator.NameGenerators
{
    /// <summary>
    /// For generating api clients from swaggger.
    /// </summary>
    /// Nswag usful information:
    ///     https://github.com/RicoSuter/NSwag/wiki/CSharpClientGenerator
    ///     https://github.com/RicoSuter/NSwag/wiki/CSharpClientGeneratorSettings
    ///     https://github.com/RicoSuter/NSwag/wiki/ClientGeneratorBaseSettings
    public class ApiOperationNameGenerator : IOperationNameGenerator
    {
        public bool SupportsMultipleClients => true;

        private readonly List<string> _preventedPartNames;

        public ApiOperationNameGenerator(IList<string> preventedPartNames)
        {
            _preventedPartNames = new List<string>(preventedPartNames);
        }

        public ApiOperationNameGenerator() : this(new List<string> { })
        {

        }

        public string GetClientName(OpenApiDocument document, string path, string httpMethod, OpenApiOperation operation)
        {
            return operation.Tags[0];
        }

        public string GetOperationName(OpenApiDocument document, string path, string httpMethod, OpenApiOperation operation)
        {
            var pathParts = new List<string>(path.Split('/')).
                Except(_preventedPartNames).
                Where(part => !part.Contains('{'));

            var stringBuilder = new StringBuilder();

            AddCammelCaseName(httpMethod, stringBuilder);

            foreach (var pathPart in pathParts)
            {
                AddCammelCaseName(pathPart, stringBuilder);
            }

            return stringBuilder.ToString();
        }

        public void AddCammelCaseName(string name, StringBuilder stringBuilder)
        {
            stringBuilder.Append(char.ToUpper(name[0]));

            if (name.Length > 1)
            {
                stringBuilder.Append(name[1..]);
            }
        }
    }
}
