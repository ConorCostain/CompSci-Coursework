using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler{

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log(eventData.pointerDrag.name + "Dropped on" + gameObject.name);
		eventData.pointerDrag.GetComponent<Drag>().ParentToReturnTo = transform;
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
