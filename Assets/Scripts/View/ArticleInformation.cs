using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleInformation : ViewBase
{
    private RectTransform rectinfo;
    private Text text;
    private void Start()
    {
        rectinfo = transform.Find("info").GetComponent<RectTransform>();
        text = rectinfo.GetComponentInChildren<Text>();
        Hide();
    }
    private void Update()
    {
        rectinfo.anchoredPosition = Input.mousePosition;
    }

    public  void SetShowInfo(string info)
    {
        if (text != null)
        {
            text.text = info;
        }
    }
}
