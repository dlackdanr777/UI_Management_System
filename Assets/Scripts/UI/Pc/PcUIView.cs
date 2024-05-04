using Muks.UI;
using System;
using UnityEngine.EventSystems;

namespace Muks.PcUI
{
    public abstract class PcUIView : UIView, IPointerDownHandler
    {
        /// <summary> UIâ�� Ŭ�������� ����� �븮�� </summary>
        public Action OnFocus;


        /// <summary>�ش� UI â�� Ŭ���ϸ� ����� �Լ�</summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            OnFocus?.Invoke();
        }
    }
}

