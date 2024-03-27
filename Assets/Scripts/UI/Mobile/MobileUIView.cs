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
        /// 이것이 Appeared일때 Hide실행 가능,
        /// 이것이 Disappeared일때 Show실행 가능
        /// </summary>
        public VisibleState VisibleState;


        /// <summary>UI를 불러낼때 실행되는 함수</summary>
        public abstract void Show();


        /// <summary>UI를 끌때 실행되는 함수</summary>
        public abstract void Hide();
    }
}


