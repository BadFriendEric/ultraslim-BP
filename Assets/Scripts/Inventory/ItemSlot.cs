using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    Item item;
    public Image icon;

    public void addItem(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
