using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	public Inventory inventory;

	public ItemPanel itemPanel;

	public void DispalyItem(ItemSlot itemSlot)
	{
		itemPanel.DisplayItem(inventory.items[itemSlot.index].item);
	}
}
