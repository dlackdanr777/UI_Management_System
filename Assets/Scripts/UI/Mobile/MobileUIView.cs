using UnityEngine;


namespace Muks.MobileUI
{
    public abstract class MobileUIView : MonoBehaviour
    {
        protected MobileUINavigation _uiNav;

        [HideInInspector] public RectTransform RectTransform;

        public virtual void Init(MobileUINavigation uiNav)
        {
            _uiNav = uiNav;
            RectTransform = GetComponent<RectTransform>();
        }


        /// <summary>
        /// �̰��� Appeared�϶� Hide���� ����,
        /// �̰��� Disappeared�϶� Show���� ����
        /// </summary>
        public VisibleState VisibleState;


        /// <summary>UI�� �ҷ����� ����Ǵ� �Լ�</summary>
        public abstract void Show();


        /// <summary>UI�� ���� ����Ǵ� �Լ�</summary>
        public abstract void Hide();
    }
}


