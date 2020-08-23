using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PageScrollType
{
    Horizontal,
    Vertical
}
public class PageScrollView : MonoBehaviour , IBeginDragHandler, IEndDragHandler
{
    #region 字段
    protected ScrollRect rect;

    protected int pageCount;
    private RectTransform content;
    protected float[] pages;

    public float moveTime = 0.3f;
    private float timer = 0;
    private float startMovePos;
    protected int currentPage = 0;

    private bool isMoving = false;
    private bool isDraging = false;

    //是否开启自动滚动
    public bool isAutoScroll = false;
    public float AutoScrollTime = 2;
    private float AutoScrollTimer = 0;

    public PageScrollType pageScrollType = PageScrollType.Horizontal;

    #endregion

    #region 事件
    public Action<int> onPageChange;
    #endregion

    #region Unity回调
    protected virtual void Start()
    {
        Init();
    }

    
    protected virtual void Update()
    {
        ListenerMove();
        ListenerAutoScroll();
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        //滚动到最近的一页
        this.ScrollToPage(CaculateMinDistancePage());
        isDraging = false;
        AutoScrollTimer = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }
    #endregion

    #region 方法

    public void Init()
    {
        rect = transform.GetComponent<ScrollRect>();
        if (rect == null)
        {
            throw new System.Exception("未查询到ScrollRect");
        }
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        pageCount = content.childCount;
        if(pageCount == 1)
        {
            throw new System.Exception("只有一页，不用进行分页滚动");
        }
        pages = new float[pageCount];
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i] = i * (1 / (float)(pageCount - 1));
        }
    }
    //监听移动
    public void ListenerMove()
    {
        if (isMoving)
        {
            timer += Time.deltaTime * 1 / moveTime; //move Time为0.3，相当于乘以3；
            switch (pageScrollType)
            {
                case PageScrollType.Horizontal:
                    rect.horizontalNormalizedPosition = Mathf.Lerp(startMovePos, pages[currentPage], timer);
                    break;
                case PageScrollType.Vertical:
                    rect.verticalNormalizedPosition = Mathf.Lerp(startMovePos, pages[currentPage], timer);
                    break;
               
            }
           
            if (timer >= 1)
            {
                isMoving = false;
            }
        }
    }

    //监听自动滚动
    public void ListenerAutoScroll()
    {
        if (isDraging)
        {
            return;
        }
        if (isAutoScroll)
        {
            AutoScrollTimer += Time.deltaTime;
            if (AutoScrollTimer >= AutoScrollTime)
            {
                AutoScrollTimer = 0;
                //滚动到下一页
                currentPage++;
                currentPage %= pageCount;
                ScrollToPage(currentPage);
            }
        }
    }

    public void ScrollToPage(int page)
    {
        isMoving = true;
        this.currentPage = page;
        timer = 0;
        switch (pageScrollType)
        {
            case PageScrollType.Horizontal:
                startMovePos = rect.horizontalNormalizedPosition;
                break;
            case PageScrollType.Vertical:
                startMovePos = rect.verticalNormalizedPosition;
                break;
           
        }

        if(onPageChange != null)
        {
            onPageChange(this.currentPage);
        }
    }

    //计算最近的一页
    public int CaculateMinDistancePage()
    {
        int minPage = 0;
        //计算出离得最近的一页
        for (int i = 1; i < pages.Length; i++)
        {
            switch (pageScrollType)
            {
                case PageScrollType.Horizontal:
                    if (Mathf.Abs(pages[i] - rect.horizontalNormalizedPosition) < Mathf.Abs(pages[minPage] - rect.horizontalNormalizedPosition))
                    {
                        minPage = i;
                    }
                    break;
                case PageScrollType.Vertical:
                    if (Mathf.Abs(pages[i] - rect.verticalNormalizedPosition) < Mathf.Abs(pages[minPage] - rect.verticalNormalizedPosition))
                    {
                        minPage = i;
                    }
                    break;
            }
            
        }
        return minPage;
       
       
    }
    #endregion
}
