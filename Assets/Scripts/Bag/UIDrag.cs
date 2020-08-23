using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler,ICanvasRaycastFilter,IDragHandler
{
    private Vector3 mousePosition;

    private RectTransform rect;

    private bool isDraging = false;

    public Action onStartDrag;
    public Action onDrag;
    public Action onEndDrag;

    private void Awake()
    {
        rect = transform.GetComponent<RectTransform>();
        if(rect == null)
        {
            throw new System.Exception("只能拖拽UI物体");
        }
    }

    private void Update()
    {
        if (isDraging)
        {
            rect.anchoredPosition += (Vector2)(Input.mousePosition - mousePosition);
            mousePosition = Input.mousePosition;
            if (onDrag != null) { onDrag(); }
        }
        if (Input.GetMouseButtonUp(0) && isDraging)
        {
            if (onEndDrag != null) { onEndDrag(); }
            isDraging = false;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePosition = Input.mousePosition;
        if(onStartDrag != null) { onStartDrag(); }
        isDraging = true;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return !isDraging;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
