/// <summary>
///  简单队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class GameQueue<T>
{
    private GameQueueNode<T> Head;
    private GameQueueNode<T> Tail;

    public GameQueueNode<T> Current;

    public bool MoveNext()
    {
        if(Current == null)
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
    public int Count = 0;

    public void Enqueue(T obj)
    {
        GameQueueNode<T> node = new GameQueueNode<T>(obj);
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
    }

    public bool HasItem()
    {
        return Head != null;
    }

    public T Dequeue()
    {
        // 队列内没有元素
        if (!HasItem())
            return default(T);

        T obj = Head.GetObj();
        var next = Head.GetNext();
        if (next != null)
        {
            next.SetLast(null);
        }
        Head = next;
        return obj;
    }
}

public class GameQueueNode<T>
{
    private GameQueueNode<T> mLast;
    private GameQueueNode<T> mNext;

    private T mObj;

    public GameQueueNode(T obj)
    {
        mObj = obj;
    }

    public T GetObj()
    {
        return mObj;
    }

    public void SetLast(GameQueueNode<T> node)
    {
        mLast = node;
    }

    public GameQueueNode<T> GetLast()
    {
        return mLast;
    }

    public void SetNext(GameQueueNode<T> node)
    {
        mNext = node;
    }

    public GameQueueNode<T> GetNext()
    {
        return mNext;
    }


}
