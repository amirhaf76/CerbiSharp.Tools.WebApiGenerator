using NSwag.CodeGeneration.CSharp;

namespace CerbiSharp.Tools.WebApiGenerator.Generator
{
    public class ApiClientGeneratorConfig
    {
        public ApiClientGeneratorConfig()
        {
            GeneratorSettings = new CSharpClientGeneratorSettings();
            Output = string.Empty;
            UrlAddress = string.Empty;
        }

        public CSharpClientGeneratorSettings GeneratorSettings { get; set; }

        public string Output { get; set; } = string.Empty;

        public string UrlAddress { get; set; } = string.Empty;

        public bool IsConfigReadyForGenerate()
        {
            return
                !string.IsNullOrEmpty(Output) ||
                !string.IsNullOrEmpty(UrlAddress) ||
                GeneratorSettings == null;
        }
    }
}
