using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Muks.PcUI;
using Muks.Tween;


[RequireComponent(typeof(CanvasGroup))]
public class TestUI1 : PcUIView
{
    [SerializeField] private Button _exitButton;


    private CanvasGroup _canvasGroup;

    private Vector3 _startScale => new Vector3(0.5f, 0.5f, 0.5f);


    public override void Init(PcUINavigation uiNav)
    {
        base.Init(uiNav);
        _canvasGroup = GetComponent<CanvasGroup>();

        _exitButton.onClick.AddListener(OnExitButtonClicked);
        gameObject.SetActive(false);
    }


    public override void Hide()
    {
        gameObject.SetActive(true);
        VisibleState = VisibleState.Disappearing;
        _canvasGroup.blocksRaycasts = false;
        transform.localScale = Vector3.one;

        Tween.TransformScale(gameObject, _startScale, 0.3f, TweenMode.EaseInBack, () =>
        {
            VisibleState = VisibleState.Disappeared;
            gameObject.SetActive(false);
        });

    }


    public override void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.blocksRaycasts = false;
        VisibleState = VisibleState.Appearing;
        transform.localScale = _startScale;

        Vector3 targetScale = Vector3.one;
        Tween.TransformScale(gameObject, targetScale, 0.3f, TweenMode.EaseOutBack, () =>
        {
            VisibleState = VisibleState.Appeared;
            _canvasGroup.blocksRaycasts = true;
        });
    }


    private void OnExitButtonClicked()
    {
        _uiNav.Pop(this);
    }
}
