using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IDragHandler ,IEndDragHandler{

	//A Variable to store the object being dragged as it won't always be the original object clicked on
	public GameObject draggedObject;
	
	// Method called when a drag is started on the gameobject with this script on it
	public void OnBeginDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Started");
		draggedObject = gameObject;
	
		//If the block comes from the roster create a duplicate
		if (transform.parent.GetComponent<Drop>() != null)	//Prevents crashing from no drop script
		{
			Debug.Log("Parent has a drop script");
			// If the block is from a roster then it creates a duplicate instead of dragging the
			// original object
			if (transform.parent.GetComponent<Drop>().zoneType == Drop.DropZoneType.Roster)
			{
				Debug.Log("Roster duplicate created");
				draggedObject = Instantiate(gameObject, GetCanvas(transform)); 
			}
		}
		// Prevents the block being dragged being detected when searching for what it's dropped on
		draggedObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
		
	}

	// Method is regularly called throughout the duration of the drag
	public void OnDrag(PointerEventData eventdata)
	{
		// Sets the position of the object to that of the cursor
		draggedObject.transform.position = eventdata.position;
	}

	// Method called after the drag has ended and after the OnDrop Method in the Drop Script
	public void OnEndDrag(PointerEventData eventdata)
	{
		Debug.Log("Drag Ended");
		draggedObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	//The canvas is the highest level in the UI Hierarchy so we can recursively check upwards until found
	private Transform GetCanvas(Transform trans)	//checks through the parents to find the Canvas
	{
		if (trans.gameObject.name == "Canvas")
			return trans;
		else
			return GetCanvas(trans.parent);   //Recursion used to repeat action till done
	}
}
