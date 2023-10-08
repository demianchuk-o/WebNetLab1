using WebNetLab1.Collections;
using Xunit;

namespace WebNetLab1.Tests;

public class InitializationTests
{
    [Fact]
    public void ParameterlessCtor_WhenCalled_ThenEmptyQueue()
    {
        var queue = new MyQueue<int>();
        
        Assert.Empty(queue);
    }

    [Fact]
    public void CtorWithSource_WhenNonEmptySource_ThenQueueEqualsSource()
    {
        var source = Enumerable.Range(1, 5);
        var queue = new MyQueue<int>(source);
        
        Assert.Equal(source, queue);
    }

    [Fact]
    public void CtorWithSource_WhenEmptySource_ThenThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>((() => { var queue = new MyQueue<int>(null);}));
    }
}