using System.Collections;

namespace WebNetLab1.Tests.ClassData;

public class MultipleItemsQueueData : IEnumerable<object[]>
{
    private static readonly int[] IntArray = { 1, 2, 3, 4, 5, 6, 7 };
    private static readonly string[] StringArray = { "a", "b", "c", "d", "e", "f", "g" };
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new[] { IntArray };
        yield return new[] { StringArray };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}