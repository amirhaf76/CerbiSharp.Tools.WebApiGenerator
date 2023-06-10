using CerbiSharp.Tools.WebApiGenerator.Core;
using Microsoft.Extensions.Configuration;


var paths = new List<string>();


paths.Add(await CreateSampleSwaggerClientAsync());

paths.ForEach(e => Console.WriteLine(e));


static async Task<string> CreateSampleSwaggerClientAsync()
{
    const string NAME_SPACE = "CerbiSharp.Tools.WepApiGenerator.SampleSwagger";
    const string FILE_NAME = "SampleSwaggerClient";

    var swaggerJsonUrl = (GetAppSettings()?.GetSection("Urls:SampleSwagger").Get<string>()) ?? string.Empty;

    return await ApiClientGeneratorConfig
        .CreateDefaultApiClientGeneratorConfigWithDefaultGeneratorSetting(NAME_SPACE, FILE_NAME)
        .ConfigGeneratorSettings(setting =>
        {
            // add your configuration.
            setting.ClientClassAccessModifier = "internal";
            setting.ClientBaseClass = "SampleSwaggerBaseClient";

            // An interface is used here for dependency injection.
            setting.ConfigurationClass = "SampleSwaggerConfig";
            setting.UseHttpClientCreationMethod = true;

            // Setting name for response class.Default: SwaggerResponse
            // setting.ResponseClass = "SwaggerResponseLegacy";

            // Setting name generator.Default: ApiOperationNameGenerator.
            // setting.CSharpGeneratorSettings.TypeNameGenerator = new LegacyTypeNameGenerator();
        })
        .SetAddressUrl(swaggerJsonUrl)
        .GenerateClientFileAndSaveAsync();
}

static IConfiguration GetAppSettings()
{
    return new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("appsettings.json")
        .Build();
}

