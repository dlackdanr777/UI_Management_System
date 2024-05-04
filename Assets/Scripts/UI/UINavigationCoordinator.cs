using System.Collections.Generic;
using UnityEngine;


namespace Muks.UI
{
    /// <summary> UI Navigation.cs���� �����ϴ� Ŭ���� (��� UINav ���� ��밡��)</summary>
    public class UINavigationCoordinator : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private NavigationData[] _navDatas;

        private LinkedList<NavigationData> _navList = new LinkedList<NavigationData>();


        private void Start()
        {
            Init();
        }


        private void Init()
        {
            for(int i = 0, count = _navDatas.Length; i < count; i++)
            {
                int index = i;
                _navDatas[index].UiNav.OnFocusHandler += () => OnFocusEvent(_navDatas[index]);
                _navList.AddLast(_navDatas[index]);
            }
        }


        /// <summary>������ UI ���� Ŭ������ �켱������ �ֻ����� �δ� �Լ� </summary>
        private void OnFocusEvent(NavigationData navData)
        {
            _navList.Remove(navData);
            _navList.AddLast(navData);

            if (!navData.FocusEnabled)
                return;

            navData.UiNav.transform.SetAsLastSibling();
        }


        /// <summary> UI Navigation�� �켱���� ��� UI�� �ݴ� �Լ� </summary>
        public bool Pop()
        {
            NavigationData navData = _navList.Last.Value;
            UINavigation uiNav = navData.UiNav;

            //���� �� ���� UI ���� Ŭ������ UI�� ���� ���� �ʴٸ�?
            if(uiNav.Count <= 0)
            {
                //�ڷᱸ���� ��ȸ
                for(int i = 0, count = _navDatas.Length - 1; i < count; i++)
                {
                    //������ �ڷḦ �� ó������ �д�.
                    _navList.Remove(navData);
                    _navList.AddFirst(navData);

                    //������ �ڷḦ �޾ƿ� UI�� ���� �ִ��� Ȯ��. ������ �ݰ� ��, ������ ���� �ڷḦ ��ȸ
                    navData = _navList.Last.Value;
                    uiNav = navData.UiNav;

                    if (uiNav.Count <= 0)
                        continue;

                    uiNav.Pop();
                    return true;
                }

                return false;
            }

            uiNav.Pop();
            return true;
        }


        /// <summary> �������� UI ��ü�� Ȱ��ȭ ��Ű�� �Լ� </summary>
        public void AllShow()
        {
            foreach(NavigationData navData in _navList)
            {
                navData.UiNav.AllShow();
            }
        }


        /// <summary> �������� UI ��ü�� ��Ȱ��ȭ ��Ű�� �Լ� </summary>
        public void AllHide()
        {
            foreach (NavigationData navData in _navList)
            {
                navData.UiNav.AllHide();
            }
        }
    }
}


