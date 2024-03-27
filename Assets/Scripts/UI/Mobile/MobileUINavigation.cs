using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Muks.MobileUI
{

    public class MobileUINavigation : MonoBehaviour
    {
        [Header("Views")]
        [Tooltip("�ֻ��� lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("�� Ŭ�������� ������ UIViews")]
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


        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� �����ϸ� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
        public bool Check(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (_uiViewList.Contains(uiView))
                    return true;
            }
            return false;
        }


        /// <summary>�̸��� �޾� ���� �̸��� view�� �����ִ� �Լ�</summary>
        public void Push(string viewName)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
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

            Debug.LogError("��ųʸ��� �ش� �̸��� ���� UIViewŬ������ �����ϴ�.");
        }


        /// <summary>View Class�� �޾� Veiw�� �����ִ� �Լ�</summary>
        public void Push(MobileUIView uiView)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
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

            Debug.LogError("��ųʸ��� �ش� �̸��� ���� UIViewŬ������ �����ϴ�.");
        }


        /// <summary>���� ui ���� ���ȴ� ui�� �ҷ����� �Լ�</summary> 
        public void Pop()
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_uiViewList.Count <= 0)
            {
                Debug.LogError("���� �ִ� UI�� �����ϴ�.");
                return;
            }

            MobileUIView selectView = _uiViewList[Count - 1];
            selectView.Hide();
            _uiViewList.RemoveAt(Count - 1);

            if (1 <= _uiViewList.Count)
                _uiViewList.Last().RectTransform.SetAsLastSibling();
        }


        /// <summary> viewName�� Ȯ���� �ش� UI �� ���ߴ� �Լ�</summary>
        public void Pop(string viewName)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_uiViewList.Count <= 0)
                return;

            MobileUIView view = _uiViewList.Find(x => x == _viewDic[viewName]);
            if (view == null)
            {
                Debug.LogError("�ش� uiView�� �������� �ʽ��ϴ�.");
                return;
            }

            view.Hide();
            _uiViewList.Remove(view);
        }



        /// <summary> view�� �Ű� ������ �޾� �ش� UI�� �ݴ� �Լ�</summary>
        public void Pop(MobileUIView uiView)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_uiViewList.Count <= 0)
                return;

            MobileUIView view = _uiViewList.Find(x => x == uiView);
            if (view == null)
            {
                Debug.LogError("�ش� uiView�� �������� �ʽ��ϴ�.");
                return;
            }

            _uiViewList.Remove(uiView);
            uiView.Hide();
        }


        /// <summary>�� ó�� ���ȴ� ui�� �̵��ϴ� �Լ�</summary>
        public void Clear()
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            while (_uiViewList.Count > 0)
            {
                _uiViewList.Last().Hide();
                _uiViewList.Remove(_uiViewList.Last());
            }
        }


        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _uiViewList)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> �ѳ��� ��� UIView�� SetActive(false)�Ѵ�. </summary>
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


        /// <summary>�����ִ� UI�� VisibleState�� Ȯ�� �� bool���� �����ϴ� �Լ�</summary>
        private bool ViewsVisibleStateCheck()
        {
            foreach (MobileUIView view in _viewDic.Values)
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
