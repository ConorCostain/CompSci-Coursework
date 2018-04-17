using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IDragHandler ,IEndDragHandler{

	public Transform ParentToReturnTo;
	public GameObject draggedObject;
	

	public void OnBeginDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Started");
		
		//ParentToReturnTo = transform.parent;

		//If the block comes from the roster create a duplicate
		if (transform.parent.GetComponent<Drop>().ZoneType == Drop.DropZoneType.Roster)
			draggedObject = Instantiate(gameObject, Canvas(transform));
		else
			draggedObject = gameObject;

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

	private Transform Canvas(Transform trans)	//checks through the parents to find the Canvas
	{
		if (trans.gameObject.name == "Canvas")
			return trans;
		else
			return Canvas(trans.parent);   //Recursion used to repeat action till done
	}
}
