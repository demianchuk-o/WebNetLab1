using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;

namespace WebNetLab1.Tests;

public class PeekTests
{
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Peek_WhenNonEmptyQueue_ThenReturnItem<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        var peekedItem = queue.Peek();
        
        Assert.Equal(items[0], peekedItem);
    }
    
    [Fact]
    public void Peek_WhenEmptyQueue_ThenThrowInvalidOperationException()
    {
        var queue = new MyQueue<int>();

        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Peek_WhenQueueCleared_ThenThrowInvalidOperationException<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        queue.Clear();

        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void TryPeek_WhenNonEmptyQueue_ThenReturnTrueAndItem<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        
        var peekedResult = queue.TryPeek(out var peekedItem);
        
        Assert.True(peekedResult);
        Assert.Equal(items[0], peekedItem);
    }
    
    [Fact]
    public void TryPeek_WhenEmptyQueue_ThenReturnFalseAndDefault()
    {
        var queue = new MyQueue<int>();

        var peekedResult = queue.TryPeek(out var peekedItem);
        
        Assert.False(peekedResult);
        Assert.Equal(default, peekedItem);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void TryPeek_WhenQueueCleared_ThenReturnFalseAndDefault<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        queue.Clear();
        
        var peekedResult = queue.TryPeek(out var peekedItem);
        
        Assert.False(peekedResult);
        Assert.Equal(default, peekedItem);
    }
}