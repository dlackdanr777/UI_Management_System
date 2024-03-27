using Muks.MobileUI;
using Muks.PcUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(MobileUINavigation))]
public class MobileUIMain : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _allShowButton;
    [SerializeField] private Image _cursorImage;


    private MobileUINavigation _uiNav;


    private void Awake()
    {
        _uiNav = GetComponent<MobileUINavigation>();
        _allShowButton.onClick.AddListener(OnAllShowButtonClicked);
    }


    private void Update()
    {
        _cursorImage.transform.position = Input.mousePosition;

        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if( 0 < _uiNav.Count)
        {
            _uiNav.Pop();
        }
        else
        {
            _uiNav.Push("ExitUI");
        }
    }


    private void OnAllShowButtonClicked()
    {
        _uiNav.AllShow();
    }
}
