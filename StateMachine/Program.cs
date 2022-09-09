public static class Program
{
    static async Task Main(string[] args)
    {
        var taskloop = new TaskLoop()
        {
            A = () => Console.WriteLine($"After Delay { Thread.CurrentThread.ManagedThreadId };"),
            Max = 5
        };
        Console.WriteLine($"Hello World { Thread.CurrentThread.ManagedThreadId};");
        taskloop.Run();
        taskloop.Task.Wait();
        Console.ReadKey();
    }

    }
    

