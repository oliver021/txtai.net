using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Txtai
{
    /// <summary>
    /// txtai segmentation class.
    /// </summary>
    public class Segmentation : RequestBase
    {
        public Segmentation(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        /// Segments text into semantic units.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<string> Segment(string text) 
            => GetRawAsync(Api.SegmentEndpoint, new() { ["text"] = text });

        /// <summary>
        /// Segments text into semantic units.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<string> BatchSegment(string texts)
            => GetRawAsync(Api.BatchSegment, new() { ["texts"] = texts });
    }
}
