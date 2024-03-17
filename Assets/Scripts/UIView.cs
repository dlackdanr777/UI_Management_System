using System;
using UnityEngine;

namespace Muks.PcUI
{

    public enum VisibleState
    {
        Disappeared, // �����
        Disappearing, //������� ��
        Appeared, //��Ÿ��
        Appearing, //��Ÿ������
    }


    public abstract class UIView : MonoBehaviour
    {
        ///  <summary> Appeared, Disappeared�϶� Show(), Hide()���� ����</summary>
        public VisibleState VisibleState;

        /// <summary> UIâ�� Ŭ�������� ����� �븮�� </summary>
        public Action OnFocus;

        protected UINavigation _uiNav;

        protected RectTransform _rectTransform;


        public virtual void Init(UINavigation uiNav)
        {
            _uiNav = uiNav;
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>UI�� �ҷ����� ����Ǵ� �Լ�</summary>
        public abstract void Show();


        /// <summary>UI�� ���� ����Ǵ� �Լ�</summary>
        public abstract void Hide();
    }
}

