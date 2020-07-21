using System;
using System.Threading;
using McMaster.Extensions.CommandLineUtils;

namespace LoadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication { Name = "LoadTest", Description = "A Simple LoadTest Tool" };

            app.HelpOption("-?|-h|--help");

            var optionUri = app.Option("--uri <URL>", "load test URL", CommandOptionType.SingleValue).IsRequired();
            var optionCount = app.Option("--count <count>", "number of requests for each thread", CommandOptionType.SingleValue);
            var optionThreads = app.Option("--thread <thread>", "total number of threads", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                if (optionUri.HasValue())
                {
                    var uri = new Uri(optionUri.Value() ?? "");
                    int.TryParse(optionThreads.Value() ?? "2", out var thread);
                    int.TryParse(optionCount.Value() ?? "10", out var count);

                    Console.WriteLine($"Load Test: [{uri}] with [{thread}] threads and [{count}] requests/thread");

                    var result = new TestRunner(uri).Run(thread, count, new CancellationToken());
                    var stat = new LoadTestStatistics(result, false);

                    Console.WriteLine(stat);
                }
                else
                {
                    app.ShowInHelpText = true;
                    app.ShowHelp();
                }

                return 0;
            });

            app.Execute(args);
        }
    }
}