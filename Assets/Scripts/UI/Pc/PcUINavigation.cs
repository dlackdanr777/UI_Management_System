using Muks.UI;
using System.Collections.Generic;
using UnityEngine;


namespace Muks.PcUI
{
    public class PcUINavigation : UINavigation
    {

        [Header("Views")]
        [Tooltip("�ֻ��� lootUIView")]
        [SerializeField] private PcViewDicStruct _rootUiView;

        [Tooltip("�̰����� ������ UIView")]
        [SerializeField] private PcViewDicStruct[] _uiViews;

        public override int Count => _activeViewList.Count;
        private bool _isViewsInactive;
        public override bool IsViewsInactive => _isViewsInactive;

        /// <summary> ViewDicStruct���� ������ Name�� Key��, UIView�� ������ �����س��� ��ųʸ� </summary>
        private Dictionary<string, PcUIView> _viewDic = new Dictionary<string, PcUIView>();
        private LinkedList<PcUIView> _activeViewList = new LinkedList<PcUIView>();



        private void Start()
        {
            Init();
        }


        private void Init()
        {
            _viewDic.Clear();
            _rootUiView.UIView?.ViewInit(this);

            //uiViewList�� ����� ���� ��ųʸ��� ����
            for (int i = 0, count = _uiViews.Length; i < count; i++)
            {
                string name = _uiViews[i].Name;
                PcUIView uiView = _uiViews[i].UIView;
                _viewDic.Add(name, uiView);

                uiView.ViewInit(this);

                //UI View�� OnPointerDown()����� ����� ��� ����
                uiView.OnFocus += () =>
                {

                    //��ũ�� ����Ʈ���� �ش� UIView�� ���� �� �Ǿ����� ��ġ
                    //SetAsLastSibling()���� ��ܿ� UI�� ���� �� �ֵ��� ��
                    _activeViewList.Remove(uiView);
                    _activeViewList.AddFirst(uiView);
                    uiView.transform.SetAsLastSibling();
                    OnFocusHandler?.Invoke();
                };
            }
        }


        /// <summary>�̸��� �޾� �ش��ϴ� UIView�� �����ִ� �Լ�</summary>
        public override void Push(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
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
                OnFocusHandler?.Invoke();
            }
        }


        /// <summary>��Ŀ������ UI�� �ݴ� �Լ�</summary>
        public override void Pop()
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_activeViewList.First == null)
                return;

            _activeViewList.First.Value.Hide();
            _activeViewList.RemoveFirst();
            OnFocusHandler?.Invoke();
        }


        /// <summary> viewName�� Ȯ���� �ش� UI�� �ݴ� �Լ�</summary>
        public override void Pop(string viewName)
        {
            //�ִϸ��̼��� �������� View�� ������ Push, Pop�� ���´�.
            if (!ViewsVisibleStateCheck())
                return;

            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                if (!_activeViewList.Contains(uiView))
                    return;

                _activeViewList.Remove(uiView);
                uiView.Hide();
                OnFocusHandler?.Invoke();
            }
        }


        /// <summary> ������ ��� UIView�� SetActive(true)�Ѵ�. </summary>
        public override void AllShow()
        {
            _rootUiView.UIView.gameObject.SetActive(true);

            foreach (PcUIView view in _activeViewList)
            {
                view.gameObject.SetActive(true);
            }

            _isViewsInactive = false;
            OnFocusHandler?.Invoke();
        }


        /// <summary> �ѳ��� ��� UIView�� SetActive(false)�Ѵ�. </summary>
        public override void AllHide()
        {
            _rootUiView.UIView.gameObject.SetActive(false);

            foreach (PcUIView view in _activeViewList)
            {
                view.gameObject.SetActive(false);
            }

            _isViewsInactive = true;
            OnFocusHandler?.Invoke();
        }


        public override VisibleState GetVisibleState(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView view))
            {
                return view.VisibleState;
            }

            Debug.LogErrorFormat("{0}�� �����Ǵ� UIView�� �������� �ʽ��ϴ�.", viewName);
            return default;
        }


        /// <summary>�Ű� ������ �ش��ϴ� UIView Class�� Ȱ��ȭ�� ���¸� ��, �ƴϸ� ������ ��ȯ�ϴ� �Լ�</summary>
        public override bool CheckActiveView(string viewName)
        {
            if (_viewDic.TryGetValue(viewName, out PcUIView uiView))
            {
                if (_activeViewList.Contains(uiView))
                    return true;
            }

            Debug.LogErrorFormat("{0}�� �ش��ϴ� UIView�� �������� �ʽ��ϴ�.");
            return false;
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

