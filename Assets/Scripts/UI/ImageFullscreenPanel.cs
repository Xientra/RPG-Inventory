using UnityEngine;
using UnityEngine.UI;

public class ImageFullscreenPanel : MonoBehaviour
{
	private RawImage image;

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	public void SetTexture(Texture texture)
	{
		Vector2 imgArea = image.rectTransform.sizeDelta;

		Vector2 imgRes = new Vector2(texture.width, texture.height);

		/*
		if (imgArea.


		if (texture.width > texture.height)
		{
			float newHeight = ((float)texture.height / (float)texture.width);
			image.rectTransform.localScale = new Vector2(1, newHeight);
			
		}
		else if (texture.height > texture.width)
		{
			float newWidth = ((float)texture.width / (float)texture.height);
			image.rectTransform.localScale = new Vector2(newWidth, 1);
		}
		*/

		image.texture = texture;
	}
}
