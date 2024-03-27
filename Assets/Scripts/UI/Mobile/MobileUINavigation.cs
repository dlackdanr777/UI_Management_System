using Muks.PcUI;
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


        private List<MobileUIView> _activeViewList = new List<MobileUIView>();
        private Dictionary<string, MobileUIView> _viewDic = new Dictionary<string, MobileUIView>();
        private int _hideMainUICount = 0;
        public int Count => _activeViewList.Count;


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


        /// <summary>이름을 받아 현재 이름의 view를 열어주는 함수</summary>
        public void Push(string viewName)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (!_activeViewList.Contains(uiView))
                {
                    _activeViewList.Add(uiView);
                    uiView.Show();
                }
                else
                {
                    _activeViewList.Remove(uiView);
                    _activeViewList.Add(uiView);
                    uiView.gameObject.SetActive(true);
                }

                uiView.transform.SetAsLastSibling();
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

                if (!_activeViewList.Contains(uiView))
                {
                    _activeViewList.Add(uiView);
                    uiView.Show();
                }
                else
                {
                    _activeViewList.Remove(uiView);
                    _activeViewList.Add(uiView);
                    uiView.gameObject.SetActive(true);
                }

                uiView.transform.SetAsLastSibling();
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

            if (_activeViewList.Count <= 0)
            {
                Debug.LogError("열려 있는 UI가 없습니다.");
                return;
            }

            MobileUIView selectView = _activeViewList[Count - 1];
            selectView.Hide();
            _activeViewList.RemoveAt(Count - 1);

            if (1 <= _activeViewList.Count)
                _activeViewList.Last().transform.SetAsLastSibling();
        }


        /// <summary> viewName을 확인해 해당 UI 를 감추는 함수</summary>
        public void Pop(string viewName)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.Count <= 0)
                return;

            MobileUIView view = _activeViewList.Find(x => x == _viewDic[viewName]);
            if (view == null)
            {
                Debug.LogError("해당 uiView가 열려있지 않습니다.");
                return;
            }

            view.Hide();
            _activeViewList.Remove(view);
        }



        /// <summary> view를 매개 변수로 받아 해당 UI를 닫는 함수</summary>
        public void Pop(MobileUIView uiView)
        {
            //애니메이션이 진행중인 View가 있으면 Push, Pop을 막는다.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.Count <= 0)
                return;

            MobileUIView view = _activeViewList.Find(x => x == uiView);
            if (view == null)
            {
                Debug.LogError("해당 uiView가 열려있지 않습니다.");
                return;
            }

            _activeViewList.Remove(uiView);
            uiView.Hide();
        }


        /// <summary> 꺼놨던 모든 UIView를 SetActive(true)한다. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _activeViewList)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> 켜놨던 모든 UIView를 SetActive(false)한다. </summary>
        public void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (MobileUIView view in _activeViewList)
            {
                view.gameObject.SetActive(false);
            }
        }


        //RootUI만을 끄는 상황이 없었으므로 임시 주석처리
        //AllHide(), AllShow()로 끌 수 있기에 사용 하지 않음
/*
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
*/


        /// <summary>매개 변수에 해당하는 UIView Class가 활성화된 상태면 참, 아니면 거짓을 반환하는 함수</summary>
        public bool ActiveViewCheck(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (_activeViewList.Contains(uiView))
                    return true;
            }

            else
            {
                Debug.LogErrorFormat("{0}에 해당하는 UIView가 존재하지 않습니다.");
                return false;
            }

            return false;
        }


        /// <summary>매개 변수에 해당하는 UIView Class가 활성화된 상태면 참, 아니면 거짓을 반환하는 함수</summary>
        public bool ActiveViewCheck(MobileUIView uiView)
        {
            if (_activeViewList.Contains(uiView))
                return true;

            return false;
        }


        public VisibleState GetVisibleStateByViewName(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView view))
            {
                return view.VisibleState;
            }

            Debug.LogErrorFormat("{0}에 대응되는 UIView가 존재하지 않습니다.", viewName);
            return default;
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
