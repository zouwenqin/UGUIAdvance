using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoopItem : MonoBehaviour
{
    private RectTransform rect;
    private RectTransform viewRect;

    private Vector3[] rectCorners;
    private Vector3[] viewCorners;

    #region 事件

    public Action onAddHead;
    public Action onRemoveHead;
    public Action onAddLast;
    public Action onRemoneLast;
    #endregion
    private void Awake()
    {
        rect = transform.GetComponent<RectTransform>();
        viewRect = transform.GetComponentInParent<ScrollRect>().GetComponent<RectTransform>();
        //初始化数组
        rectCorners = new Vector3[4];
        viewCorners = new Vector3[4];

    }

    private void Update()
    {
        
    }

    public void ListenerCorner()
    {
        //获取到自身的边界
        rect.GetWorldCorners(rectCorners);
        //获取显示区域的边界
        viewRect.GetWorldCorners(viewCorners);

        if (IsFirst())
        {
            if (rectCorners[0].y > viewCorners[1].y)
            {
                //把头节点隐藏掉
                if(onRemoveHead != null) { onRemoveHead(); }
            }
            if (rectCorners[1].y < viewCorners[1].y)
            {
                //添加头节点
                if(onAddHead != null) { onAddHead(); }
            }
        }

        //添加尾部
        if (IsLast())
        {
            if (rectCorners[0].y > viewCorners[0].y)
            {
                if(onAddLast != null) { onAddLast(); }
            }
            //回收尾部
            if (rectCorners[1].y < viewCorners[0].y)
            {
                if(onRemoneLast != null) { onRemoneLast(); }
            }
        }
    }

    public bool IsFirst()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                if(transform.parent.GetChild(i) == transform)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }

    public bool IsLast()
    {
        for (int i = transform.parent.childCount - 1; i >= 0; i--)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                if (transform.parent.GetChild(i) == transform)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }
}
