using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Muks.UI;


namespace Muks.MobileUI
{

    public class MobileUINavigation : UINavigation
    {

        [Header("Views")]
        [Tooltip("�ֻ��� lootUIView")]
        [SerializeField] private MobileViewDicStruct _rootUiView;

        [Tooltip("�� Ŭ�������� ������ UIViews")]
        [SerializeField] private MobileViewDicStruct[] _uiViews;

        public override int Count => _activeViewList.Count;

        private bool _isViewsInactive;
        public override bool IsViewsInactive => _isViewsInactive;

        private List<MobileUIView> _activeViewList = new List<MobileUIView>();
        private Dictionary<string, MobileUIView> _viewDic = new Dictionary<string, MobileUIView>();



        private void Start()
        {
            Init();
        }


        private void Init()
        {
            _viewDic.Clear();
            _rootUiView.UIView?.ViewInit(this);

            for (int i = 0, count = _uiViews.Length; i < count; i++)
            {
                string name = _uiViews[i].Name;
                MobileUIView uiView = _uiViews[i].UIView;
                _viewDic.Add(name, uiView);
                uiView.ViewInit(this);
            }
        }


        /// <summary>�̸��� �޾� ���� �̸��� view�� �����ִ� �Լ�</summary>
        public override void Push(string viewName)
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
                OnFocusHandler?.Invoke();
                return;
            }

            Debug.LogError("��ųʸ��� �ش� �̸��� ���� UIViewŬ������ �����ϴ�.");
        }



        /// <summary>���� ui ���� ���ȴ� ui�� �ҷ����� �Լ�</summary> 
        public override void Pop()
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
            OnFocusHandler?.Invoke();

            if (1 <= _activeViewList.Count)
                _activeViewList.Last().transform.SetAsLastSibling();
        }


        /// <summary> viewName�� Ȯ���� �ش� UI �� ���ߴ� �Լ�</summary>
        public override void Pop(string viewName)
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
            OnFocusHandler?.Invoke();
        }



        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public override void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (MobileUIView view in _activeViewList)
            {
                view.gameObject.SetActive(true);
            }

            _isViewsInactive = false;
            OnFocusHandler?.Invoke();
        }


        public override void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (MobileUIView view in _activeViewList)
            {
                view.gameObject.SetActive(false);
            }

            _isViewsInactive = true;
            OnFocusHandler?.Invoke();
        }



        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� Ȱ��ȭ�� ���¸� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
        public override bool CheckActiveView(string viewName)
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


        public override VisibleState GetVisibleState(string viewName)
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
