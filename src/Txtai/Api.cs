using System;
using System.Net.Http;

namespace Txtai
{
    public static class Api
    {
        /// <summary>
        /// Constants of extract endpoint path.
        /// </summary>
        public const string ExtractorEndpoint = "/extract";
        public const string BatchTextract = "/batchtextract";

        /// <summary>
        /// Constants of similarity endpoint path.
        /// </summary>
        public const string SimilarityEndpoint = "/similarity";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string BatchSimilarityEndpoint = "/batchsimilarity";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string WorkflowEndpoint = "/workflow";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string SegmentEndpoint = "/segment";
        public const string BatchSegment = "/batchsegment";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string LabelEndpoint = "/label";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string BatchLabel = "/batchlabel";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string EmbeddingsEndpoint = "/embeddings";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string SummaryEndpoint = "/summary";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string TranslationEndpoint = "/translate";
        public const string BatchTranslation = "/batchtranslate";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string TranscriptionEndpoint = "/transcribe";
        public const string BatchTranscription = "/batchtranscribe";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string TextractEndpoint = "/textract";

        /// <summary>
        /// Constants to access the path string.
        /// </summary>
        public const string EmbeddingsSearch = "/search";
        public const string EmbeddingsBatchSearch = "/batchsearch";
        public const string EmbeddingsAdd = "/add";
        public const string EmbeddingsIndex = "/index";
        public const string EmbeddingsUpsert = "/upsert";
        public const string EmbeddingsDelete = "/delete";
        public const string EmbeddingsCount = "/count";
        public const string EmbeddingsTransform = "/transform";
        public const string EmbeddingsBTransform = "/batchtransform";

        /// <summary>
        /// Build instances from two main depedencies to make requests.
        /// This is to work globally without DI Service, to use an ASP.Net for example,
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Main dependencies for all request.</returns>
        public static (HttpClient Client, IApiSerializer Serializer) ResolveDependecies(string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(uri) };
            var serializer = new JsonApiSerializer();
            return (client, serializer);
        }

        /// <summary>
        /// Build instances from two main depedencies to make requests.
        /// This is to work globally without DI Service, to use an ASP.Net for example,
        /// There is risk with <see cref="HttpClient"/> lifecycle managment.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Main dependencies for all request.</returns>
        public static (HttpClient Client, IApiSerializer Serializer) ResolveDependecies(string uri, Action<HttpClient> configureClient = null)
        {
            var client = new HttpClient { BaseAddress = new Uri(uri) };
            if(configureClient is not null) 
                configureClient(client);
            var serializer = new JsonApiSerializer();
            return (client, serializer);
        }

        /// <summary>
        /// Build a instance of txtai client using uri and optional callback to configure
        /// http client.
        /// There is risk with <see cref="HttpClient"/> lifecycle managment.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Main dependencies for all request.</returns>
        public static TxtaiClient CreateClient(string uri, Action<HttpClient> configureClient = null)
        {
            return new TxtaiClient(uri, configureClient);
        }
    }

    /// <summary>
    /// A class to build request object to use txtai backend.
    /// There is risk with <see cref="HttpClient"/> lifecycle managment.
    /// Care about the memory runtime when use HttpClient created manually.
    /// </summary>
    public class TxtaiClient
    {
        public TxtaiClient(string txtaiUrl)
        {
            (Client, Serializer) = Api.ResolveDependecies(txtaiUrl);
        }

        public TxtaiClient(string txtaiUrl, Action<HttpClient> configure)
        {
            (Client, Serializer) = Api.ResolveDependecies(txtaiUrl, configure);
        }

        public TxtaiClient(HttpClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Serializer = new JsonApiSerializer();
        }

        public TxtaiClient(HttpClient client, IApiSerializer serializer)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public HttpClient Client { get; set; }

        public IApiSerializer Serializer { get; set; }

        public TRequest Create<TRequest>()
            where TRequest : RequestBase
        {
            return (TRequest)Activator.CreateInstance(typeof(TRequest), Client, Serializer);
        }
    }
}
