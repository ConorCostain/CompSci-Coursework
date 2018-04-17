using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler{

	public enum DropZoneType {Canvas, Roster ,Bin, CodeList }
	public DropZoneType ZoneType;

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log(eventData.pointerDrag.name + "Dropped on" + gameObject.name);
		
		switch (ZoneType)
		{
			case DropZoneType.Canvas:
				eventData.pointerDrag.GetComponent<Drag>().ParentToReturnTo = transform;
				return;

			case DropZoneType.Roster:
				Destroy(eventData.pointerDrag.GetComponent<Drag>().draggedObject);
				return;

			case DropZoneType.Bin:
				Destroy(eventData.pointerDrag.GetComponent<Drag>().draggedObject);
				return;

			case DropZoneType.CodeList:
				eventData.pointerDrag.GetComponent<Drag>().ParentToReturnTo = transform;
				return;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		return;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		return;
	}
}
