using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    public GameObject image;
    bool isClick = false;

    private void Start()
    {
        Debug.LogError("初始化了");
        image.GetComponent<Button>().onClick.AddListener(() =>
        {
            isClick = !isClick;
            if (isClick)
            {
                Debug.LogError("点击显示");
                image.GetComponent<Image>().material = Resources.Load<Material>("Shaders/Shader01");
            }
            else
            {
                Debug.LogError("点击隐藏");
                image.GetComponent<Image>().material = null;
            }
        });
    }

    public void OnClick()
    {
        Debug.LogError("点击了图片");
    }
}
