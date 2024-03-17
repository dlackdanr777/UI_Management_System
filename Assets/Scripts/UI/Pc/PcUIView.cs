using System;
using UnityEngine;

namespace Muks.PcUI
{

    public enum VisibleState
    {
        Disappeared, // 사라짐
        Disappearing, //사라지는 중
        Appeared, //나타남
        Appearing, //나타나는중
    }


    public abstract class PcUIView : MonoBehaviour
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
    }
}

