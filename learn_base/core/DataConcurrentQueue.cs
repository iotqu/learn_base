using System.Collections.Concurrent;

namespace learn_base.core;

public class DataConcurrentQueue
{
    private static readonly ConcurrentQueue<string> dataQueue = new();

    public static void Push(string data)
    {
        dataQueue.Enqueue(data);
    }

    public static void Pull()
    {
        int count = dataQueue.Count;
        while (true)
        {
            var source = dataQueue.TryDequeue(out var entity);
            if (source)
            {
                Console.WriteLine(entity);
            }
        }
        /*for (int i = 0; i < count; i++)
        {
            var source = dataQueue.TryDequeue(out var entity);
            if (source)
            {
                Console.WriteLine(entity);
            }
        }*/
    }
}