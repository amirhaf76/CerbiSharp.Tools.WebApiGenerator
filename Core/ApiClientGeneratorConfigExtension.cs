namespace CerbiSharp.Tools.WepApiGenerator.Core
{
    public static class ApiClientGeneratorConfigExtension
    {
        private static readonly string s_directoryResult = @"ClientGeneratorResult";

        public static async Task<string> GenerateClientFileAndSaveAsync(this ApiClientGeneratorConfig apiClientGeneratorConfig)
        {
            if (!apiClientGeneratorConfig.IsConfigReadyForGenerate())
            {
                throw new ArgumentException($"{nameof(apiClientGeneratorConfig)} is not ready for generate");
            }

            apiClientGeneratorConfig.AddFormatToFileName();

            var filePath = Path.Combine(GetDirectory(), apiClientGeneratorConfig.FileName!);

            await new ApiClientGenerator(apiClientGeneratorConfig.GeneratorSettings!).
                GenerateCodeAndSaveInFileAsync(filePath, apiClientGeneratorConfig.Url!);

            return filePath;
        }

        public static ApiClientGeneratorConfig SetAddressUrl(this ApiClientGeneratorConfig config, string url)
        {
            config.Url = url;

            return config;
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
