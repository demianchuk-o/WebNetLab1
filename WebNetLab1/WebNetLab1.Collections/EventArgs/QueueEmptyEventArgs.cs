namespace WebNetLab1.Collections.EventArgs;

public class QueueEmptyEventArgs : System.EventArgs
{ 
    public string Message { get; }

    public QueueEmptyEventArgs(string message)
    {
        Message = message;
    }

}