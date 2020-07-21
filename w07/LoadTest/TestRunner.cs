using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTest
{
    public class TestRunner
    {
        private readonly Uri _uri;

        public TestRunner(Uri uri)
        {
            _uri = uri;
        }

        public ConcurrentQueue<JobResult> Run(int threads, int count, CancellationToken cancellationToken)
        {
            var results = new ConcurrentQueue<JobResult>();

            var tasks = new List<Task>();
            for (var i = 0; i < threads; i++)
            {
                var i1 = i;
                tasks.Add(Task.Run(() => RunBatch(i1, count, results), cancellationToken));
            }
            Task.WaitAll(tasks.ToArray());

            return results;
        }

        private async Task RunBatch(int batchIndex, int count, ConcurrentQueue<JobResult> results)
        {
            var job = new Job(batchIndex, this._uri);

            for (var i = 0; i < count; i++)
            {
                await job.DoWork();
            }

            results.Enqueue(job.GetResult());
        }
    }
}