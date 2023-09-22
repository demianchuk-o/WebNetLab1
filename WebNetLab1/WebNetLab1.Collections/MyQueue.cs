using System.Collections;

namespace WebNetLab1.Collections;

public class MyQueue<T>
{
    private MyQueueNode? _head;
    private MyQueueNode? _tail;

    public MyQueue()
    {
        _head = null;
        _tail = null;
    }

    public MyQueue(IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            Enqueue(item);
        }
    }
    
    public void Enqueue(T item)
    {
        var newNode = new MyQueueNode(item);
        if (_head is null)
        {
            _head = newNode;
            _tail = _head;
            return;
        }

        _tail.Next = newNode;
        _tail = newNode;
    }
    
    public T Dequeue()
    {
        if (_head is null)
        {
            throw new InvalidOperationException("The queue is empty.");
        }
        
        var removedData = _head.Data;

        _head = _head.Next;

        return removedData;
    }

    public T Peek()
    {
        if (_head is null)
        {
            ThrowForEmptyQueue();
        }

        return _head.Data;
    }

    private void ThrowForEmptyQueue()
    {
        throw new InvalidOperationException("The queue is empty.");
    }
    private class MyQueueNode
    {
        public T Data { get; }
        public MyQueueNode? Next { get; internal set; }

        public MyQueueNode(T data)
        {
            Data = data;
            Next = null;
        }
    }
}