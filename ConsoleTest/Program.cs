using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new ThreadLocal<Test>(() =>
                { return new Test(); }
            );

            Action action = () =>
                  {
                      //bool isCreated = threadName.IsValueCreated;
                      //Console.WriteLine(threadName.Value + (isCreated ? ("Repeated"):""));
                      //number.Value = Guid.NewGuid();
                      Console.WriteLine($"{test.Value.Id} threadId is: {Thread.CurrentThread.ManagedThreadId}.");
                  };

            Parallel.Invoke(action, action, action, action, action, action, action, action, action, action, action);
            //Console.Write(number.Values);
            test.Dispose();
            Console.Read();
        }

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
    }

    public class Test
    {
        public Guid Id { get; set; }

        public Test()
        {
            Id = Guid.NewGuid();
        }
    }
}
