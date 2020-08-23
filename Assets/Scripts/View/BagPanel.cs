using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : ViewBase
{
    #region 数据
    private List<Article> articles = new List<Article>();

    private List<GameObject> articleItems = new List<GameObject>();
    #endregion

    public GameObject articleItemprefab;
    private BagGrid[] bagGrids;
    public MenuPanel menuPanel;

    public BagGrid currentHoverGrid;  //当前所处的格子
    public ArticleItem currentDragArticle; //当前拖拽的物品

    public static BagPanel _instance;

    public ArticleInformation articleInformation;

    #region Unity回调

    private void Awake()
    {
        _instance = this;
        InitArticleData();
        bagGrids = transform.GetComponentsInChildren<BagGrid>();
    }

    private void Start()
    {
        LoadData();
    }
    #endregion

    #region 方法
    public override void Hide()
    {
        base.Hide();
        menuPanel.Show();
    }

    public override void Show()
    {
        //base.Show();
        Invoke("ShowExc", 1);
    }

    public void ShowExc()
    {
        base.Show();
    }

    //初始化物品数据
    public void InitArticleData()
    {
        //武器
        articles.Add(new Article("枪", "Sprite/weapon1", ArticleType.Weapon, 1));
        articles.Add(new Article("刀", "Sprite/weapon2", ArticleType.Weapon, 2));
        articles.Add(new Article("剑", "Sprite/weapon3", ArticleType.Weapon, 3));
        articles.Add(new Article("仙剑", "Sprite/weapon4", ArticleType.Weapon, 4));
        //药品
        articles.Add(new Article("饺子", "Sprite/drug1", ArticleType.Drug, 1));
        articles.Add(new Article("鸡肉", "Sprite/drug2", ArticleType.Drug, 2));
        articles.Add(new Article("药丸", "Sprite/drug3", ArticleType.Drug, 3));
        articles.Add(new Article("仙丹", "Sprite/drug4", ArticleType.Drug, 4));
        //鞋子
        articles.Add(new Article("草鞋", "Sprite/shoe1", ArticleType.Shoes, 1));
        articles.Add(new Article("布鞋", "Sprite/shoe2", ArticleType.Shoes, 2));
        articles.Add(new Article("鞋", "Sprite/shoe3", ArticleType.Shoes, 3));
        articles.Add(new Article("皮鞋", "Sprite/shoe4", ArticleType.Shoes, 4));
        //秘籍
        articles.Add(new Article("降龙十八掌", "Sprite/book1", ArticleType.Book, 1));
        articles.Add(new Article("九阳神功", "Sprite/book2", ArticleType.Book, 2));
        articles.Add(new Article("如来神掌", "Sprite/book3", ArticleType.Book, 3));
        articles.Add(new Article("葵花宝典", "Sprite/book4", ArticleType.Book, 4));
    }

    //加载数据（加载全部数据）
    public void LoadData()
    {
        HideAllArticleItems();
        for (int i = 0; i < articles.Count; i++)
        {
           GetBagGrid().SetArticleItem(LoadArticleItem(articles[i]));

        }
    }

    public void LoadData(ArticleType articleType)
    {
        HideAllArticleItems();
        for (int i = 0; i < articles.Count; i++)
        {
            if (articles[i].articleType == articleType)
            {
               
                GetBagGrid().SetArticleItem( LoadArticleItem(articles[i]));
            }
        }
    }

    //加载一个物品
    public ArticleItem LoadArticleItem(Article article)
    {
        GameObject obj = GetArticleItem();
        ArticleItem articleItem = obj.GetComponent<ArticleItem>();
        articleItem.SetArticle(article);
        return articleItem;
    }

    //获取一个空闲的格子
    public BagGrid GetBagGrid()
    {
        for (int i = 0; i < bagGrids.Length; i++)
        {
            if (bagGrids[i].ArticleItem == null)
            {
                return bagGrids[i];
            }
        }
        return null;
    }

    //获取一个物品
    public GameObject GetArticleItem()
    {
        for (int i = 0; i < articleItems.Count; i++)
        {
            if(articleItems[i].activeSelf == false)
            {
                articleItems[i].SetActive(true);
                return articleItems[i];
            }
        }
        return GameObject.Instantiate(articleItemprefab);
    }

    //清理 隐藏所有的物品
    public void HideAllArticleItems()
    {
        for (int i = 0; i < bagGrids.Length; i++)
        {
            if(bagGrids[i].ArticleItem != null)
            {
                bagGrids[i].ClearGrid();
            }
        }
    }
    #endregion

    #region 点击事件

    public void OnAllToggleValueChange(bool v)
    {
        if (v) { LoadData(); }
    }

    public void OnWeaponToggleValueChange(bool v)
    {
        if (v) { LoadData(ArticleType.Weapon); }
    }

    public void OnShoesToggleValueChange(bool v)
    {
        if (v) { LoadData(ArticleType.Shoes); }
    }

    public void OnBookToggleValueChange(bool v)
    {
        if (v) { LoadData(ArticleType.Book); }
    }

    public void OnDrugToggleValueChange(bool v)
    {
        if (v) { LoadData(ArticleType.Drug); }
    }
    #endregion

}
