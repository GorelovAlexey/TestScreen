using System.Collections.Generic;
using System.Linq;
using Code.Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class RewardsWindow : BaseWindow
    {
        [SerializeField] private Button btnClose;
        [SerializeField] private TMP_Text btnCloseText;

        [SerializeField] private RectTransform content;
        [SerializeField] private ScrollRect scrollRect;

        [SerializeField] private TMP_Text textTitle;
        [SerializeField] private ItemView itemView;
        [SerializeField] private HintInventoryItemTest hintView;

        protected override void Awake()
        {
            base.Awake();
            btnClose.onClick.AddListener(Hide);
            btnCloseText.text = "Close";

            hintView.gameObject.SetActive(false);
        }

        protected override void OnShow(object[] args)
        {
            textTitle.text = (string)args[0];

            var items = (List<InventoryItem>)args[1];

            foreach (var i in items)
            {
                var inst = Instantiate(itemView, content.transform);
                inst.Setup(i);
                inst.gameObject.name = $"Item: {i.Title}";

                var dragController = inst.GetOrAddComponent<HintDragController>();
                dragController.Setup(inst, hintView);
            }

            // horiztal layout и content size fitter обычно могут не сразу занимать верный размер на экране
            // данный вызов исправляет визуальный недочет
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);

            scrollRect.horizontalNormalizedPosition = 0;
        }

        protected override void OnHide()
        {
        }
    }
}