using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IDragHandler ,IEndDragHandler{

	public Transform ParentToReturnTo;

	public void OnBeginDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Started");
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
		ParentToReturnTo = transform.parent;
		transform.SetParent(transform.parent.parent);
	}

	public void OnDrag(PointerEventData eventdata)
	{
		transform.position = eventdata.position;

	}

	public void OnEndDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Ended");
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
		transform.SetParent(ParentToReturnTo);
	}
}
