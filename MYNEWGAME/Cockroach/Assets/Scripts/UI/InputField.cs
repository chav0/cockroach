using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputField : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Vector2 Angle; 

    public void OnDrag(PointerEventData data)
    {
        if (data.dragging)
        {
            SetAngle(data.position);
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        SetAngle(data.position);
    }

    private void SetAngle(Vector2 position)
    {        
        var vector = position - new Vector2(Screen.width, Screen.height) / 2f;
        Angle = vector.normalized; 
    }
}
