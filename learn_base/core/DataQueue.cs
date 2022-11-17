namespace learn_base.core;

/// Queue 队列中数据 先进先出
public class DataQueue
{
    private static readonly Queue<string> dataQueue = new(10);

    public static void Push(string data)
    {
        dataQueue.Enqueue(data);
    }

    public static string Pull()
    {
        return (dataQueue.Count > 0 ? dataQueue.Dequeue() : null)!;
    }

    public static Queue<string> GetQueue()
    {
        return dataQueue;
    }
}