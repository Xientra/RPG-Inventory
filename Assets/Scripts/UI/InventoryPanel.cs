using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	[Header("External References:")]

	[Tooltip("The Inventory object this UI Panel will change")]
	public Inventory inventory;
	public ItemPanel itemPanel;


	[Header("Internal References:")]

	public UIItemSlot itemSlotExample;
	public Transform contentView;
	public Transform addItemButtonSpace;

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
		ClearUIItemSlots();

		for (int i = 0; i < inventory.items.Count; i++)
		{
			CreateUIItemSlot(i, inventory.items[i]);
			//CreateUIItemSlot(i, inventory.items[i].item, inventory.items[i].amount);
		}

		addItemButtonSpace.SetAsLastSibling();
	}
	public void UpdateUI(Inventory.InventorySlot slot)
	{
		UpdateUI();
	}

	private void ClearUIItemSlots()
	{
		foreach (Transform t in contentView)
			if (t.gameObject.activeSelf == true && t.CompareTag("UI Item Slot"))
				Destroy(t.gameObject);
	}

	private void CreateUIItemSlot(int index, Item item, int amount)
	{
		UIItemSlot slot = Instantiate(itemSlotExample, contentView).GetComponent<UIItemSlot>();
		slot.Set(item, amount);
		slot.name = "[" + item.name + "]";
		slot.gameObject.SetActive(true);
	}

	private void CreateUIItemSlot(int index, Inventory.InventorySlot invSlot)
	{
		UIItemSlot slot = Instantiate(itemSlotExample, contentView).GetComponent<UIItemSlot>();
		slot.Set(invSlot);
		slot.name = "[" + invSlot.item.name + "]";
		slot.gameObject.SetActive(true);
	}




	// -========== Buttons/Events ==========- //

	public void Event_OnAmountChanged(UIItemSlot origin)
	{
		inventory.SetAmount(origin.GetAmount(), origin.inventorySlot);
	}


	public void Btn_AddItemToInventory()
	{
		if (itemPanel.HasItem)
			inventory.AddItem(itemPanel.CreateItemFromFields());
		else
			inventory.AddItem();
	}

	public void Btn_Remove(UIItemSlot origin)
	{
		//inventory.RemoveSlot(origin.nameLabel.text); // using the name
		inventory.RemoveSlot(origin.inventorySlot);
	}

	public void Btn_Duplicate(UIItemSlot origin)
	{
		//inventory.RemoveSlot(origin.inventorySlot);
		Debug.LogWarning("Does not work and i am too lazy to make it work.");
	}

	public void Btn_OnItemSlotClick(UIItemSlot origin)
	{
		itemPanel.DisplayItem(origin.inventorySlot.item, inventory);
	}


	public void Btn_MoveUp(UIItemSlot origin)
	{
		ChangeOrder(origin, -1);
	}
	public void Btn_MoveDown(UIItemSlot origin)
	{
		ChangeOrder(origin, +1);
	}
	public void ChangeOrder(UIItemSlot origin, int amount)
	{
		//inventory.MoveSlot(origin.GetName(), amount); // using the name
		inventory.MoveSlot(origin.inventorySlot, amount);
	}



	// -========== Export and Import ==========- //


	public void Btn_ExportItemList()
	{
		if (!inventory.items.Any())
		{
			PopupWindow.Create("Inventory is empty.");
			return;
		}

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
			if (!importObj.itemSlots.Any())
			{
				PopupWindow.Create(PopupWindow.importFailedErrorMsg);
				return;
			}
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
