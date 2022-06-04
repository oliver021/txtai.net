using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    public class Translation : RequestBase
    {
        public Translation(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        /// Translates text from source language into target language.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        public Task<string> Translate(string text, string target, string source)
        {
            // text should not be null or empty
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
            }

            var dictBody = new Dictionary<string, string>();

            // check for target
            if (target != null)
            {
                dictBody.Add("target", target);
            }

            // check for source
            if (source != null)
            {
                dictBody.Add("source", source);
            }

            return GetRawAsync(Api.TranslationEndpoint, dictBody);
        }

        /// <summary>
        /// Translates text from source language into target language.
        /// </summary>
        /// <param name="texts"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public  Task<string> BatchTranslate(List<string> texts, string target, string source)
        {
            // convert to json the query and texts
            var dictBody = new Dictionary<string, object>() {
                ["texts"] = texts,
            };

            // check for target
            if (target != null)
            {
                dictBody.Add("target", target);
            }

            // check for source
            if (source != null)
            {
                dictBody.Add("source", source);
            }

            // send translation request
            return PostRawAsync(Api.BatchTranslation, dictBody);
        }
    }
}
