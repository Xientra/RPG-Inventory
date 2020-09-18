using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIItemSlot : MonoBehaviour
{
	public TMP_Text nameLabel;
	public TMP_InputField amountInput;

	public Inventory.InventorySlot inventorySlot;


	public void Set(Inventory.InventorySlot inventorySlot)
	{
		this.inventorySlot = inventorySlot;
		nameLabel.text = inventorySlot.item.name;
		amountInput.text = inventorySlot.amount.ToString();
	}

	private void UpdateUI()
	{
		nameLabel.text = inventorySlot.item.name;
		amountInput.text = inventorySlot.amount.ToString();
	}

	public void Set(Item item)
	{
		nameLabel.text = item.name;
		amountInput.text = "1";
	}

	public void Set(Item item, int amount)
	{
		nameLabel.text = item.name;
		amountInput.text = amount.ToString();
	}

	public string GetName()
	{
		return nameLabel.text;
	}

	public int GetAmount()
	{
		return int.Parse(amountInput.text);
	}

	public void Btn_ChangeAmount(int amount)
	{
		string newText = (int.Parse(amountInput.text) + amount).ToString();
		if (newText.Length <= amountInput.characterLimit)
			amountInput.text = newText;
	}
}
