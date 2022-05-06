using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragMove : MonoBehaviour, IDragHandler
{
    public Vector2 Scaler = Vector2.one;
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            this.transform.localPosition += new Vector3(eventData.delta.x * Scaler.x, eventData.delta.y * Scaler.y, 0);
        }
    }
}
