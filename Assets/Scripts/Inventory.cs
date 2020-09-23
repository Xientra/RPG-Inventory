using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{

	public List<InventorySlot> items;

	public event Action OnItemAdded;
	public event Action OnItemRemoved;
	public event Action<InventorySlot> OnSlotUpdated;
	public event Action OnOrderUpdated;

	public void AddItem()
	{
		Item emptyItem = new Item("Empty item", "", "");
		InventorySlot newItem = new InventorySlot(emptyItem);
		items.Add(newItem);

		OnItemAdded?.Invoke();
	}

	public void AddItem(Item item)
	{
		/*
		if (items.Exists((s) => s.item.name == item.name))
		{
			Debug.Log("Item Allready Exists!");
			return;
		}
		*/

		InventorySlot newItem = new InventorySlot(item);
		items.Add(newItem);

		OnItemAdded?.Invoke();
	}

	public void UpdateItem(Item itemToUpdate, Item newValues)
	{
		itemToUpdate.name = newValues.name;
		itemToUpdate.description = newValues.description;
		itemToUpdate.imagePath = newValues.imagePath;

		OnItemAdded?.Invoke();
	}
	public void UpdateItem(Item itemToUpdate, string newName, string newDescription, string newPath)
	{
		itemToUpdate.name = newName;
		itemToUpdate.description = newDescription;
		itemToUpdate.imagePath = newPath;

		OnItemAdded?.Invoke();
	}

	public void MergeItemLists(List<InventorySlot> list)
	{
		/*
		foreach (InventorySlot slot in list)
			if (!items.Exists((s) => s.item.name == slot.item.name))
				items.Add(slot);
		*/
		items.AddRange(list);

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

		OnSlotUpdated?.Invoke(slot);
	}

	public void RemoveSlot(string itemName)
	{
		RemoveSlot(GetSlot(itemName));
	}
	public void RemoveSlot(int index)
	{
		RemoveSlot(items[index]);
	}
	public void RemoveSlot(InventorySlot slot)
	{
		items.Remove(slot);
		OnItemRemoved?.Invoke();
	}

	public void MoveSlot(string itemName, int moveBy)
	{
		MoveSlot(GetSlot(itemName), moveBy);
	}

	public void MoveSlot(InventorySlot slot, int moveBy)
	{
		int oldIndex = items.IndexOf(slot);
		items.Remove(slot);

		int newIndex = oldIndex + moveBy;
		// clamp newIndex between 0 and items.Count
		newIndex = newIndex >= items.Count ? items.Count : newIndex < 0 ? 0 : newIndex;

		items.Insert(newIndex, slot);

		OnOrderUpdated?.Invoke();
	}


	[Serializable]
	public class InventorySlot
	{
		public int amount;
		public Item item;
		public string hexColor;

		public InventorySlot(Item item)
		{
			this.item = item;
			this.amount = 1;
		}
	}
}
