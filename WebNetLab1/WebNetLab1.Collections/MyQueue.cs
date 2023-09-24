using System.Collections;

namespace WebNetLab1.Collections;

public class MyQueue<T> : IEnumerable<T>
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
    
    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
            ThrowForEmptyQueue();
        }
        
        var removedData = _head.Data;

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _head = _head.Next;
        }
        

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