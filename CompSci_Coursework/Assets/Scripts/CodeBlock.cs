using System.Linq;
using UnityEngine;
using TMPro;

public class CodeBlock : MonoBehaviour {

	public enum CodeBlockType {Add, Subtract, Multiply, Divide, VariableSet, If, While, Output}
	public CodeBlockType blockType;

	private bool useComparitor = false;

	public delegate void CodeFunction();

	private delegate BaseCodeBlock ConditionalBlockSetup();
	private ConditionalBlockSetup condBlockSetup;

	public Variable param1;
	public Variable param2;
	public Variable param3;

	public GameObject inputField1;
	public GameObject inputField2;
	public GameObject inputField3;

	private BaseCodeBlock codeBlock = null;


	public void Start()
	{
		
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
				useComparitor = true;
				break;
			case CodeBlockType.While:
				
				condBlockSetup = () =>
				{
					return new WhileBlock(gameObject, inputField3.GetComponent<TMP_InputField>().text);
				};
				useComparitor = true;
				break;
		}
	}

	public CodeFunction GetCodeFunction()
	{
		// Before The code block is returned it is checked to make sure all parameters are correct
		if(inputField1 != null && inputField1.GetComponent<TMP_InputField>() != null)
		{
			setParam(1, inputField1.GetComponent<TMP_InputField>().text);
		}
		if (inputField2 != null && inputField1.GetComponent<TMP_InputField>() != null)
		{
			setParam(2, inputField2.GetComponent<TMP_InputField>().text);
		}
		if (useComparitor)
		{
			codeBlock = condBlockSetup.Invoke();
		}
		else if(inputField3 != null && inputField1.GetComponent<TMP_InputField>() != null)
		{
			setParam(3, inputField3.GetComponent<TMP_InputField>().text);
		}

		//Sets the params in the object
		codeBlock.SetParams(param1, param2, param3);

		return new CodeFunction(codeBlock.CodeBlockFunction);
	}

	public void setParam(int paramNumber, string paramData)
	{
		Variable tempParam;
		paramData = paramData.ToLower();

		//Use of Try Catch for defensive programming
		try
		{
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
		

		switch (paramNumber)
		{
			case 1:
				param1 = tempParam;
					break;
			case 2:
				param2 = tempParam;
				break;

			case 3:
				param3 = tempParam;
				break;

		}
	}

}
