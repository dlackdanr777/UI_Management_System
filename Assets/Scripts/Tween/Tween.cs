using System;
using System.Collections.Generic;
using UnityEngine;

///<summary> ����ð��� ���� �ӵ��� ��� �޸� ���� ���ΰ�? </summary>
public enum TweenMode
{
    /// <summary>���</summary>
    Constant,

    /// <summary>����</summary>
    Quadratic,

    /// <summary>õõ�� ���� õõ�� ����</summary>
    Smoothstep,

    /// <summary>���� õõ�� ���� ���� õõ�� ����</summary>
    Smootherstep,

    /// <summary>������ ��ġ�� ���ٰ� ���ڸ��� ���ư�</summary>
    Spike,

    /// <summary>�������뿡 ������ �̵�</summary>
    EaseInQuint,

    /// <summary>�ʹݿ� ������ �̵�</summary>
    EaseOutQuint,

    /// <summary>�߰��� ������ �̵�</summary>
    EaseInOutQuint,

    /// <summary>�������뿡 �ſ� ������ �̵�</summary>
    EaseInExpo,

    /// <summary>�ʹݿ� �ſ� ������ �̵�</summary>
    EaseOutExpo,

    /// <summary>�߰��� �ſ� ������ �̵�</summary>
    EaseInOutExpo,

    /// <summary>�������� �ι� ƨ��� ��ġ�� �̵�</summary>
    EaseInElastic,

    /// <summary>������ ��ġ�� ���� ������ ƨ��</summary>
    EaseOutElastic,

    /// <summary>�߰��� ��ġ�� ���� ������ ƨ��</summary>
    EaseInOutElastic,

    /// <summary>�ѹ� �ڷ� ���ٰ� ������ �ϰ� �ش� ������ �̵�</summary>
    EaseInBack,

    /// <summary>��ġ�� ���� �ѹ� ƨ�� �� �ش� ������ �̵�</summary>
    EaseOutBack,

    /// <summary>�������ϰ� ��ġ�� ���� �ѹ� ƨ��</summary>
    EaseInOutBack,

    /// <summary>���� ������ ������ ƨ��� �ش� ������ �̵�</summary>
    EaseInBounce,

    /// <summary>��ǥ �� �̵� �� ��ǥ ������ ������ ƨ��</summary>
    EaseOutBounce,

    /// <summary>���� ������ ������ ƨ�� �� �߰��� ��ǥ �� �̵� �� ��ǥ �������� ������ ƨ��</summary>
    EaseInOutBounce,

    /// <summary>Sin �׷��� �̵�</summary>
    Sinerp,

    /// <summary>Cos �׷��� �̵�</summary>
    Coserp,
}



namespace Muks.Tween
{

    /// <summary>Ʈ�� �ִϸ��̼��� ���� ���� Ŭ����</summary>
    public static class Tween
    {

        /// <summary>�ش� ������Ʈ�� �Ͻ������� ��� Tween ���� �Լ�</summary>
        public static void Play(GameObject gameObject)
        {
            TweenData[] tweens = gameObject.GetComponents<TweenData>();

            foreach (TweenData tween in tweens)
            {
                tween.enabled = true;
            }
        }


        /// <summary>�ش� ������Ʈ�� ��� Tween ���� �Լ�</summary>
        public static void Stop(GameObject gameObject)
        {
            TweenData[] tweens = gameObject.GetComponents<TweenData>();

            foreach(TweenData tween in tweens)
            {
                tween.enabled = false;
                tween.IsLoop = false;
                tween.OnComplete = null;
                tween.OnUpdate = null;
                tween.DataSequences.Clear();
            }
        }


        /// <summary>�ش� ������Ʈ�� ��� Tween �Ͻ� ���� �Լ�(Play() ȣ�� ������ ����)</summary>
        public static void Pause(GameObject gameObject)
        {
            TweenData[] tweens = gameObject.GetComponents<TweenData>();

            foreach (TweenData tween in tweens)
            {
                tween.enabled = false;
            }
        }


        /// <summary>��ǥ ������ ���� �ð����� ������Ʈ�� �̵���Ű�� �Լ�</summary>
        public static TweenData TransformMove(GameObject targetObject, Vector3 targetPosition, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTransformMove objToMove = !targetObject.GetComponent<TweenTransformMove>()
                ? targetObject.AddComponent<TweenTransformMove>()
                : targetObject.GetComponent<TweenTransformMove>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetPosition;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToMove.IsLoop = false;
            objToMove.AddDataSequence(tempData);

            if (!objToMove.enabled)
            {
                objToMove.ElapsedDuration = 0;
                objToMove.TotalDuration = 0;
                objToMove.enabled = true;
            }

            return objToMove;
        }


        /// <summary>��ǥ ������ ���� �ð����� ������Ʈ�� ȸ����Ű�� �Լ�</summary>
        public static TweenData TransformRotate(GameObject targetObject, Vector3 targetEulerAngles, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTransformRotate objToRotate = !targetObject.GetComponent<TweenTransformRotate>()
                ? targetObject.AddComponent<TweenTransformRotate>()
                : targetObject.GetComponent<TweenTransformRotate>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetEulerAngles;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToRotate.IsLoop = false;
            objToRotate.AddDataSequence(tempData);

            if (!objToRotate.enabled)
            {
                objToRotate.ElapsedDuration = 0;
                objToRotate.TotalDuration = 0;
                objToRotate.enabled = true;
            }

            return objToRotate;
        }


        /// <summary>��ǥ ������ ���� �ð����� ������Ʈ�� ũ�⸦ �����ϴ� �Լ�</summary>
        public static TweenData TransformScale(GameObject targetObject, Vector3 targetScale, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTransformScale objToScale = !targetObject.GetComponent<TweenTransformScale>()
                ? targetObject.AddComponent<TweenTransformScale>()
                : targetObject.GetComponent<TweenTransformScale>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetScale;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToScale.IsLoop = false;
            objToScale.AddDataSequence(tempData);

            if (!objToScale.enabled)
            {
                objToScale.ElapsedDuration = 0;
                objToScale.TotalDuration = 0;
                objToScale.enabled = true;
            }

            return objToScale;
        }


        /// <summary>��ǥ ������ ���� �ð����� UI�� ũ�⸦ �����ϴ� �Լ�</summary>
        public static TweenData RectTransfromSizeDelta(GameObject targetObject, Vector2 targetSizeDelta, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenRectTransformSizeDelta objToSizeDelta = !targetObject.GetComponent<TweenRectTransformSizeDelta>()
                ? targetObject.AddComponent<TweenRectTransformSizeDelta>()
                : targetObject.GetComponent<TweenRectTransformSizeDelta>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetSizeDelta;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToSizeDelta.IsLoop = false;
            objToSizeDelta.AddDataSequence(tempData);

            if (!objToSizeDelta.enabled)
            {
                objToSizeDelta.ElapsedDuration = 0;
                objToSizeDelta.TotalDuration = 0;
                objToSizeDelta.enabled = true;
            }

            return objToSizeDelta;
        }


        /// <summary>��ǥ ������ ���� �ð����� UI�� ��ġ�� �̵���Ű�� �Լ�</summary>
        public static TweenData RectTransfromAnchoredPosition(GameObject targetObject, Vector2 targetAnchoredPosition, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenRectTransformAnchoredPosition objToAnchoredPosition = !targetObject.GetComponent<TweenRectTransformAnchoredPosition>()
                ? targetObject.AddComponent<TweenRectTransformAnchoredPosition>()
                : targetObject.GetComponent<TweenRectTransformAnchoredPosition>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetAnchoredPosition;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToAnchoredPosition.IsLoop = false;
            objToAnchoredPosition.AddDataSequence(tempData);

            if (!objToAnchoredPosition.enabled)
            {
                objToAnchoredPosition.ElapsedDuration = 0;
                objToAnchoredPosition.TotalDuration = 0;
                objToAnchoredPosition.enabled = true;
            }

            return objToAnchoredPosition;
        }


        /// <summary>LayoutGroup ������Ʈ�� spacing ��ġ�� �����ϴ� �Լ�</summary>
        public static TweenData LayoutGroupSpacing(GameObject targetObject, float targetValue, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenLayoutGroupSpacing ObjToLayoutGroup = !targetObject.GetComponent<TweenLayoutGroupSpacing>()
                ? targetObject.AddComponent<TweenLayoutGroupSpacing>()
                : targetObject.GetComponent<TweenLayoutGroupSpacing>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetValue;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            ObjToLayoutGroup.IsLoop = false;
            ObjToLayoutGroup.AddDataSequence(tempData);

            if (!ObjToLayoutGroup.enabled)
            {
                ObjToLayoutGroup.ElapsedDuration = 0;
                ObjToLayoutGroup.TotalDuration = 0;
                ObjToLayoutGroup.enabled = true;
            }

            return ObjToLayoutGroup;
        }



        /// <summary>��ǥ ������ ���� �ð����� �ؽ�Ʈ �÷� ���� �����ϴ� �Լ�</summary>
        public static TweenData TextColor(GameObject targetObject, Color targetColor, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTextColor objToColor = !targetObject.GetComponent<TweenTextColor>()
                ? targetObject.AddComponent<TweenTextColor>()
                : targetObject.GetComponent<TweenTextColor>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetColor;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� �ؽ�Ʈ ���� ���� �����ϴ� �Լ�</summary>
        public static TweenData TextAlpha(GameObject targetObject, float targetAlpha, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTextAlpha objToColor = !targetObject.GetComponent<TweenTextAlpha>()
                ? targetObject.AddComponent<TweenTextAlpha>()
                : targetObject.GetComponent<TweenTextAlpha>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetAlpha;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� TMP �÷� ���� �����ϴ� �Լ�</summary>
        public static TweenData TMPColor(GameObject targetObject, Color targetColor, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTMPColor objToColor = !targetObject.GetComponent<TweenTMPColor>()
                ? targetObject.AddComponent<TweenTMPColor>()
                : targetObject.GetComponent<TweenTMPColor>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetColor;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� TMP ���� ���� �����ϴ� �Լ�</summary>
        public static TweenData TMPAlpha(GameObject targetObject, float targetAlpha, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenTMPAlpha objToColor = !targetObject.GetComponent<TweenTMPAlpha>()
                ? targetObject.AddComponent<TweenTMPAlpha>()
                : targetObject.GetComponent<TweenTMPAlpha>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetAlpha;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� �̹��� �÷� ���� �����ϴ� �Լ�</summary>
        public static TweenData IamgeColor(GameObject targetObject, Color targetColor, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenImageColor objToColor = !targetObject.GetComponent<TweenImageColor>()
                ? targetObject.AddComponent<TweenImageColor>()
                : targetObject.GetComponent<TweenImageColor>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetColor;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� �̹��� ���� ���� �����ϴ� �Լ�</summary>
        public static TweenData IamgeAlpha(GameObject targetObject, float targetAlpha, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenImageAlpha objToColor = !targetObject.GetComponent<TweenImageAlpha>()
                ? targetObject.AddComponent<TweenImageAlpha>()
                : targetObject.GetComponent<TweenImageAlpha>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetAlpha;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� ��������Ʈ ������ �÷� ���� �����ϴ� �Լ�</summary>
        public static TweenData SpriteRendererColor(GameObject targetObject, Color targetColor, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenSpriteRendererColor objToColor = !targetObject.GetComponent<TweenSpriteRendererColor>()
                ? targetObject.AddComponent<TweenSpriteRendererColor>()
                : targetObject.GetComponent<TweenSpriteRendererColor>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetColor;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� ��������Ʈ ������ ���� ���� �����ϴ� �Լ�</summary>
        public static TweenData SpriteRendererAlpha(GameObject targetObject, float targetAlpha, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenSpriteRendererAlpha objToColor = !targetObject.GetComponent<TweenSpriteRendererAlpha>()
                ? targetObject.AddComponent<TweenSpriteRendererAlpha>()
                : targetObject.GetComponent<TweenSpriteRendererAlpha>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetAlpha;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }


        /// <summary>��ǥ ������ ���� �ð����� ĵ���� �׷� ���� ���� �����ϴ� �Լ�</summary>
        public static TweenData CanvasGroupAlpha(GameObject targetObject, float targetAlpha, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenCanvasGroupAlpha objToAlpha = !targetObject.GetComponent<TweenCanvasGroupAlpha>()
                ? targetObject.AddComponent<TweenCanvasGroupAlpha>()
                : targetObject.GetComponent<TweenCanvasGroupAlpha>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetAlpha;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToAlpha.IsLoop = false;
            objToAlpha.AddDataSequence(tempData);

            if (!objToAlpha.enabled)
            {
                objToAlpha.ElapsedDuration = 0;
                objToAlpha.TotalDuration = 0;
                objToAlpha.enabled = true;
            }

            return objToAlpha;
        }


        /// <summary>��ǥ ������ ���� �ð����� ī�޶� ������ ���� �����ϴ� �Լ�</summary>
        public static TweenData CameraOrthographicSize(GameObject targetObject, float targetSize, float duration, TweenMode tweenMode = TweenMode.Constant, Action onComplete = null, Action onUpdate = null)
        {
            TweenCameraSize objToColor = !targetObject.GetComponent<TweenCameraSize>()
                ? targetObject.AddComponent<TweenCameraSize>()
                : targetObject.GetComponent<TweenCameraSize>();

            DataSequence tempData = new DataSequence();
            tempData.TargetValue = targetSize;
            tempData.Duration = duration;
            tempData.TweenMode = tweenMode;
            tempData.OnComplete = onComplete;
            tempData.OnUpdate = onUpdate;

            objToColor.IsLoop = false;
            objToColor.AddDataSequence(tempData);

            if (!objToColor.enabled)
            {
                objToColor.ElapsedDuration = 0;
                objToColor.TotalDuration = 0;
                objToColor.enabled = true;
            }

            return objToColor;
        }

    }
}

