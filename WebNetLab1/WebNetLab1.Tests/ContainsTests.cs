using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;

namespace WebNetLab1.Tests;

public class ContainsTests
{
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Contains_WhenHasElement_ThenReturnTrue<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        
        var contains = queue.Contains(items[0]);
        
        Assert.True(contains);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void Contains_WhenHasNoElement_ThenReturnFalse<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        queue.Dequeue();
        var contains = queue.Contains(items[0]);
        
        Assert.False(contains);
    }
}