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


	private string lastSaveDataPath;
	private string lastImagesPath;

	private void Awake()
	{
		lastSaveDataPath = PlayerPrefs.GetString("lastSaveDataPath", "");
		lastImagesPath = PlayerPrefs.GetString("lastImagesPath", "");
	}

	private void OnDestroy()
	{
		PlayerPrefs.SetString("lastSaveDataPath", lastSaveDataPath);
		PlayerPrefs.SetString("lastImagesPath", lastImagesPath);
	}

	public void DisplayItem(Item item)
	{
		nameInput.text = item.name;
		descritpionInput.text = item.description;
		pathInput.text = item.imagePath;
		SetImage(LoadImage(item.imagePath));
	}

	public Item GetItemFromFields()
	{
		if (nameInput == null || string.IsNullOrEmpty(nameInput.text) || string.IsNullOrWhiteSpace(nameInput.text))
			throw new ArgumentException("Item name must not be empty or whitespace.");

		return new Item(nameInput.text, descritpionInput.text, pathInput.text);
	}

	public void ImportItem()
	{
		string[] paths = StandaloneFileBrowser.OpenFilePanel("Import", lastSaveDataPath, "json", false);

		if (paths.Length > 0)
		{
			lastSaveDataPath = paths[0].Substring(0, paths[0].LastIndexOf('\\'));
			DisplayItem(ImportItemFromPath(paths[0]));
		}
		else
			Debug.Log("No File Selected");
	}

	public void ExportItem()
	{
		Item i = GetItemFromFields();

		string path = StandaloneFileBrowser.SaveFilePanel("Export To", lastSaveDataPath, i.name, "json");

		if (!string.IsNullOrEmpty(path))
		{
			lastSaveDataPath = path.Substring(0, path.LastIndexOf('\\'));
			string jsonItem = JsonUtility.ToJson(i);
			File.WriteAllText(path, jsonItem);
		}
	}

	public Item ImportItemFromPath(string path)
	{
		return (Item)JsonUtility.FromJson(File.ReadAllText(path), typeof(Item));
	}



	public void OpenImageFullscreen()
	{
		Instantiate(imageFullScreenPrefab.gameObject, transform.parent).GetComponent<ImageFullscreenPanel>().image.texture = itemImage.texture;
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
		string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", lastImagesPath, new ExtensionFilter[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg", "jiff") }, false);

		if (paths.Length == 1)
		{
			lastImagesPath = paths[0].Substring(0, paths[0].LastIndexOf('\\'));
			pathInput.text = paths[0];
			SetImage(LoadImage(paths[0]));
		}
	}

	public void Btn_Clear()
	{
		nameInput.text = "";
		pathInput.text = "";
		descritpionInput.text = "";
		itemImage.texture = null;
		itemImage.rectTransform.localScale = new Vector2(1, 1);
	}
}
