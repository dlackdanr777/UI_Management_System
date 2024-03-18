
using UnityEngine;

namespace Muks.Tween
{
    public class TweenTransformScale : TweenData
    {

        /// <summary> ���� ȸ�� ��</summary>
        public Vector3 StartScale;

        /// <summary> ��ǥ ȸ�� �� </summary>
        public Vector3 TargetScale;


        public override void SetData(DataSequence dataSequence)
        {
            base.SetData(dataSequence);
            StartScale = transform.localScale;
            TargetScale = (Vector3)dataSequence.TargetValue;
        }


        protected override void Update()
        {
            base.Update();

            float percent = _percentHandler[TweenMode](ElapsedDuration, TotalDuration);

            transform.localScale = Vector3.LerpUnclamped(StartScale, TargetScale, percent);
        }


        protected override void TweenCompleted()
        {
            if (TweenMode != TweenMode.Spike)
                transform.localScale = TargetScale;
        }
    }
}
