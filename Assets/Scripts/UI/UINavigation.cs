using System;
using UnityEngine;


namespace Muks.UI
{

    public abstract class UINavigation : MonoBehaviour
    {
        public Action OnFocusHandler;

        public abstract int Count { get; }
        public abstract bool IsViewsInactive{ get; }


        /// <summary>�̸��� �޾� ���� �̸��� view�� �����ִ� �Լ�</summary>
        public abstract void Push(string viewName);

        /// <summary>���� ui ���� ���ȴ� ui�� �ҷ����� �Լ�</summary> 
        public abstract void Pop();

        /// <summary> viewName�� Ȯ���� �ش� UI �� ���ߴ� �Լ�</summary>
        public abstract void Pop(string viewName);

        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public abstract void AllShow();

        /// <summary> �ѳ��� ��� UIView�� SetActive(false)�Ѵ�. </summary>
        public abstract void AllHide();

        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� Ȱ��ȭ�� ���¸� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
        public abstract bool CheckActiveView(string viewName);

        /// <summary> �Ű� ������ �ش��ϴ� UIView Class�� VisibleState���� ��ȯ�ϴ� �Լ� </summary>
        public abstract VisibleState GetVisibleState(string viewName);
    }

}
