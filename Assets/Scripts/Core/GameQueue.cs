/// <summary>
///  简单队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class GameQueue<T>
{
    private GameQueueNode<T> Head;
    private GameQueueNode<T> Tail;

    public GameQueueNode<T> Current;

    public GameQueueNode<T> MoveNext()
    {
        if(Current == null)
            return null;

        return Current.GetNext();
    }

    public void Reset()
    {
        Current = Head;
    }

    public void Enqueue(T obj)
    {
        GameQueueNode<T> node = new GameQueueNode<T>(obj);
        // 队列内没有元素
        if(Tail == null)
        {
            Head = node;
            Head.SetNext(node);
            node.SetLast(Head);
            Tail = Head;
            return;
        }

        // 队列内有元素，加到队尾
        Tail.SetNext(node);
        node.SetLast(Tail);
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
        Head = Head.GetNext();

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
