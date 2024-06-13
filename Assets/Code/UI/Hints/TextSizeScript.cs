using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Hints
{
    public class TextSizeScript : MonoBehaviour
    {
        public string Text
        {
            get => text.text;
            set => text.text = value;
        }

        [SerializeField] TMP_Text text;
        [SerializeField] RectTransform rect;

        public float minWidth;
        public float minHeight;

        public float maxWidth;
        public float maxHeight;

        Coroutine sizeCoroutine;
        public void ChangeSize(Action OnComplete)
        {
            sizeCoroutine = StartCoroutine(SizeUpdate(OnComplete));
        }

        public void CancelChangeSize()
        {
            if (sizeCoroutine != null)
                StopCoroutine(sizeCoroutine);
        }

        IEnumerator SizeUpdate(Action onColmplete)
        {
            if (!text || !rect)
                yield break;

            var curWidth = minWidth;
            var curHeight = minHeight;

            SetSize(curWidth, curHeight);
            yield return null;

            var maxSteps = 8;
            var widthDelta = maxWidth - minWidth;
            var heightDelta = maxHeight - minHeight;

            for (var i = 0; i < maxSteps; i++)
            {
                if (i != maxSteps - 1)
                {
                    widthDelta /= 2;
                    heightDelta /= 2;
                }

                SetSize(curWidth + widthDelta, curHeight + heightDelta);
                yield return null;

                var textSmaller = text.fontSize < text.fontSizeMax;

                if (textSmaller)
                {
                    curHeight += heightDelta;
                    curWidth += widthDelta;
                }
            }

            onColmplete?.Invoke();
        }

        void SetSize(float width, float height)
        {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            text.Rebuild(CanvasUpdate.MaxUpdateValue);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }
}