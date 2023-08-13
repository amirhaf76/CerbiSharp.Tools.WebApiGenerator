using CerbiSharp.Tools.WebApiGenerator.Exceptions;
using CerbiSharp.Tools.WebApiGenerator.Generator.MapperVisitor;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace CerbiSharp.Tools.WebApiGenerator.Generator
{
    public class ApiClientGenerator
    {
        private readonly ApiClientGeneratorConfig _apiClientGeneratorConfig;


        public ApiClientGenerator(ApiClientGeneratorConfig apiClientGeneratorConfig)
        {
            _apiClientGeneratorConfig = apiClientGeneratorConfig;
        }


        public async Task GenerateCodeAndSaveInFileAsync(string path)
        {
            if (!_apiClientGeneratorConfig.IsConfigReadyForGenerate())
            {
                throw new InvalidApiClientGeneratorConfigException($"{nameof(ApiClientGeneratorConfig)} is not ready for generate!");
            }

            // Getting swagger json.
            var receivedOpenApiDocument = await GetOpenApiDocument(new Uri(_apiClientGeneratorConfig.UrlAddress));


            // Generating code.
            var generator = new CSharpClientGenerator(receivedOpenApiDocument, _apiClientGeneratorConfig.GeneratorSettings);

            string generatedCode = generator.GenerateFile();

            // Saving file.
            SaveCodeInFile(path, generatedCode);
        }


        private static async Task<OpenApiDocument> GetOpenApiDocument(Uri uri)
        {
            using var client = new HttpClient();

            var res = await client.GetAsync(uri);

            var document = await OpenApiDocument.FromJsonAsync(await res.Content.ReadAsStringAsync());

            var visitor = new CustomizedTypeMapperVisitor<PrimitiveTypeMapperDto>(new PrimitiveTypeMapper(), new[]
            {
                PrimitiveTypeMapperFactory.CreateDecimalTypeMapperDto(),
                PrimitiveTypeMapperFactory.CreateIntTypeMapperDto(),
                PrimitiveTypeMapperFactory.CreateLongTypeMapperDto(),
            });

            visitor.Visit(document);

            return document;
        }

        private static void SaveCodeInFile(string fileName, string generatedCode)
        {
            using var clientCSharpFile = File.CreateText(fileName);

            clientCSharpFile.Write(generatedCode);
        }
    }
}
