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
        [Tooltip("최상위 lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("이곳에서 관리할 UIView")]
        [SerializeField] private ViewDicStruct[] _uiViews;

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


        /// <summary>이름을 받아 해당하는 UIView를 열어주는 함수</summary>
        public void Push(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
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


        /// <summary>View Class를 받아 Veiw를 열어주는 함수</summary>
        public void Push(PcUIView uiView)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
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

            Debug.LogError("딕셔너리에 해당 이름을 가진 UIView클래스가 없습니다.");
        }



        /// <summary>포커스중인 UI를 닫는 함수</summary>
        public void Pop()
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
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
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
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



        /// <summary> view를 매개 변수로 받아 해당 UI를 닫는 함수</summary>
        public void Pop(PcUIView uiView)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
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

            Debug.LogError("해당 uiView가 현재 UI Navigation에 등록되있지 않습니다.");
        }


        /// <summary> 꺼놨던 모든 UIView를 SetActive(true)한다. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (PcUIView view in _activeViewList)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> 켜놨던 모든 UIView를 SetActive(false)한다. </summary>
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

