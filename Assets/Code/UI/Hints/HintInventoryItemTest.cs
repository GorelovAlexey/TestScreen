using Assets.Code.UI.Hints;
using Code.Inventory;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class HintInventoryItemTest : MonoBehaviour
    {
        public float hintMaxDistRelative = .01f;
        public float hintDisplayDelaySec = 1.1f;
        public float hintAlphaDuration = .15f;
        public bool followTouchPosition;

        [Space]
        [SerializeField] TMP_Text textTitle;
        [SerializeField] TextSizeScript textDesc;
        [SerializeField] Image imageIcon;
        [SerializeField] CanvasGroup canvasGroup;

        public void Awake()
        {
            // TODO SET MAX|MIN SIZE
        } 

        private Tween alphaTween;
        public void SetData(InventoryItem item)
        {
            textTitle.text = item.Title;
            textDesc.Text = item.Description;
            imageIcon.sprite = item.Icon;
        }

        public void Display(InventoryItem item, Vector3 screenPos)
        {
            gameObject.SetActive(true);
            SetData(item);
            UpdatePosition(screenPos);

            canvasGroup.alpha = 0;
            alphaTween?.Kill();
            alphaTween = canvasGroup.DOFade(1, hintAlphaDuration)
                .SetLink(gameObject).Pause();

            textDesc.ChangeSize(() =>
            {
                alphaTween?.Play();
            });

        }

        public void Hide(bool instant = false)
        {
            alphaTween?.Kill();

            if (instant)
            {
                gameObject.SetActive(false);
                return;
            }

            alphaTween = canvasGroup.DOFade(0, hintAlphaDuration)
                .OnComplete(() => gameObject.SetActive(false))
                .SetLink(gameObject);
        }

        public void UpdatePosition(Vector3 screenPosition)
        {
            if (followTouchPosition)
                transform.position = screenPosition;
        }
    }
}