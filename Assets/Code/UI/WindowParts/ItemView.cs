using Code.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Assets.Code.UI
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image icon;

        public InventoryItem Item { get; private set; }
        public void Setup(InventoryItem item) 
        {
            Item = item;

            icon.sprite = item.Icon;
        }
    }
}