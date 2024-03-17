using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Muks.MobileUI
{
    [Serializable]
    public struct ViewDicStruct
    {
        [Tooltip("ViewŬ������ �̸�")]
        public string Name;
        public MobileUIView UIView;
    }


    public class MobileUINavigation : MonoBehaviour
    {
        [Header("Views")]
        [Tooltip("�ֻ��� lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("���� UI")]
        [SerializeField] private ViewDicStruct _exitUiView;

        [Tooltip("�� Ŭ�������� ������ UIViews")]
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
                //���� �����ִ� UI�� ���� ��쿣 UI�� ����
                if (0 < Count)
                {
                    Pop();
                }

                //�ƴ� ��쿣 ���� ���� UI�� ����.
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


        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� �����ϸ� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
        public bool Check(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (_uiViews.Contains(uiView))
                    return true;
            }
            return false;
        }


        /// <summary>�̸��� �޾� ���� �̸��� view�� �����ִ� �Լ�</summary>
        public void Push(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                foreach (MobileUIView view in _viewDic.Values)
                {
                    if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                    {
                        Debug.Log("UI�� �����ų� ������ �� �Դϴ�.");
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
                Debug.LogError("��ųʸ��� �ش� �̸��� ���� UIViewŬ������ �����ϴ�.");
            }
        }


        /// <summary>���� ui ���� ���ȴ� ui�� �ҷ����� �Լ�</summary> 
        public void Pop()
        {

            foreach (MobileUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI�� �����ų� ������ �� �Դϴ�.");
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


        /// <summary> viewName�� Ȯ���� �ش� UI �� ���ߴ� �Լ�</summary>
        public void Pop(string viewName)
        {
            foreach (MobileUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI�� �����ų� ������ �� �Դϴ�.");
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


        /// <summary>�� ó�� ���ȴ� ui�� �̵��ϴ� �Լ�</summary>
        public void Clear()
        {
            foreach (MobileUIView view in _viewDic.Values)
            {
                if (view.VisibleState == VisibleState.Disappearing || view.VisibleState == VisibleState.Appearing)
                {
                    Debug.Log("UI�� �����ų� ������ �� �Դϴ�.");
                    return;
                }
            }

            while (_uiViews.Count > 0)
            {
                _uiViews.Last().Hide();
                _uiViews.Remove(_uiViews.Last());
            }
        }


        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _uiViews)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> �ѳ��� ��� UIView�� SetActive(false)�Ѵ�. </summary>
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
