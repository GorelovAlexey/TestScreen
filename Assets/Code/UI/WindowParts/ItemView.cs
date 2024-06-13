using Code.Inventory;
using UnityEngine;
using UnityEngine.UI;

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