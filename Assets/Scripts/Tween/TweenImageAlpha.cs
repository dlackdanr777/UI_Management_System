using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Muks.Tween
{
    public class TweenImageAlpha : TweenData
    {
        /// <summary> ��ǥ ��ġ </summary>
        public Color TargetColor;

        /// <summary> ���� ��ġ</summary>
        public Color StartColor;

        public float TargetAlpha;

        public Image Image;

        public override void SetData(DataSequence dataSequence)
        {
            base.SetData(dataSequence);
            if(TryGetComponent(out Image))
            {
                TargetAlpha = (float)dataSequence.TargetValue;
                StartColor = Image.color;
                TargetColor = Image.color;
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

            Image.color = Color.LerpUnclamped(StartColor, TargetColor, percent);
        }

        protected override void TweenCompleted()
        {
            if(TweenMode != TweenMode.Spike)
                Image.color = TargetColor;
        }
    }
}

