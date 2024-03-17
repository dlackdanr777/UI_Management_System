using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muks.PcUI;


[RequireComponent(typeof(PcUINavigation))]

public class PcUIMain : MonoBehaviour
{

    private PcUINavigation _uiNav;


    private void Awake()
    {
        _uiNav = GetComponent<PcUINavigation>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            VisibleState viewState = _uiNav.GetVisibleStateByViewName("TestUI1");
            if (viewState == VisibleState.Disappeared)
                _uiNav.Show("TestUI1");

            else if(viewState == VisibleState.Appeared)
                _uiNav.Pop("TestUI1");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            VisibleState viewState = _uiNav.GetVisibleStateByViewName("TestUI2");
            if (viewState == VisibleState.Disappeared)
                _uiNav.Show("TestUI2");

            else if (viewState == VisibleState.Appeared)
                _uiNav.Pop("TestUI2");
        }
    }
}
