using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    public class Transcription : RequestBase
    {
        public Transcription(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        ///  Transcribes audio files to text.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        public Task<string> Transcribe(string file)
        {
            // convert to json the query and texts
            return  GetRawAsync(Api.TranscriptionEndpoint, new() { ["file"] = file });
        }

        /// <summary>
        ///  Transcribes audio files to text.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        public Task<string> BatchTranscribe(IEnumerable<string> files)
        {
            // convert to json the query and texts
            return PostRawAsync(Api.BatchTranscription, new { files });
        }
    }
}
