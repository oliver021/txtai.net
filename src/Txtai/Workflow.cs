using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    /// <summary>
    /// Txtai workflow class.
    /// </summary>
    public class Workflow : RequestBase
    {
        public Workflow(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        ///  Executes a named workflow using elements as input.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        public async Task<List<IndexResult>> WorkflowRequest(string query, List<string> texts)
        {
            // convert to json the query and texts
            var body = new
            {
                query,
                texts
            };

            return await PostAsync<List<IndexResult>>(Api.WorkflowEndpoint, body);
        }
    }
}
