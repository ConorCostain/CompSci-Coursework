using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour {

	private Button playButton;
	
	public void Start()
	{
		playButton = gameObject.GetComponent<Button>();
		if(playButton != null)
		{
			//Adding an event listener
			playButton.onClick.AddListener(OnClick);
		}
	}

	public static void OnClick()
	{
		Debug.Log("On Click called");
		//Finds all of the start blocks and calls there start interpreter method.
		
		IEnumerable<GameObject> startBlocks = GameObject.FindGameObjectsWithTag("CodeBlock").Where(b => b.GetComponent<StartBlock>() != null);
		//Checks that there arent more than one start block on the canvas

		ObjectiveManager objManager = FindObjectOfType<ObjectiveManager>();
		if (startBlocks.Where(b => b.transform.parent.tag == "CodeList").Count() > 1)
		{
			objManager.SetInstruction("Too many start blocks on canvas!");
			Debug.Log("Too many start blocks on canvas!");
		}
		else if (startBlocks.Where(b => b.transform.parent.tag == "CodeList").Count() > 0)
		{
			objManager.SetInstruction();
			//Finds the start block that is 
			startBlocks.Where(b => b.transform.parent.tag == "CodeList")
				.FirstOrDefault().GetComponent<StartBlock>().StartInterpreter();
		}
		else
		{
			Debug.Log("No start block on canvas!");
			objManager.SetInstruction("No start block on canvas!");
		}
	}

	

}
