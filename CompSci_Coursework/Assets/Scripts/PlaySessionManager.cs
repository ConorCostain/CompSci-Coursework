using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySessionManager : MonoBehaviour {

	//Declaration of Lists and Variables

	private List<int> inputList = new List<int>();
	private List<int> expectedOutputs = new List<int>();
	private List<int> outputList = new List<int>();
	public List<Variable> variableList = new List<Variable>();
	
	//Set functions for InputList and Expected Outputs
	public void SetInputList(List<int> inputList)
	{
		this.inputList = inputList;
	}

	public void SetexpectedOutputsList(List<int> expectedOutputs)
	{
		this.expectedOutputs = expectedOutputs;
	}

	//Method overloading allowing for a singular element to be passed through
	public void SetInputList(int input)
	{
		this.inputList.Add(input);
	}

	public void SetExpectedOutputsList(int output)
	{
		this.expectedOutputs.Add(output);
	}

	public void Output(Variable output)
	{
		Debug.Log("Output: " + output.GetValue().ToString());
		outputList.Add(output.GetValue());
	}

	// General Setup of PlaySessionManager
	public static PlaySessionManager ins;

	void Awake()	// Runs when script is loaded, before game runs
	{
		// Singleton Pattern to have a single instance of the PlaySession manager
		if(ins == null)	//Checks whether there is already an Instance,
		{
			ins = this;	//If there is no instance sets itself to be the instance
			DontDestroyOnLoad(gameObject);	//Makes it so the object will persist through scenes
		}
		else if(ins != this)	//if the instance is set to something other than this object
		{
			Destroy(gameObject);	//destroy this object as there is already a different instance
			return;					// returns to prevent running any more code in this method
									// as it can take a short time for the object to be destroyed
		}

		
	}

	public static void StartButton()
	{
		Debug.Log("OnClick called");
		GameObject.FindGameObjectsWithTag("CodeBlock").Where(b => b.GetComponent<StartBlock>() != null)
			.ToList().ForEach(b => b.GetComponent<StartBlock>().StartInterpreter());
	}

	

	//The main function which takes in a CodeList and Runs the code functions of it
	public void CodeInterpreter(Queue<GameObject> codeList, bool useInput = true)
	{
		if (useInput && inputList.Count > 0)
		{
			foreach (int input in inputList)
			{
				inputVariableSetup(input);
				ExecuteCodeList(codeList);
			} 
		}
		else
		{
			ExecuteCodeList(codeList);
		}
	}

	private void inputVariableSetup(int input)
	{
		// Checks whether the input variable is in the variable list yet and if not adds it
			//Use of Lambda Expresion
		if (variableList.Where(v => v.GetName() == "input").Count() > 0)
		{
			variableList.Add(new Variable("input", -1, input));
		}
		//If the input variable is in the list then the value is set to the current value
		else
		{
			//Use of Lambda Expression
			variableList.Where(v => v.GetName() == "input").FirstOrDefault().SetValue(input);
		}
	}

	private void ExecuteCodeList(Queue<GameObject> codeList)
	{
		//Temporary variables to hold values
		GameObject temp = null;
		CodeBlock tempScript = null;
		//Code List put into a temp so that when dequeued it does not break the original queue
		//and then it the process of going through the code list may be repeated if necessary
		Queue<GameObject> tempList = codeList;


		//Loops through all elements of the queue
		while (tempList.Count > 0)
		{
			temp = tempList.Dequeue();
			tempScript = temp.GetComponent<CodeBlock>();
			if (tempScript != null)
			{
				//Returns and Invokes the delegate containing reference to the code block function
				tempScript.GetCodeFunction().Invoke();
			}
		}
	}

}
