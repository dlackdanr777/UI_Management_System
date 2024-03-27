using UnityEngine;


namespace Muks.MobileUI
{
    public abstract class MobileUIView : MonoBehaviour
    {
        protected MobileUINavigation _uiNav;

        protected RectTransform _rectTransform;

        public virtual void Init(MobileUINavigation uiNav)
        {
            _uiNav = uiNav;
            _rectTransform = GetComponent<RectTransform>();
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


