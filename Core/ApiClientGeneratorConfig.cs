using NSwag.CodeGeneration.CSharp;

namespace CerbiSharp.Tools.WebApiGenerator.Core
{
    public class ApiClientGeneratorConfig
    {
        private const string CS_FORMAT_FILE = "cs";
        private const string DEFAULT_NAME_SPACE_FORMAT = "SwaggerGen.{0}";

        public CSharpClientGeneratorSettings? GeneratorSettings { get; set; }

        public string? FileName { get; set; }

        public string? Url { get; set; }

        public string AddFormatToFileName()
        {
            FileName = FileName + '.' + CS_FORMAT_FILE;

            return FileName;
        }

        public static ApiClientGeneratorConfig CreateDefaultApiClientGeneratorConfigWithDefaultGeneratorSetting(string nameSpace, string fileName)
        {
            return CreateDefaultApiClientGeneratorConfigWithDefaultGeneratorSetting(fileName, config =>
            {
                config.CSharpGeneratorSettings.Namespace = nameSpace;
            });
        }

        public static ApiClientGeneratorConfig CreateDefaultApiClientGeneratorConfigWithDefaultGeneratorSetting(string fileName, Action<CSharpClientGeneratorSettings> configGeneratorSettings)
        {
            var generatorSettings = ApiClientGenerator.GetDefaultClientGeneratorSettings(string.Format(DEFAULT_NAME_SPACE_FORMAT, fileName));

            configGeneratorSettings(generatorSettings);

            return new ApiClientGeneratorConfig
            {
                GeneratorSettings = generatorSettings,
                FileName = fileName,
            };
        }

        public ApiClientGeneratorConfig ConfigGeneratorSettings(Action<CSharpClientGeneratorSettings> configGeneratorSetting)
        {
            if (GeneratorSettings != null)
            {
                configGeneratorSetting(GeneratorSettings);
            }
            else
            {
                throw new NullReferenceException($"{GeneratorSettings} is null!");
            }

            return this;
        }

        public bool IsConfigReadyForGenerate()
        {
            return
                !string.IsNullOrEmpty(FileName) ||
                !string.IsNullOrEmpty(Url) ||
                GeneratorSettings == null;
        }
    }
}
