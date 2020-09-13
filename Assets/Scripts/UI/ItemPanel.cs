using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using SFB;
using System.IO;

public class ItemPanel : MonoBehaviour
{
	public TMP_InputField nameInput;
	public TMP_InputField pathInput;
	public TMP_InputField descritpionInput;

	[Space(5)]

	public RawImage itemImage;

	[Header("For Edit Btn:")]

	public Button editBtn;
	public GameObject[] setActiveOnEdit;

	[Space(5)]

	public Sprite editBtnStart;
	public Sprite editBtnFinish;

	[Space(5)]

	public ImageFullscreenPanel imageFullScreenPrefab;


	public void DisplayItem(Item item)
	{
		Clear();

		nameInput.text = item.name;
		descritpionInput.text = item.description;
		pathInput.text = item.imagePath;
		SetImage(LoadImage(item.imagePath));
	}

	public void Clear()
	{
		nameInput.text = "";
		pathInput.text = "";
		descritpionInput.text = "";
		itemImage.texture = null;
		itemImage.rectTransform.localScale = new Vector2(1, 1);
	}

	public Item GetItemFromFields()
	{
		if (nameInput == null || string.IsNullOrEmpty(nameInput.text) || string.IsNullOrWhiteSpace(nameInput.text))
			throw new ArgumentException("Item name must not be empty or whitespace.");

		return new Item(nameInput.text, descritpionInput.text, pathInput.text);
	}

	public Item ImportItem(string path)
	{
		return JsonUtility.FromJson<Item>(File.ReadAllText(path));
	}

	public void ExportItem(string path, Item item)
	{
		string jsonItem = JsonUtility.ToJson(item);
		File.WriteAllText(path, jsonItem);
	}



	public void OpenImageFullscreen()
	{
		Instantiate(imageFullScreenPrefab.gameObject, transform.parent).GetComponent<ImageFullscreenPanel>().SetTexture(itemImage.texture);
	}

	private void SetEditMode(bool value)
	{
		nameInput.interactable = value;
		descritpionInput.interactable = value;

		pathInput.gameObject.SetActive(value);

		foreach (GameObject go in setActiveOnEdit)
			go.SetActive(value);

		if (value)
			editBtn.image.sprite = editBtnFinish;
		else
			editBtn.image.sprite = editBtnStart;
	}


	private Texture2D LoadImage(string path)
	{
		try
		{
			byte[] byteArray = File.ReadAllBytes(path);

			Texture2D newTex = new Texture2D(2, 2);
			bool loadingSuccessful = newTex.LoadImage(byteArray);

			return loadingSuccessful ? newTex : null;
		}
		catch
		{
			return null;
		}
	}

	private void SetImage(Texture2D tex)
	{
		if (tex == null)
			return;

		if (tex.width > tex.height)
		{
			float newHeight = ((float)tex.height / (float)tex.width);
			itemImage.rectTransform.localScale = new Vector2(1, newHeight);
		}
		else if (tex.height > tex.width)
		{
			float newWidth = ((float)tex.width / (float)tex.height);
			itemImage.rectTransform.localScale = new Vector2(newWidth, 1);
		}

		itemImage.texture = tex;
	}


	// -========== Button Methods ==========- //

	public void Btn_Edit()
	{
		if (nameInput.interactable == false && descritpionInput.interactable == false)
		{
			SetEditMode(true);
		}
		else if (nameInput.interactable == true && descritpionInput.interactable == true)
		{
			SetEditMode(false);
		}
	}

	public void Btn_SelectImage()
	{
		string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", LastPaths.instance.lastImagesPath, new ExtensionFilter[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg", "jiff") }, false);

		if (paths.Length == 1)
		{
			LastPaths.instance.SetLastImagePath(paths[0]);
			pathInput.text = paths[0];
			SetImage(LoadImage(paths[0]));
		}
	}

	public void Btn_Clear()
	{
		Clear();
	}

	public void Btn_ImportItem()
	{
		string[] paths = StandaloneFileBrowser.OpenFilePanel("Import", LastPaths.instance.lastItemDataPath, "json", false);

		if (paths.Length > 0)
		{
			LastPaths.instance.SetLastSaveDataPath(paths[0]);
			DisplayItem(ImportItem(paths[0]));
		}
		else
			Debug.Log("No File Selected");
	}

	public void Btn_ExportItem()
	{
		Item i = GetItemFromFields();

		string path = StandaloneFileBrowser.SaveFilePanel("Export To", LastPaths.instance.lastItemDataPath, i.name, "json");

		if (!string.IsNullOrEmpty(path))
		{
			LastPaths.instance.SetLastSaveDataPath(path);
			ExportItem(path, i);
		}
	}
}
