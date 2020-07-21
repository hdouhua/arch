using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadTest
{
    public class Job
    {
        private readonly JobResult _result;

        private readonly Uri _uri;
        private readonly HttpClient _httpClient;

        public Job(int id, Uri uri)
        {
            _result = new JobResult(id);

            _uri = uri;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
        }

        public async Task DoWork()
        {
            var sw = Stopwatch.StartNew();
            using (var response = await _httpClient.GetAsync(_uri))
            {
                sw.Stop();
                var contentStream = await response.Content.ReadAsStreamAsync();
                var bytes = contentStream.Length + response.Headers.ToString().Length;
                var statusCode = (int)response.StatusCode;

                _result.Bytes += bytes;
                _result.AddStatusCode(statusCode);
                // record elapsed for succeeded request only
                if (statusCode >= 200 && statusCode < 300)
                {
                    _result.Elapsed.Add(sw.ElapsedTicks);
                }
            }
        }

        public JobResult GetResult()
        {
            return _result;
        }
    }
}