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

        [Tooltip("이곳에서 관리할 UIView")]
        [SerializeField] private ViewDicStruct[] _uiViewList;

        /// <summary> ViewDicStruct에서 설정한 Name을 Key로, UIView를 값으로 저장해놓는 딕셔너리 </summary>
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
            //uiViewList에 저장된 값을 딕셔너리에 저장
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


        /// <summary>이름을 받아 해당하는 UIView를 열어주는 함수</summary>
        public void Show(string viewName)
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


        /// <summary>포커스중인 UI를 닫는 함수</summary>
        public void Hide()
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


        /// <summary> viewName을 확인해 해당 UI를 닫는 함수</summary>
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


        public VisibleState GetVisibleStateByViewName(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView view))
            {
                return view.VisibleState;
            }

            Debug.LogErrorFormat("{0}에 대응되는 UIView가 존재하지 않습니다.", viewName);
            return default;
        }


        /// <summary>열려있는 UI의 VisibleState를 확인 후 bool값을 리턴하는 함수</summary>
        private bool ViewsVisibleStateCheck()
        {
            foreach (PcUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI가 열리거나 닫히는 중 입니다.");
                    return false;
                }
            }

            return true;
        }
    }
}

