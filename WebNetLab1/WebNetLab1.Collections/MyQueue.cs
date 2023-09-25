using System.Collections;
using System.Diagnostics.CodeAnalysis;
using WebNetLab1.Collections.EventArgs;

namespace WebNetLab1.Collections;

public class MyQueue<T> : IEnumerable<T>, ICollection
{
    private MyQueueNode? _head;
    private MyQueueNode? _tail;

    public event EventHandler<PeekEventArgs<T>> PeekEvent;
    public event EventHandler<QueueEmptyEventArgs> QueueEmptyEvent;
    
    public MyQueue()
    {
        _head = null;
        _tail = null;
    }

    public MyQueue(IEnumerable<T> source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }
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

    public void Clear()
    {
        _head = null;
        _tail = null;
        OnQueueEmpty(new QueueEmptyEventArgs("A queue was cleared."));
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
            throw new InvalidOperationException("The queue is empty.");
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
            throw new InvalidOperationException("The queue is empty.");
        }

        var result = _head.Data;
        OnPeek(new PeekEventArgs<T>("An element was retrieved from Peek method.", result));
        return result;
    }

    public bool TryPeek([MaybeNullWhen(false)] out T result)
    {
        if (_head is null)
        {
            result = default;
            return false;
        }

        result = _head.Data;
        OnPeek(new PeekEventArgs<T>("\"An element was retrieved from TryPeek method.", result));
        return true;
    }

    public bool Contains(T item)
    {
        var current = _head;
        while (current is not null)
        {
            if (current.Data.Equals(item))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public T[] ToArray()
    {
        if (Count == 0)
        {
            return Array.Empty<T>();
        }

        var array = new T[Count];

        var current = _head;
        int index = 0;
        while (current is not null)
        {
            array[index] = current.Data;
            current = current.Next;
        }

        return array;
    }

    private T HandleDequeue()
    {
        var dequeuedData = _head.Data;

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
            
            OnQueueEmpty(new QueueEmptyEventArgs("The last element was dequeued."));
        }
        else
        {
            _head = _head.Next;
        }

        return dequeuedData;
    }

    private void OnQueueEmpty(QueueEmptyEventArgs e)
    {
        QueueEmptyEvent?.Invoke(this, e);
    }
    private void OnPeek(PeekEventArgs<T> e)
    {
        PeekEvent?.Invoke(this, e);
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