using System;
using UnityEngine;

namespace Muks.UI
{
    [Serializable]
    public class NavigationData
    {
        /// <summary>Ư�� ��Ȳ���� UI�� �� ������ ������ ���ΰ� ���� </summary>
        [SerializeField] private bool _focusEnabled = true;
        public bool FocusEnabled => _focusEnabled;

        [SerializeField] private UINavigation _uiNav;
        public UINavigation UiNav => _uiNav;

    }
}
