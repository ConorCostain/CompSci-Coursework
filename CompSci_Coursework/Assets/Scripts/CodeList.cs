using System.Collections.Generic;
using UnityEngine;

public class CodeList : MonoBehaviour {

	//Stores the Prefab to instantiate a padding block
	public GameObject paddingPrefab;
	//A reference to the padding block once instantiated
	private GameObject paddingBlock;
	//The list providing reference to each of the code blocks and the order to process them
	public Queue<GameObject> codeList = new Queue<GameObject>();

	//Is called when script is initialized/game starts
	public void Start()
	{
		if(paddingPrefab != null)
		{
			paddingBlock = Instantiate(paddingPrefab, transform);
		}
	}

	//makes sure that the new code block is above the padding block for new blocks
	//to be dropped onto
	public void AddBlock(GameObject block)
	{
		Debug.Log(block.name);
		codeList.Enqueue(block);
		Debug.Log("Added to List");
		block.transform.SetParent(transform);
		Debug.Log("Parent set as code list");
		try
		{	//If the Padding block is already last sibling it will throw an exception
			paddingBlock.transform.SetAsLastSibling();
		}
		catch
		{
			Debug.Log("Padding block already last sibling");
		}
	}

	//Removes the block from the queue and if the queue is now empty the code list is destroyed
	public void RemoveBlock(GameObject block)
	{
		codeList = ElementExtractor<GameObject>(codeList, block);
		Debug.Log("Removing block from code list");
		
		if(codeList.Count < 1)
		{
			Debug.Log("Destroying Code List");
			Destroy(gameObject);
		}
		return;
	}

	//Removes a element from a queue without altering the order of it
	private Queue<T> ElementExtractor<T>(Queue<T> queue, T element)
	{
		Queue<T> newQueue = new Queue<T>();
		T temp;

		while(queue.Count > 0)
		{
			temp = queue.Dequeue();

			if(element.Equals(temp) )
			{
				while(queue.Count > 0)
				{
					newQueue.Enqueue(queue.Dequeue());
				}
			}
			else
			{
				newQueue.Enqueue(temp);
			}
		}
		return newQueue;
	}
}
