
using UnityEngine;


namespace Muks.Tween
{
    /// <summary>
    /// Recttransform�� ��Ŀ�� �������� ��ġ�̵� �ִϸ��̼��� �����ϴ� �Լ�
    /// </summary>
    public class TweenRectTransformAnchoredPosition : TweenData
    {
        /// <summary> ��ǥ ȸ�� �� </summary>
        public Vector3 TargetAnchoredPosition;

        /// <summary> ���� ȸ�� ��</summary>
        public Vector3 StartAnchoredPosition;

        public RectTransform _rectTransform;


        public override void SetData(DataSequence dataSequence)
        {
            base.SetData(dataSequence);

            if (TryGetComponent(out _rectTransform))
            {
                StartAnchoredPosition = _rectTransform.anchoredPosition;
                TargetAnchoredPosition = (Vector2)dataSequence.TargetValue;
            }
            else
            {
                Debug.LogError("�ʿ� ������Ʈ�� �������� �ʽ��ϴ�.");
            }
        }


        protected override void Update()
        {
            base.Update();

            float percent = _percentHandler[TweenMode](ElapsedDuration, TotalDuration);

            _rectTransform.anchoredPosition = Vector3.LerpUnclamped(StartAnchoredPosition, TargetAnchoredPosition, percent);
        }


        protected override void TweenCompleted()
        {
            if (TweenMode != TweenMode.Spike)
                _rectTransform.anchoredPosition = TargetAnchoredPosition;
        }
    }
}
