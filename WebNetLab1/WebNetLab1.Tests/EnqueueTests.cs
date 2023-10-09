using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;

namespace WebNetLab1.Tests;

public class EnqueueTests
{
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Enqueue_WhenEmptyQueue_ShouldAddItems<T>(T[] items)
    {
        var queue = new MyQueue<T>();

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }
        
        Assert.Equal(items, queue);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Enqueue_WhenNonEmptyQueue_ShouldAddItems<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }
        
        Assert.Equal(items.Length * 2, queue.Count);
    }
}