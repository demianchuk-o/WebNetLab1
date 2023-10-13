using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;

namespace WebNetLab1.Tests;

public class ToArrayTests
{
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void ToArray_WhenEmptyQueue_ThenReturnEmptyArray<T>(T[] items)
    {
        var queue = new MyQueue<T>();

        var array = queue.ToArray();
        
        Assert.Empty(array);
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void ToArray_WhenNonEmptyQueue_ThenReturnNewArray<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);

        var array = queue.ToArray();
        
        Assert.Equal(items, array);
    }
}