using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{

	public List<ItemSlot> items;

	void Start()
	{

	}

	void Update()
	{

	}

	public void AddItem(Item item)
	{
		ItemSlot newItem = new ItemSlot(item);
		items.Add(newItem);
	}

	public void RemoveItem(int index)
	{
		items.RemoveAt(index);
	}

	public void RemoveItem(ItemSlot itemSlot)
	{
		items.Remove(itemSlot);
	}

	public void SetAmount(int index, int newAmount)
	{
		ItemSlot s = items[index];
		s.amount++;
	}

	public void SetAmount(ItemSlot itemSlot, int newAmount)
	{
		itemSlot.amount = newAmount;
	}

	public void IncreasAmount(int index)
	{
		ItemSlot s = items[index];
		s.amount++;
	}

	public void IncreasAmount(ItemSlot itemSlot)
	{
		itemSlot.amount++;
	}

	[Serializable]
	public struct ItemSlot
	{
		public int amount;
		public Item item;

		public ItemSlot(Item item)
		{
			this.item = item;
			this.amount = 1;
		}
	}

}
