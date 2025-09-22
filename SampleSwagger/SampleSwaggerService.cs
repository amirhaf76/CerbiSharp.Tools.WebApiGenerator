using Microsoft.Extensions.Configuration;

namespace CerbiSharp.Tools.WebApiGenerator.SampleSwagger
{
    public class SampleSwaggerService
    {
        public async Task<Pet> GetPet()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var apiConfig = new SampleSwaggerConfig
            {
                Url = (config?.GetSection("URLs:SampleSwagger").Get<string>()) ?? string.Empty,
            };

            var petClient = new PetClient(apiConfig) as IPetClient;

            var pet = await petClient.GetPetAsync(1);

            return pet.Result;
        }
    }
}
