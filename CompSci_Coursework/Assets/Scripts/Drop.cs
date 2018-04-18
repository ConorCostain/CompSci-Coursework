using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler{

	public enum DropZoneType {Canvas, Roster ,Bin, CodeList }
	public DropZoneType zoneType;
	public GameObject codeListPrefab;

	public void OnDrop(PointerEventData eventData)
	{
		GameObject draggedObject = eventData.pointerDrag.GetComponent<Drag>().draggedObject;
		CodeList oldCodeListScript = draggedObject.transform.parent.GetComponent<CodeList>();
		Debug.Log(draggedObject.name + "Dropped on" + gameObject.name);
		
		switch (zoneType)
		{
			case DropZoneType.Canvas:
				if (codeListPrefab != null)
				{
					GameObject codeList = Instantiate(codeListPrefab, transform.parent);
					codeList.GetComponent<RectTransform>().localPosition = draggedObject.GetComponent<RectTransform>().localPosition;
					codeList.GetComponent<CodeList>().AddBlock(draggedObject); 
				}
				else
				{
					Debug.Log("No Code List Prefab");
				}
				return;

			case DropZoneType.Roster:
				Destroy(draggedObject);
				return;

			case DropZoneType.Bin:
				Destroy(draggedObject);
				return;

			case DropZoneType.CodeList:
				CodeList codeListScript = gameObject.transform.parent.GetComponent<CodeList>();
				if (codeListScript != null)
				{
					Debug.Log("Code List Script Found");
					codeListScript.AddBlock(draggedObject);
				}
				else
				{
					Debug.Log("No Code List Script found on Parent");
				}
				return;
		}
		//Checks to see if the block was previously in a code list and removes it
		
		if (oldCodeListScript != null)
		{
			oldCodeListScript.RemoveBlock(draggedObject);
			
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
