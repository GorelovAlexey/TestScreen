using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Assets.Code.UI
{
    public class HintDragController : MonoBehaviour, IPointerDownHandler
    {
        private HintInventoryItemTest hint;
        private ItemView itemView;

        public void Setup(ItemView itemView, HintInventoryItemTest hint)
        {
            this.hint = hint;
            this.itemView = itemView;
        }

        Tween delayedDisplayTween;
        PointerEventData eventData;
        public void OnPointerDown(PointerEventData eventData)
        {
            hint.Hide(true);

            delayedDisplayTween?.Kill();
            delayedDisplayTween = DOVirtual.DelayedCall(hint.hintDisplayDelaySec, DisplayHint)
                .SetLink(gameObject).SetLink(hint.gameObject);

            if (dragObserver == null)
                dragObserver = new DragObserver();

            dragObserver.Start(eventData.position, hint.hintMaxDistRelative, OnCancelHint);
        }

        protected void DisplayHint()
        {
            if (hint && dragObserver != null)
                hint.Display(itemView.Item, dragObserver.initialPosition);
        }

        public void OnCancelHint()
        {
            delayedDisplayTween?.Kill();
            hint.Hide();
        }

        DragObserver dragObserver;
        public void Update()
        {
            if (dragObserver?.Active == true)
            {
                hint?.UpdatePosition(dragObserver.PressPosition);
                dragObserver.UpdateHint(hint?.gameObject.activeSelf == true);
                dragObserver.Update();
            }
        }

        private class DragObserver 
        {
            public bool Active { get; private set; }

            public Vector2 initialPosition;
            public float maxRelativeMagnitude;
            public bool hintShown;
            Action onFinish;

            public void Start(Vector2 initialTouchPos, float maxRelMagnitude, Action hideHint)
            {
                initialPosition = initialTouchPos;
                hintShown = false;
                maxRelativeMagnitude = maxRelMagnitude;
                Active = true;
                onFinish = hideHint;
            }

            private void Finish()
            {
                onFinish?.Invoke();
                Active = false;
            }

            public void UpdateHint(bool shown)
            {
                hintShown = shown;
            }

            public void Update()
            {
                if (!Active)
                    return;

                // mobile and pc
                if (!IsPressed)
                {
                    Finish();
                    return;
                }

                if (hintShown)
                    return;

                var position = PressPosition;
                var delta = position - initialPosition;
                var relativeMagnitude = new Vector2(delta.x / (float)Screen.width, delta.y / (float)Screen.height).magnitude;

                if (relativeMagnitude > maxRelativeMagnitude)
                    Finish();
            }

            public bool IsPressed => Input.touchCount > 0 || Input.GetMouseButton(0);
            public Vector2 PressPosition => Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
        }
    }
}