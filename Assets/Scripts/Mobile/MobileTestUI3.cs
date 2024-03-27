using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muks.MobileUI;
using UnityEngine.UI;

public class MobileTestUI3 : MobileUIView
{
    [Header("Components")]
    [SerializeField] private Button _exitButton;


    public override void Init(MobileUINavigation uiNav)
    {
        base.Init(uiNav);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        gameObject.SetActive(false);
    }


    public override void Show()
    {
        VisibleState = VisibleState.Appeared;
        gameObject.SetActive(true);
    }


    public override void Hide()
    {
        VisibleState = VisibleState.Disappeared;
        gameObject.SetActive(false);
    }


    private void OnExitButtonClicked()
    {
        _uiNav.Pop(this);
    }

}
