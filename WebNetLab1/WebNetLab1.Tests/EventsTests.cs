using WebNetLab1.Collections;
using WebNetLab1.Tests.ClassData;
using Xunit;
using FakeItEasy;
using WebNetLab1.Collections.EventArgs;

namespace WebNetLab1.Tests;

public class EventsTests
{
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void OnQueueEmpty_WhenLastElementDequeued_ThenOneCallbackHappened<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.QueueEmptyEvent += eventHandler.Callback;
        
        foreach (var item in items)
        {
            queue.Dequeue();
        }
        
        A.CallTo(() => eventHandler.Callback(A<object?>._, A<QueueEmptyEventArgs>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void QueueEmptyEventArgs_WhenLastElementDequeued_ThenMessageIsNotNull<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.QueueEmptyEvent += eventHandler.Callback;
        
        foreach (var item in items)
        {
            queue.Dequeue();
        }

        A.CallTo(() => eventHandler.Callback(A<object?>._, A<QueueEmptyEventArgs>._))
            .WhenArgumentsMatch(args => args.Get<QueueEmptyEventArgs>(1)?.Message is not null)
            .MustHaveHappened();
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void OnQueueEmpty_WhenQueueCleared_ThenOneCallbackHappened<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.QueueEmptyEvent += eventHandler.Callback;
        
        queue.Clear();
        
        A.CallTo(() => eventHandler.Callback(A<object?>._, A<QueueEmptyEventArgs>._))
            .WhenArgumentsMatch(args => args.Get<QueueEmptyEventArgs>(1)?.Message is not null)
            .MustHaveHappenedOnceExactly();
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void QueueEmptyEventArgs_WhenQueueCleared_ThenMessageIsNotNull<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.QueueEmptyEvent += eventHandler.Callback;
        
        queue.Clear();

        A.CallTo(() => eventHandler.Callback(A<object?>._, A<QueueEmptyEventArgs>._))
            .WhenArgumentsMatch(
                args => args.Get<QueueEmptyEventArgs>(1)?.Message is not null)
            .MustHaveHappened();
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void OnPeek_WhenPeekCalledNTimes_ThenNCallbacksHappened<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.PeekEvent += eventHandler.Callback;
        var numberOfPeeks = Random.Shared.Next(1, 10);

        for (var i = 0; i < numberOfPeeks; i++)
        {
            queue.Peek();
        }
        
        A.CallTo(() => eventHandler.Callback(A<object?>._, A<PeekEventArgs<T>>._))
            .MustHaveHappened(numberOfPeeks, Times.Exactly);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void PeekEventArgs_WhenPeekCalled_ThenValidArgs<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.PeekEvent += eventHandler.Callback;

        queue.Peek();
        
        A.CallTo(() => eventHandler.Callback(A<object?>._, A<PeekEventArgs<T>>._))
            .WhenArgumentsMatch(args => 
                (args.Get<PeekEventArgs<T>>(1)?.Data.Equals(items[0]) ?? false)
                && args.Get<PeekEventArgs<T>>(1)?.Message is not null)
            .MustHaveHappened();
    }
    
    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void OnPeek_WhenTryPeekCalledNTimes_ThenNCallbacksHappened<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.PeekEvent += eventHandler.Callback;
        var numberOfPeeks = Random.Shared.Next(1, 10);
        
        for (var i = 0; i < numberOfPeeks; i++)
        {
            queue.TryPeek(out _);
        }
        
        A.CallTo(() => eventHandler.Callback(A<object?>._, A<PeekEventArgs<T>>._))
            .MustHaveHappened(numberOfPeeks, Times.Exactly);
    }

    [Theory]
    [ClassData(typeof(MultipleItemsQueueData))]
    public void PeekEventArgs_WhenTryPeekCalled_ThenValidArgs<T>(T[] items)
    {
        var queue = new MyQueue<T>(items);
        var eventHandler = A.Fake<ITestEventHandler>();
        queue.PeekEvent += eventHandler.Callback;

        queue.TryPeek(out _);
        
        A.CallTo(() => eventHandler.Callback(A<object?>._, A<PeekEventArgs<T>>._))
            .WhenArgumentsMatch(args => 
                (args.Get<PeekEventArgs<T>>(1)?.Data.Equals(items[0]) ?? false)
                && args.Get<PeekEventArgs<T>>(1)?.Message is not null)
            .MustHaveHappened();
    }
}

public interface ITestEventHandler
{
    void Callback(object? sender, EventArgs e);
}