using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{

	public List<InventorySlot> items;

	public event Action OnItemAdded;
	public event Action OnItemRemoved;
	public event Action<InventorySlot> OnAmountUpdated;
	public event Action OnOrderUpdated;

	public void AddItem(Item item)
	{
		if (items.Exists((s) => s.item.name == item.name))
		{
			Debug.Log("Item Allready Exists!");
			return;
		}

		InventorySlot newItem = new InventorySlot(item);
		items.Add(newItem);

		OnItemAdded?.Invoke();
	}

	public InventorySlot GetSlot(string itemName)
	{
		return items.FindLast((s) => s.item.name == itemName);
	}
	public InventorySlot GetSlot(int index)
	{
		return items[index];
	}

	public void SetAmount(int newAmount, string itemName)
	{
		SetAmount(newAmount, GetSlot(itemName));
	}
	public void SetAmount(int newAmount, int index)
	{
		SetAmount(newAmount, items[index]);
	}
	public void SetAmount(int newAmount, InventorySlot slot)
	{
		slot.amount = newAmount;

		OnAmountUpdated?.Invoke(slot);
	}

	public void RemoveSlot(string itemName)
	{
		RemoveSlot(GetSlot(itemName));
	}
	public void RemoveSlot(int index)
	{
		RemoveSlot(items[index]);
	}
	private void RemoveSlot(InventorySlot slot)
	{
		items.Remove(slot);
		OnItemRemoved?.Invoke();
	}



	[Serializable]
	public class InventorySlot
	{
		public int amount;
		public Item item;

		public InventorySlot(Item item)
		{
			this.item = item;
			this.amount = 1;
		}
	}

}
