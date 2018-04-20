using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler{
	// Way to Sort the different Drop zones
	public enum DropZoneType {Canvas, Roster ,Bin, CodeList }
	public DropZoneType zoneType;

	//A unity prefab to be instantiated if a new Code List is needed
	public GameObject codeListPrefab;

	//Method is called when an object is dropped
	public void OnDrop(PointerEventData eventData)
	{
		//Stores the object that was just dropped for easy access
		GameObject draggedObject = eventData.pointerDrag.GetComponent<Drag>().draggedObject;
		//If the Block was from a code list gets a reference to the script before the parent changes
		CodeList oldCodeList = null;
		if (draggedObject.tag == "CodeBlock")
		{
			oldCodeList = draggedObject.transform.parent.GetComponent<CodeList>(); 
		}
		
		Debug.Log(draggedObject.name + "Dropped on" + gameObject.name);

		if (draggedObject != null)	//Defensive programming just in case there is no dragged object somehow
		{
			switch (zoneType)	//Cycles through the four zones it can be dropped on
			{
				//If a block is dropped on the canvas then a new code list is instantiated for it
				case DropZoneType.Canvas:
					CanvasDropZone(draggedObject);
					OldCodeListCheck(oldCodeList, draggedObject);
					break;

				//If anything is dropped on the Roster it is deleted
				case DropZoneType.Roster:
					DestroyDropZone(draggedObject, oldCodeList);
					break;

				//If anything is dropped on the Bin it is deleted
				case DropZoneType.Bin:
					DestroyDropZone(draggedObject, oldCodeList);
					break;
				
				//If dropped on the Code List ensures it is properly added to the list
				case DropZoneType.CodeList:
					CodeListDropZone(draggedObject);
					OldCodeListCheck(oldCodeList, draggedObject);
					break;
			}
		}
	}

	//Method called when block is dropped onto a Canvas Drop Zone
	private void CanvasDropZone(GameObject draggedObject)
	{
		if (draggedObject.tag == "CodeBlock")
		{
			if (codeListPrefab != null) //Ensures there is a link to the prefab for a new Code List
			{
				//Creates a new code list
				GameObject codeList = Instantiate(codeListPrefab, transform.parent);
				//Sets its position to that of the drop location
				codeList.GetComponent<RectTransform>().position = draggedObject.GetComponent<RectTransform>().position;
				//Calls the AddBlock method on the Code List which adds it to the list and changes its parent
				codeList.GetComponent<CodeList>().AddBlock(draggedObject);
			}
			else
			{
				Debug.Log("No Code List Prefab");
			} 
		}
	}

	//Called when block is dropped into a code list
	private void CodeListDropZone(GameObject draggedObject)
	{
		// Gets a reference to the codeList script
		CodeList codeListScript = gameObject.transform.parent.GetComponent<CodeList>();
		if (codeListScript != null) //Ensures that a script is found
		{
			Debug.Log("Code List Script Found");
			//Calls the AddBlock method on the Code List script
			if (draggedObject.tag == "CodeBlock")
			{
				codeListScript.AddBlock(draggedObject); 
			}
			else if(draggedObject.tag == "CodeList")
			{
				CodeList draggedListScript = draggedObject.GetComponent<CodeList>();
				if(draggedListScript != null)
				{
					codeListScript.AddBlock(draggedListScript.codeList);
				}
			}
		}
		else
		{
			Debug.Log("No Code List Script found on Parent");
		}
	}

	//Method called to delete a block
	private void DestroyDropZone(GameObject draggedObject, CodeList oldCodeList)
	{
		OldCodeListCheck(oldCodeList, draggedObject);
		Destroy(draggedObject);
	}

	//Checks to see if the block was previously in a code list and removes it
	private void OldCodeListCheck(CodeList oldCodeList, GameObject draggedObject)
	{
		if (oldCodeList != null)
		{
			Debug.Log("Old CodeList Script Found");
			oldCodeList.RemoveBlock(draggedObject);
		}
	}

	//These methods are required to implement the respective interfaces which are required for the drag and drop system to work
	public void OnPointerEnter(PointerEventData eventData)
	{
		return;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		return;
	}

}
