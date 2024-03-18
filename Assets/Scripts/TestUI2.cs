using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Muks.PcUI;


public class TestUI2 : PcUIView
{
    [SerializeField] private Button _exitButton;

    public override void Init(PcUINavigation uiNav)
    {
        base.Init(uiNav);
        _exitButton.onClick.AddListener(OnExitButtonClicked);

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


    private void OnExitButtonClicked()
    {
        _uiNav.Pop(this);
    }
}
