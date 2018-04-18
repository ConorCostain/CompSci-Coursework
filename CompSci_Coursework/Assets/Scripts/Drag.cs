using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IDragHandler ,IEndDragHandler{

	
	public GameObject draggedObject;
	

	public void OnBeginDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Started");
		draggedObject = gameObject;
		

		
		
		//If the block comes from the roster create a duplicate
		if (transform.parent.GetComponent<Drop>() != null)
		{
			if (transform.parent.GetComponent<Drop>().zoneType == Drop.DropZoneType.Roster)
				draggedObject = Instantiate(gameObject, Canvas(transform)); 
		}
		
		

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
		
	}

	private Transform Canvas(Transform trans)	//checks through the parents to find the Canvas
	{
		if (trans.gameObject.name == "Canvas")
			return trans;
		else
			return Canvas(trans.parent);   //Recursion used to repeat action till done
	}
}
