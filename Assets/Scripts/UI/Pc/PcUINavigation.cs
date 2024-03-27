using Muks.MobileUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Muks.PcUI
{
    public class PcUINavigation : MonoBehaviour
    {
        [Serializable]
        public struct ViewDicStruct
        {
            [Tooltip("Key")]
            public string Name;

            [Tooltip("Value")]
            public PcUIView UIView;
        }

        [Header("Views")]
        [Tooltip("�ֻ��� lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("�̰����� ������ UIView")]
        [SerializeField] private ViewDicStruct[] _uiViews;

        /// <summary> ViewDicStruct���� ������ Name�� Key��, UIView�� ������ �����س��� ��ųʸ� </summary>
        private Dictionary<string, PcUIView> _viewDic = new Dictionary<string, PcUIView>();

        private LinkedList<PcUIView> _activeViewList = new LinkedList<PcUIView>();

        public int Count => _activeViewList.Count;


        private void Start()
        {
            Init();
        }


        private void Init()
        {
            _viewDic.Clear();
            //uiViewList�� ����� ���� ��ųʸ��� ����
            for (int i = 0, count = _uiViews.Length; i < count; i++)
            {
                string name = _uiViews[i].Name;
                PcUIView uiView = _uiViews[i].UIView;
                _viewDic.Add(name, uiView);

                uiView.Init(this);

                uiView.OnFocus += () =>
                {
                    _activeViewList.Remove(uiView);
                    _activeViewList.AddFirst(uiView);
                    uiView.transform.SetAsLastSibling();
                };
            }
        }


        /// <summary>�̸��� �޾� �ش��ϴ� UIView�� �����ִ� �Լ�</summary>
        public void Push(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
                if (!ViewsVisibleStateCheck())
                    return;

                if (!_activeViewList.Contains(uiView))
                {
                    _activeViewList.AddFirst(uiView);
                    uiView.Show();
                }
                else
                {
                    _activeViewList.Remove(uiView);
                    _activeViewList.AddFirst(uiView);
                }

                uiView.transform.SetAsLastSibling();
            }
        }


        /// <summary>View Class�� �޾� Veiw�� �����ִ� �Լ�</summary>
        public void Push(PcUIView uiView)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            foreach (PcUIView view in _viewDic.Values)
            {
                if (uiView != view)
                    continue;

                if (!_activeViewList.Contains(uiView))
                {
                    _activeViewList.AddLast(uiView);
                    uiView.Show();
                }
                else
                {
                    _activeViewList.Remove(uiView);
                    _activeViewList.AddLast(uiView);
                    uiView.gameObject.SetActive(true);
                }

                uiView.transform.SetAsLastSibling();
                return;
            }

            Debug.LogError("��ųʸ��� �ش� �̸��� ���� UIViewŬ������ �����ϴ�.");
        }



        /// <summary>��Ŀ������ UI�� �ݴ� �Լ�</summary>
        public void Pop()
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.First == null)
                return;

            _activeViewList.First.Value.Hide();
            _activeViewList.RemoveFirst();

            if (_activeViewList.First == null)
                return;

            _activeViewList.First.Value.transform.SetAsLastSibling();
        }


        /// <summary> viewName�� Ȯ���� �ش� UI�� �ݴ� �Լ�</summary>
        public void Pop(string viewName)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                if (!_activeViewList.Contains(uiView))
                    return;

                _activeViewList.Remove(uiView);
                uiView.Hide();
            }
        }



        /// <summary> view�� �Ű� ������ �޾� �ش� UI�� �ݴ� �Լ�</summary>
        public void Pop(PcUIView uiView)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (!_activeViewList.Contains(uiView))
                return;

            foreach (PcUIView view in _viewDic.Values)
            {
                if (uiView != view)
                    continue;

                _activeViewList.Remove(uiView);
                uiView.Hide();
                return;
            }

            Debug.LogError("�ش� uiView�� ���� UI Navigation�� ��ϵ����� �ʽ��ϴ�.");
        }


        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (PcUIView view in _activeViewList)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> �ѳ��� ��� UIView�� SetActive(false)�Ѵ�. </summary>
        public void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (PcUIView view in _activeViewList)
            {
                view.gameObject.SetActive(false);
            }
        }


        public VisibleState GetVisibleStateByViewName(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView view))
            {
                return view.VisibleState;
            }

            Debug.LogErrorFormat("{0}�� �����Ǵ� UIView�� �������� �ʽ��ϴ�.", viewName);
            return default;
        }


        /// <summary>�����ִ� UI�� VisibleState�� Ȯ�� �� bool���� �����ϴ� �Լ�</summary>
        private bool ViewsVisibleStateCheck()
        {
            foreach (PcUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI�� �����ų� ������ �� �Դϴ�.");
                    return false;
                }
            }

            return true;
        }
    }
}

