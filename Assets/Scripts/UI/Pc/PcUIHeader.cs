using UnityEngine;
using UnityEngine.EventSystems;


namespace Muks.PcUI
{
    /// <summary>PC UI ��� ����� �ٿ� Target�� �巡���� �� �ְ� ���ִ� Ŭ����</summary>
    public class PcUIHeader : MonoBehaviour, IBeginDragHandler, IDragHandler
    { 
        [Header("Components")]
        [SerializeField] private RectTransform _moveTarget;

        private Vector2 _rectBegin;
        private Vector2 _moveBegin;
        private Vector2 _moveOffset;


        public void OnBeginDrag(PointerEventData eventData)
        {
            _rectBegin = _moveTarget.anchoredPosition;
            _moveBegin = eventData.position;
        }


        public void OnDrag(PointerEventData eventData)
        {
            _moveOffset = eventData.position - _moveBegin;
            _moveTarget.anchoredPosition = _rectBegin + _moveOffset;
        }
    }
}

