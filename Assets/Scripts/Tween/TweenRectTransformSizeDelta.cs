
using UnityEngine;


namespace Muks.Tween
{
    /// <summary>
    /// Recttransform�� width�� height�� �����ϴ� Tween
    /// </summary>
    public class TweenRectTransformSizeDelta : TweenData
    {
        /// <summary> ��ǥ ȸ�� �� </summary>
        public Vector2 TargetSizeDelta;

        /// <summary> ���� ȸ�� ��</summary>
        public Vector2 StartSizeDelta;

        public RectTransform RectTransform;


        public override void SetData(DataSequence dataSequence)
        {
            base.SetData(dataSequence);

            if (TryGetComponent(out RectTransform))
            {
                StartSizeDelta = RectTransform.sizeDelta;
                TargetSizeDelta = (Vector2)dataSequence.TargetValue;
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

            float width = Mathf.LerpUnclamped(StartSizeDelta.x, TargetSizeDelta.x, percent);
            float height = Mathf.LerpUnclamped(StartSizeDelta.y, TargetSizeDelta.y, percent);
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }


        protected override void TweenCompleted()
        {
            if (TweenMode != TweenMode.Spike)
            {
                RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TargetSizeDelta.x);
                RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TargetSizeDelta.y);
            }  
        }
    }
}
