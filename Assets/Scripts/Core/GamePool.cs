using System;
using System.Collections.Generic;

public class GamePool
{
    private Type type;
    private Stack<object> pool;

    public GamePool(Type type)
    {
        this.type = type;
        this.pool = new Stack<object>();
    }

    public void PushObj(object obj)
    {
        if(!obj.GetType().Equals(type))
        {
            Log.Error(ErrorLevel.Normal, "PushObj Failed,type check failed!");
            return;
        }

        pool.Push(obj);
    }

    public T PopObj<T>() where T:class
    {
        if(!typeof(T).Equals(type))
        {
            Log.Error(ErrorLevel.Normal, "PopObj Failed,type check failed!");
            return null;
        }

        if(pool.Count  == 0)
        {
            return null;
        }

        return pool.Pop() as T;
    }

    public void Clear()
    {
        pool.Clear();
    }


}
