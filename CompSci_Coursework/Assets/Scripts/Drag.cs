using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IDragHandler ,IEndDragHandler{

	public Transform ParentToReturnTo;
	public GameObject draggedObject;

	public void OnBeginDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Started");
		
		ParentToReturnTo = transform.parent;
		draggedObject = Instantiate(gameObject, transform.parent.parent);
		draggedObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventdata)
	{
		draggedObject.transform.position = eventdata.position;

	}

	public void OnEndDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Ended");
		draggedObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
		draggedObject.transform.SetParent(ParentToReturnTo);
	}
}
