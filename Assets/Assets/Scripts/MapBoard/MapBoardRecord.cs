using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoardRecord : IMapEditRecord
{
    private IMapEditRecord mLastRecord;
    private IMapEditRecord mNextRecord;

    public void Recover()
    {
        
    }

    public void SetLast(IMapEditRecord record)
    {
        mLastRecord = record;
    }

    public IMapEditRecord GetLast()
    {
        return mLastRecord;
    }

    public void SetNext(IMapEditRecord record)  
    {
        mLastRecord = record;
    }

    public IMapEditRecord GetNext()
    {
        return mNextRecord;
    }
}
