using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;

namespace WebNetLab1.Tests;

public class DequeueTests
{
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void DequeueOne_WhenNonEmptyQueue_ThenRemoveAndReturnAndDecreaseCount<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        var dequeuedItem = queue.Dequeue();
        
        Assert.Equal(items[0], dequeuedItem);
        Assert.Equal(items.Length - 1, queue.Count);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void DequeueMany_WhenNonEmptyQueue_ThenRemoveAndReturnAndQueueEmpty<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        foreach (var item in items)
        {
            var dequeued = queue.Dequeue();
            Assert.Equal(item, dequeued);
        }
        
        Assert.Empty(queue);
    }
    
    [Fact]
    public void Dequeue_WhenEmptyQueue_ThenThrowInvalidOperationException()
    {
        var queue = new MyQueue<int>();

        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Dequeue_WhenQueueCleared_ThenThrowInvalidOperationException<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        queue.Clear();

        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void TryDequeueOne_WhenNonEmptyQueue_ThenRemoveAndReturnTrueAndDecreaseCount<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        var dequeuedResult = queue.TryDequeue(out var dequeuedItem);
        
        Assert.True(dequeuedResult);
        Assert.Equal(items[0], dequeuedItem);
        Assert.Equal(items.Length - 1, queue.Count);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void TryDequeueMany_WhenNonEmptyQueue_ThenRemoveAndReturnTrueAndQueueEmpty<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        foreach (var item in items)
        {
            var dequeuedResult = queue.TryDequeue(out var dequeuedItem);
            Assert.True(dequeuedResult);
            Assert.Equal(item, dequeuedItem);
        }
        
        Assert.Empty(queue);
    }
    
    [Fact]
    public void TryDequeue_WhenEmptyQueue_ThenReturnFalse()
    {
        var queue = new MyQueue<int>();

        Assert.False(queue.TryDequeue(out _));
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void TryDequeue_WhenQueueCleared_ThenReturnFalse<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        queue.Clear();

        Assert.False(queue.TryDequeue(out _));
    }
}