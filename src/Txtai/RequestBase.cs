using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RequestBase
    {
        /// <summary>
        /// The client used to communicate with the TxtAI API.
        /// </summary>
        protected readonly HttpClient client;

        /// <summary>
        /// The serializer used to serialize and deserialize the JSON.
        /// </summary>
        protected readonly IApiSerializer serializer;

        /// <summary>
        /// Return current http client instance.
        /// </summary>
        /// <returns></returns>
        public HttpClient GetHttpClient()
        {
            return client;
        }

        /// <summary>
        /// Construct to inject HttpClient.
        /// </summary>
        /// <param name="client">HttpClient</param>
        public RequestBase(HttpClient client, IApiSerializer serializer)
        {
            this.client = client;
            this.serializer = serializer;
        }

        /// <summary>
        /// Get raw content string from "GET" request.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task GetAndVoid(string path, Dictionary<string, string> query)
           // post the json to the TxtAI API
           => client.GetAsync(PreparePathForQueries(path, query));

        /// <summary>
        /// Get raw content string from "GET" request.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<string> GetRawAsync(string path, Dictionary<string, string> query)
           // post the json to the TxtAI API
           => client.GetStringAsync(PreparePathForQueries(path, query));

        /// <summary>
        /// get content deserialized from "GET" request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string path, Dictionary<string, string> query)
           // post the json to the TxtAI API
           => serializer.Deserialize<T>(await client.GetStringAsync(PreparePathForQueries(path, query)));

        /// <summary>
        /// Post and take raw content string from "GET" request.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<string> PostRawAsync(string path, object body)
        {
            // serialize the json
            var json = serializer.Serialize(body);

            // post the json to the TxtAI API
            var response = await client.PostAsync(path, new StringContent(json, Encoding.UTF8, "application/json"));

            // but without deserialization
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Post and parse result from response content using typeparam.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string path, object body)
        {
            // serialize the json
            var json = serializer.Serialize(body);

            // post the json to the TxtAI API
            var response = await client.PostAsync(path, new StringContent(json, Encoding.UTF8, "application/json"));

            // deserialize the json
            return serializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Post and take raw content string from "GET" request.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task OnlyPostAsync(string path, object body)
        {
            // serialize the json
            var json = serializer.Serialize(body);

            // post the json to the TxtAI API
            _ = await client.PostAsync(path, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Prepares the path to concat query params if are set.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private string PreparePathForQueries(string path, Dictionary<string, string> query)
        => 
            (query is not null && query.Any()) ? (path + "?" + ConvertQuery(query)) : path;
        

        /// <summary>
        /// Simple query params formater
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string ConvertQuery(Dictionary<string, string> query)
        {
            return string.Join("&", query.Select(q => $"{q.Key}={q.Value}"));
        }
    }
}