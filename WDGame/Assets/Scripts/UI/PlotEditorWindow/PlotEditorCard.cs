using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameEngine;

public abstract class PlotEditorCard : UIControl
{
    protected List<int> _prevCards = new List<int>();
    protected List<int> _nextCards = new List<int>();
    public int CardId { get; private set; }
    public bool IsStart { get; private set; }

    public void InitCard(int cardId,int prevCardId,bool isStart)
    {
        CardId = cardId;
        if(prevCardId >= 0)
        {
            _prevCards.Add(prevCardId);
        }

        IsStart = isStart;
    }

    public virtual void CardUpdate(float deltaTime) 
    {

    }
}
