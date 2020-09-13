using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	[Header("External References:")]

	[Tooltip("The Inventory object this UI Panel will change")]
	public Inventory inventory;
	public ItemPanel itemPanel;


	[Header("Internal References:")]

	public ItemSlot itemSlotExample;
	public Transform contentView;

	private void Start()
	{
		inventory.OnItemAdded += UpdateUI;
		inventory.OnItemRemoved += UpdateUI;
		inventory.OnOrderUpdated += UpdateUI;
		//inventory.OnSlotUpdated += UpdateUI;
		itemSlotExample.gameObject.SetActive(false);
	}

	public void UpdateUI()
	{
		ClearContentView();

		for (int i = 0; i < inventory.items.Count; i++)
		{
			CreateUIItemSlot(i, inventory.items[i].item, inventory.items[i].amount);
		}
	}
	public void UpdateUI(Inventory.InventorySlot slot)
	{
		UpdateUI();
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


	public void OnAmountChanged(ItemSlot origin)
	{
		inventory.SetAmount(origin.GetAmount(), origin.GetName());
	}


	// -========== Buttons ==========- //

	public void Btn_AddItemToInventory()
	{
		inventory.AddItem(itemPanel.GetItemFromFields());
	}
	public void Btn_UpdateItem()
	{
		inventory.UpdateItem(itemPanel.GetItemFromFields());
	}


	public void Btn_Remove(ItemSlot origin)
	{
		inventory.RemoveSlot(origin.nameLabel.text);
	}

	public void Btn_OnItemSlotClick(ItemSlot origin)
	{
		itemPanel.DisplayItem(inventory.items[origin.index].item);
	}

	public void ChangeOrder(ItemSlot origin, int amount)
	{
		inventory.MoveSlot(origin.GetName(), amount);
	}

	public void Btn_MoveUp(ItemSlot origin)
	{
		ChangeOrder(origin, -1);
	}
	public void Btn_MoveDown(ItemSlot origin)
	{
		ChangeOrder(origin, +1);
	}


	public void Btn_ExportItemList()
	{
		string path = StandaloneFileBrowser.SaveFilePanel("Export To", LastPaths.instance.lastItemListPath, "ItemList", "json");

		if (!string.IsNullOrEmpty(path))
		{
			LastPaths.instance.SetLastItemListPath(path);

			InventoryList exportObj = new InventoryList(inventory.items);
			string jsonObj = JsonUtility.ToJson(exportObj);
			File.WriteAllText(path, jsonObj);
		}
	}

	public void Btn_ImportItemList()
	{
		string[] paths = StandaloneFileBrowser.OpenFilePanel("Import", LastPaths.instance.lastItemListPath, "json", false);

		if (paths.Length > 0)
		{
			LastPaths.instance.SetLastItemListPath(paths[0]);

			InventoryList importObj = JsonUtility.FromJson<InventoryList>(File.ReadAllText(paths[0]));
			inventory.MergeItemLists(importObj.itemSlots);
		}
	}

	/// <summary>
	/// Used for exporting and importing a list of items.
	/// </summary>
	public class InventoryList
	{
		public List<Inventory.InventorySlot> itemSlots;

		public InventoryList(List<Inventory.InventorySlot> itemSlots)
		{
			this.itemSlots = itemSlots;
		}
	}
}
