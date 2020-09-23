using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PopupWindow : MonoBehaviour
{
	public static PopupWindow instance;

	public TMP_Text messageLabel;
	private Image darkBackground;

	public static string nameEmptyOrWhiteSpaceErrorMsg = "Please enter a name that is not empty or just whitespace.";
	public static string importFailedErrorMsg = "Import failed. Please make sure, that you selected a valid path and a correct .json file.";
	public static string imageLoadingFailedErrorMsg = "Image loading failed. Please make sure, that you selected a valid path and file.";


	private void Awake()
	{
		if (instance == null)
			instance = this;

		darkBackground = GetComponent<Image>();

		Activate(false);
	}

	public static void Create(string message)
	{
		instance.Show(message);
	}

	public void Show(string message)
	{
		messageLabel.text = message;
		Activate(true);
	}

	public void Btn_OK()
	{
		messageLabel.text = "";
		Activate(false);
	}

	private void Activate(bool value)
	{
		darkBackground.enabled = value;

		foreach (Transform t in transform)
			t.gameObject.SetActive(value);
	}
}
