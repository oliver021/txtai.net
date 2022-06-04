using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{

    public struct Document
    {
        public string id;
        public string text;

        /**
         * Creates a document.
         * 
         * @param id document id
         * @param text document text
         */
        public Document(String id, String text)
        {
            this.id = id;
            this.text = text;
        }
    }

    public struct SearchResult
    {
        public string id;
        public double score;

        public SearchResult(string id, double score)
        {
            this.id = id;
            this.score = score;
        }
    }

    /// <summary>
    /// Txtai embeddings class.
    /// </summary>
    public class Embeddings : RequestBase
    {
        public Embeddings(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        /// Finds documents in the embeddings model most similar to the input query. Returns
        /// * a list of {id: value, score: value} sorted by highest score, where id is the
        /// * document id in the embeddings model.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Task<List<SearchResult>> Search(string query, int limit)
           => GetAsync<List<SearchResult>>(Api.EmbeddingsSearch, new() {
               ["query"] = query,
               ["limit"] = limit.ToString()
           });

        /// <summary>
        /// Finds documents in the embeddings model most similar to the input queries. Returns
        /// * a list of {id: value, score: value} sorted by highest score per query, where id is
        /// * the document id in the embeddings model.
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Task<List<List<SearchResult>>> BatchSearch(IEnumerable<string> queries, int limit)
           => PostAsync<List<List<SearchResult>>>(Api.EmbeddingsBatchSearch, new
           {
               queries,
               limit = limit.ToString()
           });

        /// <summary>
        /// Adds a batch of documents for indexing.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public Task Add(IEnumerable<Document> documents)
           => OnlyPostAsync(Api.EmbeddingsAdd, documents);

        /// <summary>
        /// Builds an embeddings index for previously batched documents.
        /// </summary>
        /// <returns></returns>
        public async Task Index()
           =>  await GetAndVoid(Api.EmbeddingsIndex, null);

        /// <summary>
        /// Runs an embeddings upsert operation for previously batched documents.
        /// </summary>
        /// <returns></returns>
        public async Task Upsert()
           => await GetAndVoid(Api.EmbeddingsUpsert, null);

        /// <summary>
        /// Total number of elements in this embeddings index.
        /// </summary>
        /// <returns></returns>
        public async Task<int> Count()
        {
            var response = await GetRawAsync(Api.EmbeddingsUpsert, null);

            try
            {
                return int.Parse(response);
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Transforms text into an embeddings array.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<double[]> Transform(string text)
        => GetAsync<double[]>(Api.EmbeddingsTransform, new() { ["text"] = text });

        /// <summary>
        /// Transforms list of text into embeddings arrays.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<double[]> BatchTransform(IEnumerable<string> texts)
        => PostAsync<double[]>(Api.EmbeddingsBTransform, texts);
    }
}
