using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Muks.MobileUI;


public class MobileRootUI : MobileUIView
{
    [Header("Components")]
    [SerializeField] private Button _testUI1Button;
    [SerializeField] private Button _testUI2Button;
    [SerializeField] private Button _testUI3Button;
    [SerializeField] private Button _allHideBUtton;


    public override void Init(MobileUINavigation uiNav)
    {
        base.Init(uiNav);
        _testUI1Button.onClick.AddListener(OnTestUI1ButtonClicked);
        _testUI2Button.onClick.AddListener(OnTestUI2ButtonClicked);
        _testUI3Button.onClick.AddListener(OnTestUI3ButtonClicked);
        _allHideBUtton.onClick.AddListener(OnAllHideButtonClicked);
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


    private void OnTestUI1ButtonClicked()
    {
        _uiNav.Push("TestUI1");
    }


    private void OnTestUI2ButtonClicked()
    {
        _uiNav.Push("TestUI2");
    }


    private void OnTestUI3ButtonClicked()
    {
        _uiNav.Push("TestUI3");
    }


    private void OnAllHideButtonClicked()
    {
        _uiNav.AllHide();
    }
}
