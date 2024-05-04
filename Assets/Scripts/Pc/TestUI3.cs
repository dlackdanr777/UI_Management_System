using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Muks.PcUI;
using Muks.Tween;


[RequireComponent(typeof(CanvasGroup))]
public class TestUI3 : PcUIView
{
    [Header("Components")]
    [SerializeField] private RectTransform _animationTarget;
    [SerializeField] private Button _exitButton;

    private CanvasGroup _canvasGroup;

    private Vector2 _tmpPos;
    private Vector2 _startPos => _tmpPos + new Vector2(0, 100);
    private Vector2 _endPos => _tmpPos - new Vector2(0, 100);


    public override void Init()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _tmpPos = _animationTarget.anchoredPosition;
        gameObject.SetActive(false);
    }


    public override void Hide()
    {
        gameObject.SetActive(true);
        VisibleState = VisibleState.Disappearing;
        _canvasGroup.blocksRaycasts = false;

        _tmpPos = _animationTarget.anchoredPosition;

        Tween.RectTransfromAnchoredPosition(_animationTarget.gameObject, _endPos, 0.3f, TweenMode.EaseInBack, () =>
        {
            VisibleState = VisibleState.Disappeared;
            gameObject.SetActive(false);
        });
    }


    public override void Show()
    {
        gameObject.SetActive(true);
        VisibleState = VisibleState.Appearing;
        _canvasGroup.blocksRaycasts = false;

        _animationTarget.anchoredPosition = _startPos;

        Tween.RectTransfromAnchoredPosition(_animationTarget.gameObject, _tmpPos, 0.3f, TweenMode.EaseOutBack, () =>
        {
            VisibleState = VisibleState.Appeared;
            _canvasGroup.blocksRaycasts = true;
            gameObject.SetActive(true);
        });
    }


    private void OnExitButtonClicked()
    {
        _uiNav.Pop("TestUI3");
    }
}
