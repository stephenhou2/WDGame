/// <summary>
///  简单队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class SimpleQueue<T>
{
    private SimpleQueueNode<T> Head;
    private SimpleQueueNode<T> Tail;

    public SimpleQueueNode<T> Current;

    public bool MoveNext()
    {
        if (Current == null)
            return false;

        var next = Current.GetNext();
        if (next == null)
            return false;

        Current = next;
        return true;
    }

    public void Reset()
    {
        Current = Head;
    }
    public int Count { get; private set; } = 0;

    public void Enqueue(T obj)
    {
        SimpleQueueNode<T> node = new SimpleQueueNode<T>(obj);
        // 队列内没有元素
        if(Tail == null)
        {
            Head = node;
            Tail = Head;
            Count = 1;
            return;
        }

        // 队列内有元素，加到队尾
        Tail.SetNext(node);
        node.SetLast(Tail);
        Tail = node;
        Count++;
    }

    public bool HasItem()
    {
        return Head != null;
    }

    public T Dequeue()
    {
        // 队列内没有元素
        if (Count == 0)
        {
            return default(T);
        }

        // 队列内只有一个元素
        T obj = Head.GetObj();
        if(Count == 1)
        {
            Head = null;
            Tail = null;
            Count = 0;
            return obj;
        }

        // 队列内有多个元素
        var next = Head.GetNext();
        if (next != null)
        {
            next.SetLast(null);
        }
        Head = next;
        Count--;
        return obj;
    }
}

public class SimpleQueueNode<T>
{
    private SimpleQueueNode<T> mLast;
    private SimpleQueueNode<T> mNext;

    private T mObj;

    public SimpleQueueNode(T obj)
    {
        mObj = obj;
    }

    public T GetObj()
    {
        return mObj;
    }

    public void SetLast(SimpleQueueNode<T> node)
    {
        mLast = node;
    }

    public SimpleQueueNode<T> GetLast()
    {
        return mLast;
    }

    public void SetNext(SimpleQueueNode<T> node)
    {
        mNext = node;
    }

    public SimpleQueueNode<T> GetNext()
    {
        return mNext;
    }


}
