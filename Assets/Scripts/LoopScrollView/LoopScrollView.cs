using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollView : MonoBehaviour
{
    #region 字段
    //子物体的预制体
    public GameObject childItemPrefab;

    private GridLayoutGroup contentLayoutGroup;
    private ContentSizeFitter sizeFitter;

    private RectTransform content;

    #endregion

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        contentLayoutGroup.enabled = true;
        sizeFitter.enabled = true;
        //添加一个子节点
        OnAddHead();
        //禁用
        Invoke("EnableFalseGrid", 0.1f);

    }
    public void Init()
    {
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        //初始化
        contentLayoutGroup = transform.Find("Viewport/Content").GetComponentInChildren<GridLayoutGroup>();
        sizeFitter = transform.Find("Viewport/Content").GetComponentInChildren<ContentSizeFitter>();
    }

    public GameObject GetChildItem()
    {
        //查找有没有被回收的子节点
        for (int i = 0; i < content.childCount; i++)
        {
            if(!content.GetChild(i).gameObject.activeSelf)
            {
                content.GetChild(i).gameObject.SetActive(true);
                return content.GetChild(i).gameObject;
            }
        }

        //如果没有，创建一个
        GameObject childItem = GameObject.Instantiate(childItemPrefab, content.transform);

        //设置数据
        childItem.transform.localScale = Vector3.one;
        childItem.transform.localPosition = Vector3.zero;

        //设置锚点
        childItem.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        childItem.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        //设置宽高
        childItem.GetComponent<RectTransform>().sizeDelta = contentLayoutGroup.cellSize;

        LoopItem loopItem = childItem.AddComponent<LoopItem>();
        loopItem.onAddHead += this.OnAddHead;
        loopItem.onRemoveHead += this.onRemoveHead;
        loopItem.onAddLast += this.onAddLast;
        loopItem.onRemoneLast += this.onRemoveLast;
        return childItem;
    }

    
    //在上面添加一个物体
    public void OnAddHead()
    {
        Transform first =  FindFirst();
        GameObject obj = GetChildItem();
        obj.transform.SetAsFirstSibling();
        //动态地设置位置
        if (first != null)
        {
            obj.transform.localPosition = first.localPosition + new Vector3(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y, 0);
        }
    }

    //移除当前最上面的物体
    public void onRemoveHead()
    {
        Transform first = FindFirst();
        if(first != null)
        {
            first.gameObject.SetActive(false);
        }
    }

    public void onAddLast()
    {
        Transform last = FindFirst();
        GameObject obj = GetChildItem();
        obj.transform.SetAsLastSibling();
        //动态地设置位置
        if (last != null)
        {
            obj.transform.localPosition = last.localPosition - new Vector3(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y, 0);
        }

        //高度要不要增加
        if (IsNeedAddContentHeight(obj.transform))
        {
            //对高度进行增加
            content.sizeDelta += new Vector2(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y);
        }
    }

    public void onRemoveLast()
    {
        Transform last = FindLast();
        if (last != null)
        {
            last.gameObject.SetActive(false);
        }
    }

    public Transform FindFirst()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    public Transform FindLast()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            if (content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    public void EnableFalseGrid()
    {
        contentLayoutGroup.enabled = false;
        sizeFitter.enabled = false;
    }

    //判断是不是需要增加Content的高度
    public bool IsNeedAddContentHeight(Transform trans)
    {
        Vector3[] rectCorners = new Vector3[4];
        Vector3[] contentCorners = new Vector3[4];
        trans.GetComponent<RectTransform>().GetWorldCorners(rectCorners);
        content.GetWorldCorners(contentCorners);

        if(rectCorners[0].y < contentCorners[0].y)
        {
            return true;
        }
        return false;
    }
}
