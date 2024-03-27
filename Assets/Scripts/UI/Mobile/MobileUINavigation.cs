using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Muks.MobileUI
{

    public class MobileUINavigation : MonoBehaviour
    {
        [Header("Views")]
        [Tooltip("최상위 lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("이 클래스에서 관리할 UIViews")]
        [SerializeField] private ViewDicStruct[] _uiViews;


        private List<MobileUIView> _uiViewList = new List<MobileUIView>();
        private Dictionary<string, MobileUIView> _viewDic = new Dictionary<string, MobileUIView>();
        private int _hideMainUICount = 0;
        public int Count => _uiViewList.Count;


        private void Start()
        {
            Init();
        }


        private void Init()
        {
            _viewDic.Clear();
            _rootUiView.UIView?.Init(this);

            for (int i = 0, count = _uiViews.Length; i < count; i++)
            {
                string name = _uiViews[i].Name;
                MobileUIView uiView = _uiViews[i].UIView;
                _viewDic.Add(name, uiView);
                uiView.Init(this);
            }
        }


        /// <summary>매개 변수에 해당하는 UIView Class가 존재하면 참, 아니면 거짓을 반환하는 함수</summary>
        public bool Check(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (_uiViewList.Contains(uiView))
                    return true;
            }
            return false;
        }


        /// <summary>이름을 받아 현재 이름의 view를 열어주는 함수</summary>
        public void Push(string viewName)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (!_uiViewList.Contains(uiView))
                {
                    _uiViewList.Add(uiView);
                    uiView.Show();
                }
                else
                {
                    _uiViewList.Remove(uiView);
                    _uiViewList.Add(uiView);
                    uiView.gameObject.SetActive(true);
                }

                uiView.RectTransform.SetAsLastSibling();
                return;
            }

            Debug.LogError("딕셔너리에 해당 이름을 가진 UIView클래스가 없습니다.");
        }


        /// <summary>View Class를 받아 Veiw를 열어주는 함수</summary>
        public void Push(MobileUIView uiView)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            foreach(MobileUIView view in _viewDic.Values)
            {
                if (uiView != view)
                    continue;

                if (!_uiViewList.Contains(uiView))
                {
                    _uiViewList.Add(uiView);
                    uiView.Show();
                }
                else
                {
                    _uiViewList.Remove(uiView);
                    _uiViewList.Add(uiView);
                    uiView.gameObject.SetActive(true);
                }

                uiView.RectTransform.SetAsLastSibling();
                return;
            }

            Debug.LogError("딕셔너리에 해당 이름을 가진 UIView클래스가 없습니다.");
        }


        /// <summary>현재 ui 전에 열렸던 ui를 불러오는 함수</summary> 
        public void Pop()
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_uiViewList.Count <= 0)
            {
                Debug.LogError("열려 있는 UI가 없습니다.");
                return;
            }

            MobileUIView selectView = _uiViewList[Count - 1];
            selectView.Hide();
            _uiViewList.RemoveAt(Count - 1);

            if (1 <= _uiViewList.Count)
                _uiViewList.Last().RectTransform.SetAsLastSibling();
        }


        /// <summary> viewName을 확인해 해당 UI 를 감추는 함수</summary>
        public void Pop(string viewName)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_uiViewList.Count <= 0)
                return;

            MobileUIView view = _uiViewList.Find(x => x == _viewDic[viewName]);
            if (view == null)
            {
                Debug.LogError("해당 uiView가 열려있지 않습니다.");
                return;
            }

            view.Hide();
            _uiViewList.Remove(view);
        }



        /// <summary> view를 매개 변수로 받아 해당 UI를 닫는 함수</summary>
        public void Pop(MobileUIView uiView)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_uiViewList.Count <= 0)
                return;

            MobileUIView view = _uiViewList.Find(x => x == uiView);
            if (view == null)
            {
                Debug.LogError("해당 uiView가 열려있지 않습니다.");
                return;
            }

            _uiViewList.Remove(uiView);
            uiView.Hide();
        }


        /// <summary>맨 처음 열렸던 ui로 이동하는 함수</summary>
        public void Clear()
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            while (_uiViewList.Count > 0)
            {
                _uiViewList.Last().Hide();
                _uiViewList.Remove(_uiViewList.Last());
            }
        }


        /// <summary> 꺼놨던 모든 UIView를 SetActive(true)한다. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _uiViewList)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> 켜놨던 모든 UIView를 SetActive(false)한다. </summary>
        public void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (MobileUIView view in _uiViewList)
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


        /// <summary>열려있는 UI의 VisibleState를 확인 후 bool값을 리턴하는 함수</summary>
        private bool ViewsVisibleStateCheck()
        {
            foreach (MobileUIView view in _viewDic.Values)
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
