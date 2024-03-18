using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Muks.Tween
{
    public class TweenCanvasGroupAlpha : TweenData
    {
        /// <summary> ���� ��ġ</summary>
        public float StartAlpha;

        public float TargetAlpha;

        public CanvasGroup _canvasGroup;


        public override void SetData(DataSequence dataSequence)
        {
            base.SetData(dataSequence);
            if(TryGetComponent(out _canvasGroup))
            {
                TargetAlpha = (float)dataSequence.TargetValue;
                StartAlpha = _canvasGroup.alpha;
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
            
            _canvasGroup.alpha = Mathf.LerpUnclamped(StartAlpha, TargetAlpha, percent);
        }


        protected override void TweenCompleted()
        {
            if (TweenMode != TweenMode.Spike)
                _canvasGroup.alpha = TargetAlpha;
        }
    }
}

