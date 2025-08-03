using CerbiSharp.Tools.WebApiGenerator.Generator;
using CerbiSharp.Tools.WebApiGenerator.Generator.NameGenerators;
using NSwag.CodeGeneration.CSharp;

namespace CerbiSharp.Tools.WebApiGenerator.Extensions
{
    public static class ApiClientGeneratorConfigExtension
    {
        private static readonly string s_directoryResult = @"ClientGeneratorResult";

        public static ApiClientGeneratorConfig SetDefaultApiClientGeneratorSettings(this ApiClientGeneratorConfig config)
        {
            var settings = config.GeneratorSettings ?? new CSharpClientGeneratorSettings();

            settings.ClassName = "{controller}Client";

            settings.CSharpGeneratorSettings.GenerateDefaultValues = true;
            settings.CSharpGeneratorSettings.GenerateDataAnnotations = true;
            settings.CSharpGeneratorSettings.InlineNamedTuples = true;
            settings.CSharpGeneratorSettings.RequiredPropertiesMustBeDefined = true;

            settings.OperationNameGenerator = new ApiOperationNameGenerator();

            settings.ExceptionClass = "ApiException";

            settings.GenerateClientClasses = true;
            settings.GenerateClientInterfaces = true;

            settings.UseBaseUrl = true;
            settings.GenerateBaseUrlProperty = false;

            settings.GenerateResponseClasses = true;
            settings.WrapResponses = true;
            settings.ResponseClass = "SwaggerResponse";

            settings.GenerateDtoTypes = true;
            settings.WrapDtoExceptions = true;

            settings.GenerateUpdateJsonSerializerSettingsMethod = true;

            settings.ClientBaseClass = "BaseClient";

            settings.ConfigurationClass = "IClientConfig";
            settings.InjectHttpClient = false;
            settings.UseHttpClientCreationMethod = true;

            return config;
        }

        public static ApiClientGeneratorConfig ConfigApiClientGeneratorConfig(this ApiClientGeneratorConfig config, Action<ApiClientGeneratorConfig> configApiGeneratorConfig)
        {
            configApiGeneratorConfig(config);

            return config;
        }

        public static async Task<string> GenerateClientFileAndSaveAsync(this ApiClientGeneratorConfig config)
        {
            var filePath = Path.Combine(GetDirectory(), config.Output!);

            await new ApiClientGenerator(config).GenerateCodeAndSaveInFileAsync(filePath);

            return filePath;
        }

        private static string GetDirectory()
        {
            var path = Path.Combine(Environment.CurrentDirectory, s_directoryResult);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
