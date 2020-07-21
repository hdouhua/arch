using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LoadTest
{
    public class LoadTestStatistics
    {
        private readonly int _totalRequests;
        private readonly float _requestPerSecond;
        private readonly long _receivedBytes;
        private readonly Dictionary<int, int> _responseStatus;
        private readonly double[] _latencies;

        public LoadTestStatistics(ConcurrentQueue<JobResult> result, bool debug = false)
        {
            this._responseStatus = new Dictionary<int, int>();
            this._receivedBytes = 0;

            int thread = 0;
            List<float> elapsed = new List<float>();
            foreach (var r in result)
            {
                thread++;
                this._receivedBytes += r.Bytes;
                foreach ((int k, int v) in r.StatusCode)
                {
                    if (this._responseStatus.ContainsKey(k))
                    {
                        this._responseStatus[k] += v;
                    }
                    else
                    {
                        this._responseStatus[k] = v;
                    }
                }
                elapsed.AddRange((from e in r.Elapsed select Utils.TickToMilliseconds(e)));

                if (debug)
                {
                    Console.WriteLine(r);
                }
            }

            elapsed.Sort();
            var tempArr = elapsed.ToArray();
            this._latencies = new[]
            {
                tempArr.Average(),
                tempArr.GetStdDev(),
                tempArr.GetPercentile(50),
                tempArr.GetPercentile(95)
            };

            this._totalRequests = this._responseStatus.Sum(it => it.Value);
            this._requestPerSecond = (float)(1000.0 / _latencies[0] * thread);

            elapsed = null;
            tempArr = null;
        }

        public override string ToString()
        {
            return $@"
Total Requests: {_totalRequests}
Requests/sec: {_requestPerSecond:F3}
Received Bytes: {(_receivedBytes / 1024.0 / 1024.0):F3} M
Response Status:
{string.Join('\n', Utils.OutputStatusCode(_responseStatus))}

Latency:
    Average: {_latencies[0]:F3} ms
    StdDev:  {_latencies[1]:F3} ms
    50%:     {_latencies[2]:F3} ms
    95%:     {_latencies[3]:F3} ms
";
        }
    }
}