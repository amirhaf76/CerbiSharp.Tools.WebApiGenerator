using CerbiSharp.Tools.WebApiGenerator.Extensions;
using CerbiSharp.Tools.WebApiGenerator.Generator;
using Microsoft.Extensions.Configuration;


var paths = new List<string>
{
    await CreateSampleSwaggerClientAsync()
};

paths.ForEach(e => Console.WriteLine(e));


static async Task<string> CreateSampleSwaggerClientAsync()
{
    const string NAME_SPACE = "CerbiSharp.Tools.WebApiGenerator.SampleSwagger";
    const string FILE_NAME = "SampleSwaggerClient.cs";

    var swaggerJsonUrl = (GetAppSettings()?.GetSection("URLs:SampleSwagger").Get<string>()) ?? string.Empty;

    return await new ApiClientGeneratorConfig()
        .SetDefaultApiClientGeneratorSettings()
        .ConfigApiClientGeneratorConfig(config =>
        {
            config.UrlAddress = swaggerJsonUrl;
            config.Output = FILE_NAME;

            var settings = config.GeneratorSettings;

            settings.CSharpGeneratorSettings.Namespace = NAME_SPACE;

            //      Add your configuration.
            settings.ClientClassAccessModifier = "internal";
            settings.ClientBaseClass = "SampleSwaggerBaseClient";

            //      An interface is used here for dependency injection.
            settings.ConfigurationClass = "SampleSwaggerConfig";
            settings.UseHttpClientCreationMethod = true;

            //      Setting name for response class.Default: SwaggerResponse.
            // setting.ResponseClass = "SwaggerResponseLegacy";

            //      Setting name generator.Default: ApiOperationNameGenerator.
            // setting.CSharpGeneratorSettings.TypeNameGenerator = new LegacyTypeNameGenerator();

            //      Injecting Http Client
            settings.InjectHttpClient = true;

            //      Removing config and Base class
            settings.UseBaseUrl = false;
            settings.ClientBaseClass = string.Empty;
            settings.ConfigurationClass = string.Empty;
        })
        .GenerateClientFileAndSaveAsync();
}

static IConfiguration GetAppSettings()
{
    return new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("appsettings.json")
        .Build();
}

