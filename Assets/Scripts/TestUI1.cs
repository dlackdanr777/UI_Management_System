using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muks.PcUI;
using UnityEngine.UI;

public class TestUI1 : PcUIView
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
