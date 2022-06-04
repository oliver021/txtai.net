using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    /// <summary>
    /// 
    /// </summary>
    public class TextExtractor : RequestBase
    {
        public TextExtractor(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        /// Extracts text from a file at path.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<string> Textract(string file)
            => GetRawAsync(Api.TextractEndpoint, new() { ["file"] = file });

        /// <summary>
        /// Extracts text from a file at many files.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<string> BatchTextract(string files)
            => GetRawAsync(Api.BatchTextract, new() { ["files"] = files });
    }
}
