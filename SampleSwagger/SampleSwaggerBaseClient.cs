namespace CerbiSharp.Tools.WepApiGenerator.SampleSwagger
{
    internal class SampleSwaggerBaseClient
    {
        private readonly SampleSwaggerConfig _sampleSwaggerBaseConfig;

        public SampleSwaggerBaseClient(SampleSwaggerConfig configuration)
        {
            _sampleSwaggerBaseConfig = configuration;

            BaseUrl = _sampleSwaggerBaseConfig.Url;
        }

        public string? BaseUrl { get; private set; }

        protected async Task<HttpClient> CreateHttpClientAsync(CancellationToken ct = default)
        {
            return await Task.FromResult(new HttpClient());
        }
    }
}
