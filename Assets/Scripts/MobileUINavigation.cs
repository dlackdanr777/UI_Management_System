using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Muks.MobileUI
{
    [Serializable]
    public struct ViewDicStruct
    {
        [Tooltip("View클래스의 이름")]
        public string Name;
        public MobileUIView UIView;
    }


    public class MobileUINavigation : MonoBehaviour
    {
        [Header("Views")]
        [Tooltip("최상위 lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("종료 UI")]
        [SerializeField] private ViewDicStruct _exitUiView;

        [Tooltip("이 클래스에서 관리할 UIViews")]
        [SerializeField] private ViewDicStruct[] _uiViewList;


        private List<MobileUIView> _uiViews = new List<MobileUIView>();
        private Dictionary<string, MobileUIView> _viewDic = new Dictionary<string, MobileUIView>();
        private int _hideMainUICount = 0;
        public int Count => _uiViews.Count;


        private void Start()
        {
            Init();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //만약 켜져있는 UI가 있을 경우엔 UI를 끈다
                if (0 < Count)
                {
                    Pop();
                }

                //아닐 경우엔 게임 종료 UI를 띄운다.
                else
                {
                    Push(_exitUiView.Name);
                }
            }
        }


        private void Init()
        {
            _viewDic.Clear();
            _rootUiView.UIView?.Init(this);

            if (_exitUiView.UIView != null)
            {
                _exitUiView.UIView.Init(this);
                _viewDic.Add(_exitUiView.Name, _exitUiView.UIView);
            }

            for (int i = 0, count = _uiViewList.Length; i < count; i++)
            {
                string name = _uiViewList[i].Name;
                MobileUIView uiView = _uiViewList[i].UIView;
                _viewDic.Add(name, uiView);
                uiView.Init(this);
            }
        }


        /// <summary>매개 변수에 해당하는 UIView Class가 존재하면 참, 아니면 거짓을 반환하는 함수</summary>
        public bool Check(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (_uiViews.Contains(uiView))
                    return true;
            }
            return false;
        }


        /// <summary>이름을 받아 현재 이름의 view를 열어주는 함수</summary>
        public void Push(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                foreach (MobileUIView view in _viewDic.Values)
                {
                    if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                    {
                        Debug.Log("UI가 열리거나 닫히는 중 입니다.");
                        return;
                    }
                }

                if (!_uiViews.Contains(uiView))
                {
                    _uiViews.Add(uiView);
                    uiView.Show();
                }
                else
                {
                    _uiViews.Remove(uiView);
                    _uiViews.Add(uiView);
                    uiView.gameObject.SetActive(true);
                }

                uiView.RectTransform.SetAsLastSibling();
            }
            else
            {
                Debug.LogError("딕셔너리에 해당 이름을 가진 UIView클래스가 없습니다.");
            }
        }


        /// <summary>현재 ui 전에 열렸던 ui를 불러오는 함수</summary> 
        public void Pop()
        {

            foreach (MobileUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI가 열리거나 닫히는 중 입니다.");
                    return;
                }
            }

            if (_uiViews.Count <= 0)
                return;

            MobileUIView selectView = _uiViews.Last();
            selectView.Hide();
            _uiViews.RemoveAt(Count - 1);

            if (1 <= _uiViews.Count)
                _uiViews.Last().RectTransform.SetAsLastSibling();

        }


        /// <summary> viewName을 확인해 해당 UI 를 감추는 함수</summary>
        public void Pop(string viewName)
        {
            foreach (MobileUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI가 열리거나 닫히는 중 입니다.");
                    return;
                }
            }

            if (_uiViews.Count <= 0)
                return;

            if (_uiViews.Find(x => x == _viewDic[viewName]) == null)
                return;

            MobileUIView selectView = _uiViews.Find(x => x == _viewDic[viewName]);
            selectView.Hide();
            _uiViews.Remove(selectView);
        }


        /// <summary>맨 처음 열렸던 ui로 이동하는 함수</summary>
        public void Clear()
        {
            foreach (MobileUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI가 열리거나 닫히는 중 입니다.");
                    return;
                }
            }

            while (_uiViews.Count > 0)
            {
                _uiViews.Last().Hide();
                _uiViews.Remove(_uiViews.Last());
            }
        }


        /// <summary> 꺼놨던 모든 UIView를 SetActive(true)한다. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _uiViews)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> 켜놨던 모든 UIView를 SetActive(false)한다. </summary>
        public void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (MobileUIView view in _uiViews)
            {
                view.gameObject.SetActive(false);
            }
        }


        public void HideRootUI()
        {
            _hideMainUICount += 1;
            _rootUiView.UIView.gameObject?.SetActive(false);
        }


        public void ShowRootUI()
        {
            _hideMainUICount = Mathf.Clamp(_hideMainUICount - 1, 0, 1000);

            if (_hideMainUICount == 0)
                _rootUiView.UIView.gameObject?.SetActive(true);
        }


        public MobileUIView GetUIView(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView view))
            {
                return view;
            }

            return view;
        }
    }

}
