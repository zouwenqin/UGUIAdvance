using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : ViewBase
{
    public GonggaoPanel gonggaoPanel;
    public FriendsPanel FriendsPanel;
    public TaskView taskView;
    public BagPanel bagPanel;

    public void OnBtnGonggaoClick()
    {
        gonggaoPanel.Show();
    }

    public void OnBtnfriendsClick()
    {
        FriendsPanel.Show();
    }

    public void OnBtnTaskClick()
    {
        taskView.Show();
    }

    public void OnBtnBagPanelClick()
    {
        bagPanel.Show();
        this.Hide();
    }
    public override void Hide()
    {
        //base.Hide();
        transform.GetComponent<Animator>().SetBool("isShow", false);
    }

    public override void Show()
    {
        // base.Show();
        transform.GetComponent<Animator>().SetBool("isShow", true);
    }
}
