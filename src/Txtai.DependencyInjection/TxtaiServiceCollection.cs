using System;
using System.Text;
using System.Net.Http;
using Txtai;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// The TxtAI API.
    /// Contains main configuration for the TxtAI API using Dependency Injection Service Collection.
    /// This is the most suitable way to configure the TxtAI API to work for example in ASP.NET Core.
    /// <example>
    /// <code>
    /// public void ConfigureServices(IServiceCollection services)
    /// {
    ///     services.AddTxtai(base: "http://localhost:5000");
    /// }
    /// </summary>
    public static class TxtaiServiceCollection
    {
        private const string TxtaiHttpClient = "txtai";

        /// <summary>
        /// Add TxtAI API to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="base">The base url of the TxtAI API.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddTxtai(this IServiceCollection services, string @base = "http://localhost:5000")
        {
            // add the base url
            services.AddHttpClient(TxtaiHttpClient, c =>
            {
                c.BaseAddress = new Uri(@base);
            });

            // add the serializer
            services.AddSingleton<IApiSerializer, JsonApiSerializer>();

            // registers
            Add<Workflow>(services);
            Add<Labels>(services);
            Add<Transcription>(services);
            Add<Translation>(services);
            Add<Segmentation>(services);
            Add<Extractor>(services);
            Add<TextExtractor>(services);
            Add<Similarity>(services);
            Add<Embeddings>(services);

            return services;
        }

        private static void Add<T>(IServiceCollection services)
        {
            services.AddSingleton(typeof(T), x => RegisterRequest<T>(x));
        }

        private static object RegisterRequest<T>(IServiceProvider provider)
        {
            var factory = provider.GetService<IHttpClientFactory>();
            var jsonApi = provider.GetService<IApiSerializer>();
            return Activator.CreateInstance(typeof(T), factory?.CreateClient(TxtaiHttpClient), jsonApi);
        }
    }
}
