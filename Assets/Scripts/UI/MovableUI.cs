using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MovableUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	private RectTransform rectTransform;

	private bool moving = false;
	private Vector3 offset;


	[Tooltip("If checked this RectTransform can only be dragged, when hovering over the handler")]
	public bool onlyWorkWithHandle = true;
	[Tooltip("A Garphic (image or similar) that functions as a handle to move this RectTransform")]
	public Graphic handle;


	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (moving)
		{
			rectTransform.position = Input.mousePosition + offset;
			//rectTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
			ClampToScreen(rectTransform);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.pointerCurrentRaycast.gameObject == handle.gameObject)
		{
			offset = rectTransform.position - Input.mousePosition;
			moving = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		moving = false;
	}

	private void ClampToScreen(RectTransform t)
	{
		float x = Mathf.Clamp(t.anchoredPosition.x, t.sizeDelta.x / 2, -t.sizeDelta.x / 2);
		float y = Mathf.Clamp(t.anchoredPosition.y, t.sizeDelta.y / 2, -t.sizeDelta.y / 2);

		t.anchoredPosition = new Vector2(x, y);
	}

	private void ClampToScreenHalf(RectTransform t)
	{
		Vector2 rectSize = new Vector2(t.rect.width, t.rect.height) / 2;
		Vector2 rectDelta = t.sizeDelta / 2;

		float x = Mathf.Clamp(t.anchoredPosition.x, rectDelta.x - rectSize.x, -rectDelta.x + rectSize.x);
		float y = Mathf.Clamp(t.anchoredPosition.y, rectDelta.y - rectSize.y, -rectDelta.y + rectSize.y);


		t.anchoredPosition = new Vector2(x, y);
	}

	private void ClampToScreenBy(RectTransform t, float pixel)
	{
		Vector2 rectSize = new Vector2(t.rect.width, t.rect.height) / 2;
		Vector2 rectDelta = t.sizeDelta / 2;

		float x = Mathf.Clamp(t.anchoredPosition.x, rectDelta.x - rectSize.x + pixel, -rectDelta.x + rectSize.x - pixel);
		float y = Mathf.Clamp(t.anchoredPosition.y, rectDelta.y - rectSize.y + pixel, -rectDelta.y + rectSize.y - pixel);

		t.anchoredPosition = new Vector2(x, y);
	}
}
