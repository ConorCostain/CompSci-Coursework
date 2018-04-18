using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeList : MonoBehaviour {

	public GameObject paddingPrefab;
	public List<GameObject> codeList = new List<GameObject>();

	private GameObject paddingBlock;


	//Is called when scrip is initialized/game starts
	public void Start()
	{
		if(paddingPrefab != null)
		{
			paddingBlock = Instantiate(paddingPrefab, transform);
		}
		
		return;
	}

	//makes sure that the new code block is above the padding block for new blocks
	//to be dropped onto
	public void AddBlock(GameObject block)
	{
		Debug.Log(block.name);
		codeList.Add(block);
		Debug.Log("Added to List");
		block.transform.SetParent(transform);
		Debug.Log("Parent set as code list");
		paddingBlock.transform.SetAsLastSibling();
		
	}

	public void RemoveBlock(GameObject block)
	{
		Debug.Log("Removing block from code list");
		codeList.Remove(block);
		if(codeList.Count < 1)
		{
			Debug.Log("Destroying Code List");
			Destroy(gameObject);
		}
		return;
	}


}
