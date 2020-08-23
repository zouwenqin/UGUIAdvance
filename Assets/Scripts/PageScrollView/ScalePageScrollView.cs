using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePageScrollView : PageScrollView
{
    #region 字段

    //所有页的Object
    protected GameObject[] items;

    public float CurrentScale = 1f;
    public float otherScale = 0.6f;

    public int lastPage;
    public int nextPage;
    #endregion

    #region Unity回调
    protected override void Start()
    {
        base.Start();
        items = new GameObject[pageCount];
        //初始化所有的GameObject
        for (int i = 0; i < pageCount; i++)
        {
            items[i] = transform.Find("Viewport/Content").GetChild(i).gameObject;
        }
    }

    protected override void Update()
    {
        base.Update();
        ListenrScale();
    }
    #endregion

    //监听scale
    public void ListenrScale()
    {
        //找到上一页和下一页
        for (int i = 0; i < pages.Length; i++)
        {
            if(pages[i]  < rect.horizontalNormalizedPosition)
            {
                lastPage = i;
                break;
            }
        }

        for (int i = 0; i < pages.Length; i++)
        {
            if(pages[i] > rect.horizontalNormalizedPosition)
            {
                nextPage = i;
                break;
            }
        }

        if(nextPage == lastPage)
        {
            return;
        }

        float percent = (rect.horizontalNormalizedPosition - pages[lastPage]) / (pages[nextPage] - pages[lastPage]);
        items[lastPage].transform.localScale = Vector3.Slerp(Vector3.one * CurrentScale, Vector3.one * otherScale, percent);
        items[nextPage].transform.localScale = Vector3.Slerp(Vector3.one * CurrentScale, Vector3.one * otherScale, 1 - percent);

        for (int i = 0; i < items.Length; i++)
        {
            if(i != lastPage && i != nextPage)
            {
                items[i].transform.localScale = Vector3.one * otherScale;
            }
        }

    }
}
