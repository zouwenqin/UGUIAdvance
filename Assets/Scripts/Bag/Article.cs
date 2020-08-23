using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ArticleType
{
    Weapon,
    Shoes,
    Book,
    Drug
}
public class Article 
{
    public string name;
    public string spritePath;
    public ArticleType articleType;
    public int count;

    public Article(string name ,string spritePath, ArticleType articleType, int count)
    {
        this.name = name;
        this.spritePath = spritePath;
        this.articleType = articleType;
        this.count = count;
    }

    public virtual string GetArticleInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<color=#92FF26>");
        stringBuilder.Append("名称:").Append(this.name);
        stringBuilder.Append("</color>");
        stringBuilder.Append("\n");

        stringBuilder.Append("<color=#FB9400>");
        stringBuilder.Append("类型:").Append(GetTypeName(this.articleType)).Append("\n");
        stringBuilder.Append("</color>");

        stringBuilder.Append("数量:").Append(this.count);

        return stringBuilder.ToString() ;
    }

    public string GetTypeName(ArticleType article)
    {
        switch (articleType)
        {
            case ArticleType.Weapon:
                return "武器";
            case ArticleType.Shoes:
                return "鞋子";
            case ArticleType.Book:
                return "秘籍";
            case ArticleType.Drug:
                return "药品";
            
        }
        return "";
    }
}
