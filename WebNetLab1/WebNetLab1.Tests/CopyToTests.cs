using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;

namespace WebNetLab1.Tests;

public class CopyToTests
{
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void CopyToGeneric_WhenNonEmptyQueue_ThenCopyToNewArray<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var array = new T[items.Length];
        
        queue.CopyTo(array, 0);
        
        Assert.Equal(items, array);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void CopyToGeneric_WhenArrayIsNull_ThenThrowArgumentNullException<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        
        Assert.Throws<ArgumentNullException>(() => queue.CopyTo(null!, 0));
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void CopyToGeneric_WhenIndexOutOfRange_ThenThrowArgumentOutOfRangeException<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var array = new T[items.Length];
        
        Assert.Throws<ArgumentOutOfRangeException>(() => queue.CopyTo(array, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => queue.CopyTo(array, items.Length + 1));
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void CopyToGeneric_WhenInsufficientSpace_ThenThrowArgumentException<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var array = new T[items.Length - 1];
        
        Assert.Throws<ArgumentException>(() => queue.CopyTo(array, 0));
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void CopyToGeneric_WhenEmptyQueue_ThenQuit<T>(T[] items)
    {
        var queue = new MyQueue<T>();
        var array = new T[items.Length];
        
        var arrayBeforeCopy = (T[])array.Clone();
        queue.CopyTo(array, 0);
        
        Assert.Equal(arrayBeforeCopy, array);
    }
    
    
}