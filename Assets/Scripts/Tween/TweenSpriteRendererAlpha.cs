using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Muks.Tween
{
    public class TweenSpriteRendererAlpha : TweenData
    {
        /// <summary> ��ǥ ��ġ </summary>
        public Color TargetColor;

        /// <summary> ���� ��ġ</summary>
        public Color StartColor;

        public float TargetAlpha;

        public SpriteRenderer SpriteRenderer;


        public override void SetData(DataSequence dataSequence)
        {
            base.SetData(dataSequence);
            if(TryGetComponent(out SpriteRenderer))
            {
                TargetAlpha = (float)dataSequence.TargetValue;
                StartColor = SpriteRenderer.color;
                TargetColor = SpriteRenderer.color;
                TargetColor.a = TargetAlpha;
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
            
            SpriteRenderer.color = Color.LerpUnclamped(StartColor, TargetColor, percent);
        }


        protected override void TweenCompleted()
        {
            if (TweenMode != TweenMode.Spike)
                SpriteRenderer.color = TargetColor;
        }
    }
}

