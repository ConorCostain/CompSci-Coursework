using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeList : MonoBehaviour
{

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
		if (handlePrefab != null)
		{
			handleBlock = Instantiate(handlePrefab, transform);
			handleBlock.transform.SetAsFirstSibling();
		}

		if (paddingPrefab != null)
		{
			paddingBlock = Instantiate(paddingPrefab, transform);
		}
		//If there is no padding block prefab then check through the children to see if there is one
		//if so enable the block
		else 
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				if(transform.GetChild(i).name == "PaddingBlock")
				{
					paddingBlock = transform.GetChild(i).gameObject;
					paddingBlock.SetActive(true);
					break; //breaks from loop to prevent wasting time when a padding block has already been found
				}
			}
		}
	}

	//makes sure that the new code block is above the padding block for new blocks
	//to be dropped onto
	public void AddBlock(GameObject block)
	{
		Debug.Log(block.name);
		codeList.Enqueue(block);
		block.transform.SetParent(transform);
		try
		{   //If the Padding block is already last sibling it will throw an exception
			paddingBlock.transform.SetAsLastSibling();
		}
		catch { }
	}

	//Method overloading so that same method may be used to add a queue or a single block
	public void AddBlock(CodeList secondCodeList)
	{

		//Lambda expression used to quickly run through the queue and add it to the main code list
		secondCodeList.codeList.ToList<GameObject>().ForEach(block =>
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
		//Destroys old Code List once added to the new one
		Destroy(secondCodeList.gameObject);
	}

	//Removes the block from the queue and if the queue is now empty the code list is destroyed
	public void RemoveBlock(GameObject block)
	{
		codeList = ElementExtractor(codeList, block);
		Debug.Log("Removing block from code list");

		if (codeList.Count < 1)
		{
			Debug.Log("Destroying Code List");
			Destroy(gameObject);
		}
		return;
	}

	//Removes a element from a queue without altering the order of it
	//Advanced Algorithm used
	private Queue<GameObject> ElementExtractor(Queue<GameObject> queue, GameObject element)
	{
		Queue<GameObject> newQueue = new Queue<GameObject>();
		GameObject temp;

		//Runs through every element until fully dequeued
		while (queue.Count > 0)
		{
			temp = queue.Dequeue();

			if (element.Equals(temp))
			{
				//If the item is found, enqueue the rest of the queue into the newQueue
				while (queue.Count > 0)
				{
					newQueue.Enqueue(queue.Dequeue());
				}
			}
			else
			{	//If the element is not a match then enqueue it to the newQueue
				newQueue.Enqueue(temp);
			}
		}
		return newQueue;
	}
}