using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPaths : MonoBehaviour
{
	public static LastPaths instance;

	[HideInInspector]
	public string lastItemDataPath;
	[HideInInspector]
	public string lastImagesPath;
	[HideInInspector]
	public string lastItemListPath;


	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			throw new System.Exception("There should only be one LastPaths instance in a scene!");

		lastItemDataPath = PlayerPrefs.GetString("lastSaveDataPath", "");
		lastImagesPath = PlayerPrefs.GetString("lastImagesPath", "");
		lastItemListPath = PlayerPrefs.GetString("lastItemListPath", "");
	}

	private void OnDestroy()
	{
		PlayerPrefs.SetString("lastSaveDataPath", lastItemDataPath);
		PlayerPrefs.SetString("lastImagesPath", lastImagesPath);
		PlayerPrefs.SetString("lastItemListPath", lastItemListPath);
	}

	public void SetLastSaveDataPath(string filePath)
	{
		lastItemDataPath = filePath.Substring(0, filePath.LastIndexOf('\\'));
	}

	public void SetLastImagePath(string filePath)
	{
		lastImagesPath = filePath.Substring(0, filePath.LastIndexOf('\\'));
	}

	public void SetLastItemListPath(string filePath)
	{
		lastItemListPath = filePath.Substring(0, filePath.LastIndexOf('\\'));
	}
}
