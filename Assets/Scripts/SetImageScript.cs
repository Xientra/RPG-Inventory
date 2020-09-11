using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using SFB;
using System;
#if UNITY_STANDALONE_WIN
using System.Windows.Forms;
#endif

public class SetImageScript : MonoBehaviour
{
	public TMP_InputField pathInputField;
	public RawImage image;

	public const string filePathPrefix = @"file:///";
	// file:///D:\_D&D\Image Resources\_Other\Pentagramm.png
	// file:///D:/_D&D/Image%20Resources/_Other/Pentagramm.png

	private void Start()
	{
		Debug.Log(Directory.GetCurrentDirectory());
	}


	public void SetImage()
	{
		SetImageUsingTexture2D();
	}

	public void SetImageUsingWindows()
	{
		/*
		OpenFileDialog ofd = new OpenFileDialog();

		ofd.InitialDirectory = Directory.GetCurrentDirectory();

		if (ofd.ShowDialog() == DialogResult.OK)
		{
			SetImageUsingTexture2D(ofd.FileName);
		}
		*/


		string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", new ExtensionFilter[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg") }, false);

		if (paths.Length == 1)
			SetImageUsingTexture2D(paths[0]);
		else
			Debug.Log("No File Selected");
	}

	[Obsolete]
	private IEnumerator SetImageUsingWWW()
	{
		string pathInput = pathInputField.text;
		pathInputField.text = "";

		pathInput.Replace(" ", "%20");
		pathInput.Replace(@"\", @"/");

		WWW www = new WWW(filePathPrefix + pathInput);

		while (!www.isDone)
			yield return null;
		image.texture = www.texture;
	}

	private void SetImageUsingTexture2D(string path)
	{

		// read image and store in a byte array
		byte[] byteArray = File.ReadAllBytes(path);
		//create a texture and load byte array to it
		// Texture size does not matter 
		Texture2D newTex = new Texture2D(2, 2);
		// the size of the texture will be replaced by image size
		bool loadingSuccessful = newTex.LoadImage(byteArray);

		if (loadingSuccessful)
		{

			if (newTex.width > newTex.height)
			{
				float newHeight = ((float)newTex.height / (float)newTex.width);
				image.rectTransform.localScale = new Vector2(1, newHeight);
			}
			else if (newTex.height > newTex.width)
			{
				float newWidth = ((float)newTex.width / (float)newTex.height);
				image.rectTransform.localScale = new Vector2(newWidth, 1);
			}

			image.texture = newTex;
		}
	}

	private void SetImageUsingTexture2D()
	{
		string pathInput = pathInputField.text;
		pathInputField.text = "";

		SetImageUsingTexture2D(pathInput);
	}
}
