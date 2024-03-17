using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muks.PcUI;

public class TestUI2 : PcUIView
{

    public override void Init(PcUINavigation uiNav)
    {
        base.Init(uiNav);
        gameObject.SetActive(false);
    }


    public override void Hide()
    {
        VisibleState = VisibleState.Disappeared;
        gameObject.SetActive(false);
    }


    public override void Show()
    {
        VisibleState = VisibleState.Appeared;
        gameObject.SetActive(true);
    }
}
