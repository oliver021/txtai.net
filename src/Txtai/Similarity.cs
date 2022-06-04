using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Txtai
{
    /// <summary>
    /// The similarity class to make request to the TxtAI API.
    /// </summary>
    public class Similarity : RequestBase
    {
        public Similarity(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {}

        /// <summary>
        /// Computes the similarity between query and list of text. Returns a list of
        /// {id: value, score: value} sorted by highest score, where id is the index
        /// in texts.
        /// </summary>
        /// <param name="query">The text to be compared.</param>
        /// <param name="texts">list of text.</param>
        /// <returns>list of <see cref="IndexResult"/></returns>
        public async Task<List<IndexResult>> SimilarityRequest(string query, List<string> texts) {
            // convert to json the query and texts
            var body = new {
                query,
                texts
            };

            // serialize the json
            var json = this.serializer.Serialize(body);

            // post the json to the TxtAI API
            var response = await this.client.PostAsync("/similarity", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            // deserialize the json
            return this.serializer.Deserialize<List<IndexResult>>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Computes the similarity between list of queries and list of text. Returns a list
        /// * of {id: value, score: value} sorted by highest score per query, where id is the
        /// * index in texts.
        /// </summary>
        /// <param name="queries">list of text.</param>
        /// <param name="texts">list of text.</param>
        /// <returns>list of <see cref="IndexResult"/></returns>
        public async Task<List<IndexResult>> SimilarityRequest(List<string> queries, List<string> texts) {
            // convert to json the queries and texts
            var body = new {
                queries,
                texts
            };

            // serialize the json
            var json = this.serializer.Serialize(body);

            // post the json to the TxtAI API
            var response = await this.client.PostAsync("/similarity", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            // deserialize the json
            return this.serializer.Deserialize<List<IndexResult>>(await response.Content.ReadAsStringAsync());
        }
    }
}