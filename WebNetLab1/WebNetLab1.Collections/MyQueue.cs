using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace WebNetLab1.Collections;

public class MyQueue<T> : IEnumerable<T>, ICollection
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
    
    public int Count
    {
        get
        {
            int count = 0;
            var current = _head;
            while (current is not null)
            {
                count++;
                current = current.Next;
            }

            return count;
        }
    }

    public bool IsSynchronized => false;
    public object SyncRoot => this;
    
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex,
                "Index is out of bounds of this array.");
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("There's not enough space to copy into this range of an array.");
        }

        if (Count == 0)
        {
            return;
        }

        var current = _head;
        int index = arrayIndex;
        while (current is not null)
        {
            array[index] = current.Data;
            index++;
            current = current.Next;
        }
    }

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (array.Rank != 1)
        {
            throw new ArgumentException("The array has an invalid rank.");
        }

        if (array.GetLowerBound(0) != 0)
        {
            throw new ArgumentException("The array must have a lower bound of 0.");
        }
        
        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex,
                "Index is out of bounds of this array.");
        }
        
        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("There's not enough space to copy into this range of an array.");
        }
        
        if (Count == 0)
        {
            return;
        }
        
        var current = _head;
        int index = arrayIndex;
        while (current is not null)
        {
            array.SetValue(current.Data, index);
            index++;
            current = current.Next;
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

        var removedData = HandleDequeue();

        return removedData;
    }

    public bool TryDequeue([MaybeNullWhen(false)] out T result)
    {
        if (_head is null)
        {
            result = default;
            return false;
        }

        result = HandleDequeue();
        return true;
    }

    public T Peek()
    {
        if (_head is null)
        {
            ThrowForEmptyQueue();
        }

        return _head.Data;
    }

    public bool TryPeek([MaybeNullWhen(false)] out T result)
    {
        if (_head is null)
        {
            result = default;
            return false;
        }

        result = _head.Data;
        return true;
    }

    private void ThrowForEmptyQueue()
    {
        throw new InvalidOperationException("The queue is empty.");
    }

    private T HandleDequeue()
    {
        var dequeuedData = _head.Data;

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _head = _head.Next;
        }

        return dequeuedData;
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