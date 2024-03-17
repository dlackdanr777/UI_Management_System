using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Muks.PcUI
{
    public abstract class PcUIView : MonoBehaviour, IPointerDownHandler
    {
        ///  <summary> Appeared, Disappeared일때 Show(), Hide()실행 가능</summary>
        public VisibleState VisibleState;

        /// <summary> UI창을 클릭했을때 실행될 대리자 </summary>
        public Action OnFocus;

        protected PcUINavigation _uiNav;

        protected RectTransform _rectTransform;


        public virtual void Init(PcUINavigation uiNav)
        {
            _uiNav = uiNav;
            _rectTransform = GetComponent<RectTransform>();
        }


        /// <summary>UI를 불러낼때 실행되는 함수</summary>
        public abstract void Show();


        /// <summary>UI를 끌때 실행되는 함수</summary>
        public abstract void Hide();


        /// <summary>해당 UI 창을 클릭하면 실행될 함수</summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            OnFocus?.Invoke();
        }
    }
}

