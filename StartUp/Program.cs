using CerbiSharp.Tools.WebApiGenerator.Extensions;
using CerbiSharp.Tools.WebApiGenerator.Generator;
using Microsoft.Extensions.Configuration;

var paths = new List<string>();

paths.Add(await CreateSampleSwaggerClientAsync());

paths.ForEach(e => Console.WriteLine(e));


static async Task<string> CreateSampleSwaggerClientAsync()
{
    const string NAME_SPACE = "CerbiSharp.Tools.WepApiGenerator.SampleSwagger";
    const string FILE_NAME = "SampleSwaggerClient.cs";

    var swaggerJsonUrl = (GetAppSettings()?.GetSection("Urls:SampleSwagger").Get<string>()) ?? string.Empty;

    return await new ApiClientGeneratorConfig()
        .SetDefaultApiClientGeneratorSettings()
        .ConfigApiClientGeneratorConfig(config =>
        {
            config.UrlAddress = swaggerJsonUrl;
            config.Output = FILE_NAME;

            var settings = config.GeneratorSettings;

            settings.CSharpGeneratorSettings.Namespace = NAME_SPACE;

            // add your configuration.
            settings.ClientClassAccessModifier = "internal";
            settings.ClientBaseClass = "SampleSwaggerBaseClient";

            // An interface is used here for dependency injection.
            settings.ConfigurationClass = "SampleSwaggerConfig";
            settings.UseHttpClientCreationMethod = true;

            // Setting name for response class.Default: SwaggerResponse.
            // setting.ResponseClass = "SwaggerResponseLegacy";

            // Setting name generator.Default: ApiOperationNameGenerator.
            // setting.CSharpGeneratorSettings.TypeNameGenerator = new LegacyTypeNameGenerator();
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

