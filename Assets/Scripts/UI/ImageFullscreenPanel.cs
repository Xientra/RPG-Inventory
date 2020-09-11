using UnityEngine;
using UnityEngine.UI;

public class ImageFullscreenPanel : MonoBehaviour
{
	public RawImage image;

	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}
