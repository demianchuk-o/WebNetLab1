using WebNetLab1.Collections;
using WebNetLab1.Collections.EventArgs;

public class Program
{
    static void Main()
    {
        void DisplayCollection(IEnumerable<int> collection)
        {
            foreach (var item in collection)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }
        
        void OnPeek(object? sender, PeekEventArgs<int> e)
        {
            Console.WriteLine($"Peek event: {e.Message}, data: {e.Data}");
        }
        
        void OnQueueEmpty(object? sender, QueueEmptyEventArgs e)
        {
            Console.WriteLine($"Queue empty event: {e.Message}");
        }
        
        var queue = new MyQueue<int>();
        queue.PeekEvent += OnPeek;
        queue.QueueEmptyEvent += OnQueueEmpty;
        
        for (int i = 0; i < 10; i++)
        {
            queue.Enqueue(i);
        }
        Console.WriteLine("Enqueue check, queue:");
        DisplayCollection(queue);
        
        Console.WriteLine("Peek check, front element: {0}", queue.Peek());
        
        if (queue.TryPeek(out var result))
        {
            Console.WriteLine("TryPeek check, front element: {0}", result);
        }
        
        Console.WriteLine("Contains check before dequeue, does the queue contain 3? {0}", queue.Contains(3));
        
        Console.WriteLine("Dequeue check, dequeued elements:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(queue.Dequeue());
        }
        
        Console.WriteLine("Contains check after dequeue, does the queue contain 3? {0}", queue.Contains(3));
        
        var array = queue.ToArray();
        Console.WriteLine("ToArray check, array:");
        DisplayCollection(array);
        
        Console.WriteLine("TryDequeue check, dequeued elements:");
        for (int i = 0; i < 3; i++)
        {
            queue.TryDequeue(out var item);
            Console.WriteLine(item);
        }
        
        Console.WriteLine("After TryDequeue check, queue:");
        DisplayCollection(queue);

        Console.WriteLine("TryDequeue event trigger, last elements:");
        for (int i = 0; i < 2; i++)
        {
            queue.TryDequeue(out var item);
            Console.WriteLine(item);
        }

        Console.WriteLine("Trying to initialize a queue from null source:");
        try
        {
            var queueFromNull = new MyQueue<int>(null);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.Message);
        }

        var queueFromArray = new MyQueue<int>(new int[] { 1, 3, 5, 7, 9 });
        Console.WriteLine("Trying to initialize a queue from array, queue:");
        DisplayCollection(queueFromArray);
        
        
        var shortArray = new int[4];
        Console.WriteLine("CopyTo check on short array:");
        try
        {
            queueFromArray.CopyTo(shortArray, 0);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        var longArray = new int[5];
        queueFromArray.CopyTo(longArray, 0);
        Console.WriteLine("CopyTo check on long array, array:");
        DisplayCollection(longArray);
        
        queueFromArray.QueueEmptyEvent += OnQueueEmpty;
        queueFromArray.Clear();
        Console.WriteLine("Clear check, queue:");
        DisplayCollection(queueFromArray);
    }
}
