using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	public Inventory inventory;

	public ItemPanel itemPanel;

	public ItemSlot itemSlotExample;

	public Transform contentView;

	private void Start()
	{
		inventory.OnItemAdded += UpdateUI;
		inventory.OnItemRemoved += UpdateUI;
		itemSlotExample.gameObject.SetActive(false);
	}

	public void Btn_OnItemSlotClick(ItemSlot origin)
	{
		itemPanel.DisplayItem(inventory.items[origin.index].item);
	}

	public void OnAmountChanged(ItemSlot origin)
	{
		inventory.SetAmount(origin.GetAmount(), origin.GetName());
	}

	public void AddItemToInventory()
	{
		inventory.AddItem(itemPanel.GetItemFromFields());
	}

	public void Btn_Remove(ItemSlot origin)
	{
		// remove item from inventory
		inventory.RemoveSlot(origin.nameLabel.text);
	}

	public void UpdateUI()
	{
		ClearContentView();

		for (int i = 0; i < inventory.items.Count; i++)
		{
			CreateUIItemSlot(i, inventory.items[i].item, inventory.items[i].amount);
		}
	}

	private void ClearContentView()
	{
		foreach (Transform t in contentView)
			if (t.gameObject.activeSelf == true)
				Destroy(t.gameObject);
	}

	private void CreateUIItemSlot(int index, Item item, int amount)
	{
		ItemSlot slot = Instantiate(itemSlotExample, contentView).GetComponent<ItemSlot>();
		slot.Set(index, item, amount);
		slot.name = "[" + item.name + "]";
		slot.gameObject.SetActive(true);
	}
}
