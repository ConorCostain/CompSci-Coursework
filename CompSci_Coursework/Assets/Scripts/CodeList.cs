using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeList : MonoBehaviour {

	//Stores the Prefab to instantiate a padding block and handle
	public GameObject handlePrefab;
	public GameObject paddingPrefab;
	//A reference to the padding and handle block once instantiated
	private GameObject handleBlock;
	private GameObject paddingBlock;
	//The list providing reference to each of the code blocks and the order to process them
	public Queue<GameObject> codeList = new Queue<GameObject>();

	//Is called when script is initialized/game starts
	public void Start()
	{
		if(handlePrefab != null)
		{
			handleBlock = Instantiate(handlePrefab, transform);
			handleBlock.transform.SetAsFirstSibling();
		}
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

	//Method overloading so that same method may be used to add a queue or a single block
	public void AddBlock(Queue<GameObject> newCodeQueue)
	{

		//Lambda expression used to quickly run through the queue and add it to the main code list
		newCodeQueue.ToList<GameObject>().ForEach(block => 
			{
				codeList.Enqueue(block);
				block.transform.SetParent(transform);
			});
		try
		{   //If the Padding block is already last sibling it will throw an exception
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
