using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
	public TMP_Text nameLabel;
	public TMP_InputField amountInput;
	public int index;

	public void Set(int index, Item item)
	{
		nameLabel.text = item.name;
		this.index = index;
		amountInput.text = "1";
	}

	public void Set(int index, Item item, int amount)
	{
		nameLabel.text = item.name;
		this.index = index;
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
