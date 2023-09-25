namespace WebNetLab1.Collections.EventArgs;

public class PeekEventArgs<T> : System.EventArgs
{
    public string Message { get; }
    public T Data { get; }

    public PeekEventArgs(string message, T data)
    {
        Message = message;
        Data = data;
    }

}