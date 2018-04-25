using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlaySessionManager : MonoBehaviour {

	//Declaration of Lists and Variables

	private List<int> inputList = new List<int>();
	private List<int> expectedOutputs = new List<int>();
	private List<int> outputList = new List<int>();
	private ObjectiveManager objManager;
	private TMP_Text outputText;
	public List<Variable> variableList = new List<Variable>();
	
	//One method for the Objective manager to call to pass through the required values
	public void ObjectivesSetup(List<int> inputList, List<int> expectedOutputs,ObjectiveManager objManager, TMP_Text outputText)
	{
		this.inputList = inputList;
		this.expectedOutputs = expectedOutputs;
		this.objManager = objManager;
		this.outputText = outputText;
	}

	//Adds the value to the List and also adds it to the linked UI element to display the output to the user
	public void Output(Variable output)
	{
		Debug.Log("Output: " + output.GetValue().ToString());
		outputList.Add(output.GetValue());
		if(objManager != null && outputText != null)
		{
			objManager.AddToTMP<int>(outputText, output.GetValue());
		}
		WinCheck();
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

	
	private void WinCheck()
	{

	}

	//The main function which takes in a CodeList and Runs the code functions of it
	public void CodeInterpreter(Queue<GameObject> codeList, bool useInput = true)
	{
		if (useInput && inputList.Count > 0)
		{
			int count = 1;
			foreach (int input in inputList)
			{
				Debug.Log("Code Interpreter Run :" + count++);
				inputVariableSetup(input);
				ExecuteCodeList(codeList);
			} 
		}
		else
		{
			Debug.Log("Code Interpreter ran without input");
			ExecuteCodeList(codeList);
		}
	}

	private void inputVariableSetup(int input)
	{
		// Checks whether the input variable is in the variable list yet and if not adds it
			//Use of Lambda Expresion
		if (!(variableList.Where(v => v.GetName() == "input").Count() > 0))
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
		Debug.Log("CodeList count = " + codeList.Count);
		//Temporary variables to hold values
		GameObject temp = null;
		CodeBlock tempScript = null;
		//Code List put into a temp so that when dequeued it does not break the original queue
		//and then it the process of going through the code list may be repeated if necessary
		//Creating a deep clone using reflection
		Queue<GameObject> tempList = new Queue<GameObject>(codeList);


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
