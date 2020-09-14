using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ImageFullscreenPanel : MonoBehaviour
{
	[SerializeField]
	private RawImage image;

	Vector2 imgArea;


	public void Start()
	{
		//imgArea = image.rectTransform.sizeDelta;
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	public void SetTexture(Texture texture)
	{
		imgArea = image.rectTransform.sizeDelta;
		imgArea = image.canvas.GetComponent<RectTransform>().sizeDelta;

		Vector2 imgRes = new Vector2(texture.width, texture.height);

		float imgAspect = imgRes.x / imgRes.y;
		float areaAspect = imgArea.x / imgArea.y;

		if (imgAspect < areaAspect)
		{
			image.rectTransform.sizeDelta = new Vector2(imgArea.y * imgAspect, imgArea.y);
		}
		else if (imgAspect > areaAspect)
		{
			image.rectTransform.sizeDelta = new Vector2(imgArea.x, imgArea.x / imgAspect);
		}


		image.texture = texture;
	}

	private void ScaleBasedOnWidth()
	{
	
	}

	private void ScaleBasedOnHHeight()
	{
	
	}
}
