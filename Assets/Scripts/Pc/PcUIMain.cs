using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muks.PcUI;
using UnityEngine.UI;

[RequireComponent(typeof(PcUINavigation))]

public class PcUIMain : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]private Button _testUI1Button;
    [SerializeField] private Button _testUI2Button;
    [SerializeField] private Button _testUI3Button;
    [SerializeField] private Image _cursorImage;

    private PcUINavigation _uiNav;


    private void Awake()
    {
        _uiNav = GetComponent<PcUINavigation>();
        _testUI1Button.onClick.AddListener(() => ShowAndHideUI("TestUI1"));
        _testUI2Button.onClick.AddListener(() => ShowAndHideUI("TestUI2"));
        _testUI3Button.onClick.AddListener(() => ShowAndHideUI("TestUI3"));
    }


    private void Update()
    {
        _cursorImage.transform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiNav.Pop();
        }

        else if(Input.GetKeyDown(KeyCode.Q))
        {
            ShowAndHideUI("TestUI1");
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            ShowAndHideUI("TestUI2");
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            ShowAndHideUI("TestUI3");
        }
    }


    private void ShowAndHideUI(string viewName)
    {
        VisibleState viewState = _uiNav.GetVisibleStateByViewName(viewName);
        if (viewState == VisibleState.Disappeared)
            _uiNav.Push(viewName);

        else if (viewState == VisibleState.Appeared)
            _uiNav.Pop(viewName);
    }
}
