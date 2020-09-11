using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{

	public List<InventorySlot> items;

	void Start()
	{

	}

	void Update()
	{

	}

	public void AddItem(Item item)
	{
		InventorySlot newItem = new InventorySlot(item);
		items.Add(newItem);
	}

	public void RemoveItem(int index)
	{
		items.RemoveAt(index);
	}

	public void RemoveItem(InventorySlot itemSlot)
	{
		items.Remove(itemSlot);
	}

	public void SetAmount(int index, int newAmount)
	{
		InventorySlot s = items[index];
		s.amount++;
	}

	public void SetAmount(InventorySlot itemSlot, int newAmount)
	{
		itemSlot.amount = newAmount;
	}

	public void IncreasAmount(int index)
	{
		InventorySlot s = items[index];
		s.amount++;
	}

	public void IncreasAmount(InventorySlot itemSlot)
	{
		itemSlot.amount++;
	}

	[Serializable]
	public struct InventorySlot
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
