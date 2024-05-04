using Muks.MobileUI;
using UnityEngine;
using UnityEngine.UI;

public class MobileExitUI : MobileUIView
{
    [Header("Components")]
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;


    public override void Init()
    {
        gameObject.SetActive(false);
        _okButton.onClick.AddListener(OnOkButtonClicked);
        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
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


    private void OnOkButtonClicked()
    {
        Application.Quit();
    }


    private void OnCancelButtonClicked()
    {
        _uiNav.Pop("ExitUI");
    }


}
