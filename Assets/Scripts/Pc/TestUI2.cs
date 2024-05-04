using Muks.PcUI;
using Muks.Tween;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
public class TestUI2 : PcUIView
{
    [SerializeField] private Button _exitButton;

    private CanvasGroup _canvasGroup;

    private float _startAlpha => 0.1f;
    private float _targetAlpha => 1f;


    public override void Init()
    {
        _canvasGroup = GetComponent<CanvasGroup>(); 
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        gameObject.SetActive(false);
    }


    public override void Hide()
    {
        gameObject.SetActive(true);
        VisibleState = VisibleState.Disappearing;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = _targetAlpha;

        Tween.CanvasGroupAlpha(gameObject, _startAlpha, 0.3f, TweenMode.Constant, () =>
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
        _canvasGroup.alpha = _startAlpha;

        Tween.CanvasGroupAlpha(gameObject, _targetAlpha, 0.3f, TweenMode.Constant, () =>
        {
            VisibleState = VisibleState.Appeared;
            _canvasGroup.blocksRaycasts = true;
        });
    }


    private void OnExitButtonClicked()
    {
        _uiNav.Pop("TestUI2");
    }
}
