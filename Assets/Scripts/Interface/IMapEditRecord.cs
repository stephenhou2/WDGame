using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapEditRecord
{
    void Recover();

    void SetLast(IMapEditRecord record);
    IMapEditRecord GetLast();
    void SetNext(IMapEditRecord record);
    IMapEditRecord GetNext();

}
