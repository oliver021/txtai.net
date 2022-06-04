using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    /// <summary>
    /// txtai labels class.
    /// </summary>
    public class Labels : RequestBase
    {
        public Labels(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        ///   Applies a zero shot classifier to text using a list of labels. Returns a list of
        ///   * {id: value, score: value} sorted by highest score, where id is the index in labels.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        public async Task<List<IndexResult>> Label(string text, List<string> labels)
        {
            // convert to json the query and texts
            var body = new
            {
                text,
                labels
            };

            return await PostAsync<List<IndexResult>>(Api.LabelEndpoint, body);
        }

        /// <summary>
        /// Applies a zero shot classifier to list of text using a list of labels. Returns a list of
        /// * {id: value, score: value} sorted by highest score, where id is the index in labels per
        /// * text element.
        /// </summary>
        /// <param name="texts"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public async Task<List<IndexResult>> BatchLabel(List<string> texts, List<string> labels)
        {
            // convert to json the query and texts
            var body = new
            {
                texts,
                labels
            };

            return await PostAsync<List<IndexResult>>(Api.BatchLabel, body);
        }
    }
}
