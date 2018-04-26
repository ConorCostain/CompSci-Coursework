using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlock : MonoBehaviour {

	//Enumerator to Decide the type of code block this object will be ( Set in Unity Editor)
	public enum CodeBlockType {Add, Subtract, Multiply, Divide, VariableSet, If, While, Output}
	public CodeBlockType blockType;

	//The The CodeFunction delegate passed through to the Code Interpreter
	public delegate void CodeFunction();

	//Used to Instantiate the derived classes from CondtionalCodeBlock at a later time than when the other code blocks are instantiated
	private delegate BaseCodeBlock ConditionalBlockSetup();
	private ConditionalBlockSetup condBlockSetup;

	//References to the Input Fields, defined in Unity Editor
	public GameObject inputField1;
	public GameObject inputField2;
	public GameObject inputField3;

	//The main object of the script containing the code block function that is executed by the code interpreter
	private BaseCodeBlock codeBlock = null;

	//Start method called when level starts
	public void Start()
	{
		// Instantiates a code block relative to the block type
		switch (blockType)
		{
			case CodeBlockType.Add:
				codeBlock = new Add();
				break;

			case CodeBlockType.Subtract:
				codeBlock = new Subtract();
				break;

			case CodeBlockType.Multiply:
				codeBlock = new Multiply();
				break;

			case CodeBlockType.Divide:
				codeBlock = new Divide();
				break;

			case CodeBlockType.VariableSet:
				codeBlock = new VariableSet();
				break;

			case CodeBlockType.Output:
				codeBlock = new OutputBlock();
				break;
			//Contents of these cases sent to a delegate as has to be run later when the code list is being ran
			//Use of an Anynmous delegate
			//Using this as it will only be used once and there will be variation between a couple anonymous methods
			case CodeBlockType.If:
				condBlockSetup = () =>
				{
					return new IfBlock(gameObject, inputField3.GetComponent<TMP_InputField>().text);
				};
				break;
			case CodeBlockType.While:
				
				condBlockSetup = () =>
				{
					return new WhileBlock(gameObject, inputField3.GetComponent<TMP_InputField>().text);
				};
				break;
		}
	}

	//gets the code block function and returns it to the Code Interpreter
	public CodeFunction GetCodeFunction()
	{
		//Gets the Variables after they have been parsed from text to Variables and added to a queue
		Queue<Variable> parameters = VariableSetup();

		//Sets the params in the object
		codeBlock.SetParams(parameters);

		return new CodeFunction(codeBlock.CodeBlockFunction);
	}

	//Enqueues the Variables once parsed
	private Queue<Variable> VariableSetup()
	{
		Queue<Variable> varQueue = new Queue<Variable>();

		// Before The code block is returned it is checked to make sure all parameters are correct
		if (inputField1 != null && inputField1.GetComponent<TMP_InputField>() != null)
		{
			varQueue.Enqueue (GetVariable(inputField1.GetComponent<TMP_InputField>().text));
		}
		if (inputField2 != null && inputField1.GetComponent<TMP_InputField>() != null)
		{
			varQueue.Enqueue(GetVariable(inputField2.GetComponent<TMP_InputField>().text));
		}
		if (condBlockSetup != null)
		{
			codeBlock = condBlockSetup.Invoke();
		}
		else if (inputField3 != null && inputField1.GetComponent<TMP_InputField>() != null)
		{
			varQueue.Enqueue(GetVariable(inputField3.GetComponent<TMP_InputField>().text));
		}

		while(varQueue.Count < 3)
		{
			//If the varQueue doesnt have 3 it fills the rest with blank Variables
			varQueue.Enqueue(new Variable());
		}
		return varQueue;
	}

	//Parses the variable from a string to a Variable Object
	private Variable GetVariable(string paramData)
	{
		Variable tempParam;
		paramData = paramData.ToLower();

		//Use of Try Catch for defensive programming
		try
		{
			//If the string can be parsed to an Int Instantiated a Variable with the value of said Int
			tempParam = new Variable(int.Parse(paramData));
	
		}
		catch
		{
			//If there is a variable without the name
			if(PlaySessionManager.ins.variableList.Where(v => v.GetName() == paramData).Count() == 0)
			{
				//Create a new variable with said name
				PlaySessionManager.ins.variableList
					.Add(new Variable(paramData, -1));
			}
			//Get a set the value and name from the variable with the name in the list
			//use of Lambda Expresion
			tempParam = PlaySessionManager.ins.variableList.Where(v => v.GetName() == paramData).FirstOrDefault();
			//Set the index of the parameter to that of the index of the variable in the variable list
			tempParam = new Variable(paramData, PlaySessionManager.ins.variableList.IndexOf(tempParam), tempParam.GetValue());
			
		}


		return tempParam;
	}

}
