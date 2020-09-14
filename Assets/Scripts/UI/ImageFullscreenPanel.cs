using UnityEngine;
using UnityEngine.UI;

public class ImageFullscreenPanel : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField]
	private RawImage image;
#pragma warning restore 0649

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	public void SetTexture(Texture texture)
	{
		Vector2 imgArea = image.canvas.GetComponent<RectTransform>().sizeDelta;

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
}
