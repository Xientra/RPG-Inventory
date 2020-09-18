using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MovableUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool moving = false;

	private RectTransform rectTransform;
	private Vector3 offset;

	[Tooltip("If checked this RectTransform can only be dragged, when hovering over the handler")]
	public bool onlyWorkWithHandle = true;
	[Tooltip("A Garphic (image or similar) that functions as a handle to move this RectTransform")]
	public Graphic handle;


	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (moving)
		{
			rectTransform.position = Input.mousePosition + offset;
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
}
