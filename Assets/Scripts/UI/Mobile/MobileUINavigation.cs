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
        [Tooltip("�ֻ��� lootUIView")]
        [SerializeField] private ViewDicStruct _rootUiView;

        [Tooltip("�� Ŭ�������� ������ UIViews")]
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


        /// <summary>�̸��� �޾� ���� �̸��� view�� �����ִ� �Լ�</summary>
        public void Push(string viewName)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
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

            Debug.LogError("��ųʸ��� �ش� �̸��� ���� UIViewŬ������ �����ϴ�.");
        }


        /// <summary>���� ui ���� ���ȴ� ui�� �ҷ����� �Լ�</summary> 
        public void Pop()
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.Count <= 0)
            {
                Debug.LogError("���� �ִ� UI�� �����ϴ�.");
                return;
            }

            MobileUIView selectView = _activeViewList[Count - 1];
            selectView.Hide();
            _activeViewList.RemoveAt(Count - 1);

            if (1 <= _activeViewList.Count)
                _activeViewList.Last().transform.SetAsLastSibling();
        }


        /// <summary> viewName�� Ȯ���� �ش� UI �� ���ߴ� �Լ�</summary>
        public void Pop(string viewName)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.Count <= 0)
                return;

            MobileUIView view = _activeViewList.Find(x => x == _viewDic[viewName]);
            if (view == null)
            {
                Debug.LogError("�ش� uiView�� �������� �ʽ��ϴ�.");
                return;
            }

            view.Hide();
            _activeViewList.Remove(view);
        }



        /// <summary> view�� �Ű� ������ �޾� �ش� UI�� �ݴ� �Լ�</summary>
        public void Pop(MobileUIView uiView)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.Count <= 0)
                return;

            MobileUIView view = _activeViewList.Find(x => x == uiView);
            if (view == null)
            {
                Debug.LogError("�ش� uiView�� �������� �ʽ��ϴ�.");
                return;
            }

            _activeViewList.Remove(uiView);
            uiView.Hide();
        }


        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _activeViewList)
            {
                view.gameObject.SetActive(true);
            }
        }


        /// <summary> �ѳ��� ��� UIView�� SetActive(false)�Ѵ�. </summary>
        public void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (MobileUIView view in _activeViewList)
            {
                view.gameObject.SetActive(false);
            }
        }


        //RootUI���� ���� ��Ȳ�� �������Ƿ� �ӽ� �ּ�ó��
        //AllHide(), AllShow()�� �� �� �ֱ⿡ ��� ���� ����
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


        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� Ȱ��ȭ�� ���¸� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
        public bool ActiveViewCheck(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out MobileUIView uiView))
            {
                if (_activeViewList.Contains(uiView))
                    return true;
            }

            else
            {
                Debug.LogErrorFormat("{0}�� �ش��ϴ� UIView�� �������� �ʽ��ϴ�.");
                return false;
            }

            return false;
        }


        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� Ȱ��ȭ�� ���¸� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
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

            Debug.LogErrorFormat("{0}�� �����Ǵ� UIView�� �������� �ʽ��ϴ�.", viewName);
            return default;
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
