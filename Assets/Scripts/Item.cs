using System;
/*
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
[Serializable]
public class Item : ScriptableObject
*/
[Serializable]
public class Item
{
	public string name;
	public string description;
	public string imagePath;

	public Item(string name, string description, string imagePath)
	{
		this.name = name;
		this.description = description;
		this.imagePath = imagePath;
	}
}
