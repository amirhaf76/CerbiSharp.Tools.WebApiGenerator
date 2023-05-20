using CerbiSharp.Tools.WepApiGenerator.NameGenerators;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace CerbiSharp.Tools.WepApiGenerator.Core
{
    public class ApiClientGenerator
    {
        private readonly CSharpClientGeneratorSettings _generatorSettings;

        public ApiClientGenerator(string nameSpace) :
            this(GetDefaultClientGeneratorSettings(nameSpace))
        {
        }

        public ApiClientGenerator(CSharpClientGeneratorSettings clientGeneratorSettings)
        {
            _generatorSettings = clientGeneratorSettings;
        }


        public static CSharpClientGeneratorSettings GetDefaultClientGeneratorSettings(string nameSpace)
        {
            return new CSharpClientGeneratorSettings
            {
                ClassName = "{controller}Client",

                CSharpGeneratorSettings =
                {
                    Namespace = nameSpace,

                    GenerateDefaultValues=true,
                    GenerateDataAnnotations =true,
                    InlineNamedTuples=true,
                    RequiredPropertiesMustBeDefined = true,
                },

                ExceptionClass = "ApiException",

                GenerateClientClasses = true,
                GenerateClientInterfaces = true,

                UseBaseUrl = true,
                GenerateBaseUrlProperty = false,

                GenerateResponseClasses = true,
                WrapResponses = true,
                ResponseClass = "SwaggerResponse",

                GenerateDtoTypes = true,
                WrapDtoExceptions = true,

                GenerateUpdateJsonSerializerSettingsMethod = true,

                ClientBaseClass = "BaseClient",

                ConfigurationClass = "IClientConfig",
                InjectHttpClient = false,
                UseHttpClientCreationMethod = true,
            };
        }

        public async Task GenerateCodeAndSaveInFileAsync(string path, string apiJsonAddress)
        {
            // Giving a strategy for generating names.
            _generatorSettings.OperationNameGenerator = new ApiOperationNameGenerator();


            // Getting swagger json.
            var receivedOpenApiDocument = await GetOpenApiDocument(new Uri(apiJsonAddress));


            // Generating code.
            var generator = new CSharpClientGenerator(receivedOpenApiDocument, _generatorSettings);

            string generatedCode = generator.GenerateFile();


            // Saving file.
            SaveCodeInFile(path, generatedCode);
        }

        private static async Task<OpenApiDocument> GetOpenApiDocument(Uri uri)
        {
            using var client = new HttpClient();

            var res = await client.GetAsync(uri);

            var document = await OpenApiDocument.FromJsonAsync(await res.Content.ReadAsStringAsync());

            return document;
        }

        private static void SaveCodeInFile(string fileName, string generatedCode)
        {
            using var clientCSharpFile = File.CreateText(fileName);

            clientCSharpFile.Write(generatedCode);
        }
    }
}
