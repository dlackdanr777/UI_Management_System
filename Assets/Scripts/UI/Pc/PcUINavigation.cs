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

        [Tooltip("�̰����� ������ UIView")]
        [SerializeField] private ViewDicStruct[] _uiViewList;

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
            for (int i = 0, count = _uiViewList.Length; i < count; i++)
            {
                string name = _uiViewList[i].Name;
                PcUIView uiView = _uiViewList[i].UIView;
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


        /// <summary>��Ŀ������ UI�� �ݴ� �Լ�</summary>
        public void Pop()
        {
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
            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                if (!ViewsVisibleStateCheck())
                    return;

                if (!_activeViewList.Contains(uiView))
                    return;

                _activeViewList.Remove(uiView);
                uiView.Hide();
            }
        }



        /// <summary> view�� �Ű� ������ �޾� �ش� UI�� �ݴ� �Լ�</summary>
        public void Pop(PcUIView uiView)
        {
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
