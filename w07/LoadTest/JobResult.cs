using System.Collections.Generic;
using System.Linq;

namespace LoadTest
{
    public class JobResult
    {
        private readonly int _id;

        public Dictionary<int, int> StatusCode { get; }

        // Elapsed Ticks
        public List<long> Elapsed { get; }
        public long Bytes { get; set; }

        public JobResult(int id)
        {
            _id = id;
            Bytes = 0;
            Elapsed = new List<long>();
            StatusCode = new Dictionary<int, int>();
        }

        public void AddStatusCode(int statusCode)
        {
            if (StatusCode.ContainsKey(statusCode))
            {
                StatusCode[statusCode] += 1;
            }
            else
            {
                StatusCode[statusCode] = 1;
            }
        }

        public override string ToString()
        {
            var elapsed = (from e in Elapsed select Utils.TickToMilliseconds(e)).ToArray();

            return $@"
Job [{_id}]
Req: [{string.Join(',', elapsed)}]
Bytes: {Bytes}
Status: {string.Join(',', Utils.OutputStatusCode(StatusCode))}";
        }
    }
}