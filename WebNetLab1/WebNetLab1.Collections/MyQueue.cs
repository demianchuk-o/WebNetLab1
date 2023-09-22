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