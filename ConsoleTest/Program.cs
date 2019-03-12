using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var content= "{'access_token': 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6IjFmZTRmZGQ0ODZiNTRlMGM5MzQ4N2I5NTUxNDJmZTYzIiwidXNlcl9pZCI6IkVOMjAxNzA5MjExNDI4IiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDEwIiwiYXVkIjoiMWZlNGZkZDQ4NmI1NGUwYzkzNDg3Yjk1NTE0MmZlNjMiLCJleHAiOjE1MzE0NzczOTksIm5iZiI6MTUzMTQ3NzMzOX0.yfFEG6kDBvZ022YlDJ_wNz3wQzFCNCLpW7No_W_Aspo'}";
            //var dic = "{'access_token':'123423432adfasdga'}";
            //var json = JObject.Parse(dic);
            //Console.WriteLine(json.ToString());
            //Console.Read();

            //var matches = FindMatches("D:/Tests/HTMLTest/TestXml.xml", "item", "872-AA");
            //matches.ToList().ForEach(m => Console.WriteLine(m.Value));

            //Console.WriteLine($"The current main thread id is {Thread.CurrentThread.ManagedThreadId}");
            //var asyncMessage = GetMessageAsync();
            ////asyncMessage.Wait();
            //Console.WriteLine(asyncMessage.Result);
            //Console.WriteLine($"The current main thread id is {Thread.CurrentThread.ManagedThreadId}");

            //var listItems = new List<string>() { "Xiao Jin", "Xiao Xue", "Lai Xi" };
            //ParallelProcess(listItems);

            //if (args.Length == 0)
            //{
            //    Console.WriteLine("There is no folder path provided.");
            //    return;
            //}

            //GetFolderSize(args[0]);

            //Console.WriteLine(Int32.MaxValue);
            //var b = new StringBuilder();
            //b.Append("TestA");
            //b.Append("TestB");

            //Console.Write(b.Length);

            //#region object proerty name test
            //var pName = "身份证号";
            //var propertyInfo = typeof(People).GetProperty(pName);
            //var test = new People();
            //propertyInfo.SetValue(test, Guid.NewGuid());
            //Console.WriteLine($"{test.Id}");
            //#endregion

            //var props = typeof(People).GetProperties();
            //var man = new People();

            //props.ToList().ForEach(p =>
            //{
            //    var value = p.GetValue(man);

            //    if (value != null)
            //    {
            //        Console.WriteLine($"Property:{p.Name}. Value:{value.ToString()}");
            //    }
            //});

            //float test = 12345.333123421342342f;

            //var a = new int[] { 3, 6, 4, 5, 7, 9, 8, 2, 1, 0 };

            //for (int i = 0; i < a.Length; i++)
            //{
            //    for (int j = 0; j < a.Length -1 - i; j++)
            //    {
            //        if (a[j] > a[j + 1])
            //        {
            //            var temp = a[j];
            //            a[j] = a[j + 1];
            //            a[j + 1] = temp;
            //        }
            //    }
            //}

            //a.ToList().ForEach(item => Console.Write(item));
            //ThreadPool.GetMinThreads(out int workerCount, out int portCount);
            //Console.WriteLine($"Minimum thread in threadpool is {workerCount} and the prot is {portCount}");

            #region Quartz
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            RunProgramRunExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to end the program");
            Console.Read();
            #endregion

            Console.Read();
        }
        //static void Main(string[] args)
        //{
        //    var test = new ThreadLocal<Test>(() =>
        //        { return new Test(); }
        //    );

        //    Action action = () =>
        //          {
        //              //bool isCreated = threadName.IsValueCreated;
        //              //Console.WriteLine(threadName.Value + (isCreated ? ("Repeated"):""));
        //              //number.Value = Guid.NewGuid();
        //              Console.WriteLine($"{test.Value.Id} threadId is: {Thread.CurrentThread.ManagedThreadId}.");
        //          };

        //    Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism=10},
        //        action, action, action, action, action, action, action, action, action, action, action);
        //    //Console.Write(number.Values);
        //    test.Dispose();
        //    Console.Read();
        //}

        //static void Main(string[] args)
        //{
        //    var number = new ThreadLocal<int>(() => 0, trackAllValues: false);

        //    Parallel.For(0, 10, i =>
        //    {
        //        number.Value += 1;
        //        Console.WriteLine(number.Value + " " + Thread.CurrentThread.ManagedThreadId);
        //    });

        //    Console.Read();
        //}

        public static IEnumerable<XElement> FindMatches(string xmlPath, string elementName, string attributeValue)
        {
            //var xmlDoc = XDocument.Load("D:/Tests/HTMLTest/XmlAnnotation.xml");
            var xmlDoc = XDocument.Load(xmlPath);
            return from el in xmlDoc.Elements(elementName)
                   where (string)el.Element(attributeValue) == attributeValue
                   select el;
        }

        public static async Task<string> GetMessageAsync()
        {
            var messageTask = Task.Run(() =>
            {
                Console.WriteLine($"This is async thread. Current thread id is {Thread.CurrentThread.ManagedThreadId}");
                return "Hello. Complex execution is finished.";
            });

            return await messageTask;
        }

        public static void ParallelProcess(ICollection<string> items)
        {
            Parallel.ForEach(items, item => Console.WriteLine($"Processing item, the current thread id is {Thread.CurrentThread.ManagedThreadId}"));
        }

        public static void GetFolderSize(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The provided path is not existing");
                return;
            }

            long totalSize = 0;
            var files = Directory.GetFiles(folderPath);
            var watch = new Stopwatch();
            watch.Start();
            Parallel.For(0, files.Length,
                index =>
                {
                    var fi = new FileInfo(files[index]);
                    var size = fi.Length;
                    Interlocked.Add(ref totalSize, size);
                });
            watch.Stop();
            Console.WriteLine($"The time used to run parallelly is {watch.Elapsed}");
            Console.Write($"The fodler {folderPath} has:");
            Console.WriteLine($"{files.Length} files and {totalSize} bytes");
            watch.Reset();

            totalSize = 0;
            watch.Start();
            foreach (var fileName in files)
            {
                var fi = new FileInfo(fileName);
                var size = fi.Length;
                totalSize += size;
            }

            watch.Stop();
            Console.WriteLine($"The time used to run normally is {watch.Elapsed}");
            Console.Write($"The fodler {folderPath} has:");
            Console.WriteLine($"{files.Length} files and {totalSize} bytes");
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                await Task.Delay(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        // simple log provider to get something to the console
        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class People
    {
        [JsonProperty("身份证号")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public bool IsMarried { get; set; }

        public People()
        {
            Id = Guid.NewGuid();
            Name = "Ross";
        }
    }

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
