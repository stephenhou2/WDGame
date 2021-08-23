using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditRecordManager
{
    private IMapEditRecord mHeadRecord;
    private IMapEditRecord mTailRecord;

    private int mMaxRecordNum;

    public MapEditRecordManager(int maxRecordNum)
    {
        mMaxRecordNum = maxRecordNum;
    }

    public void PushRecord(IMapEditRecord record)
    {

    }

    public IMapEditRecord PopRecord()
    {
        return null;
    }
}
