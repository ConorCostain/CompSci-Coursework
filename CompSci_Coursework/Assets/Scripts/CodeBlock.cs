using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CodeBlock : MonoBehaviour {

	public enum CodeBlockType {Add, Subtract, Multiply, Divide, VariableSet, If, While, Output}
	public CodeBlockType blockType;

	public delegate void CodeFunction();

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
				codeBlock = new Add();
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
		}
	}

	public CodeFunction GetCodeFunction()
	{
		// Before The code block is returned it is checked to make sure all parameters are correct
		if(inputField1 != null || inputField1.GetComponent<TMP_InputField>() != null)
		{
			setParam(1, inputField1.GetComponent<TMP_InputField>().text);
		}
		if (inputField2 != null || inputField1.GetComponent<TMP_InputField>() != null)
		{
			setParam(2, inputField1.GetComponent<TMP_InputField>().text);
		}
		if (inputField3 != null || inputField1.GetComponent<TMP_InputField>() != null)
		{
			setParam(3, inputField1.GetComponent<TMP_InputField>().text);
		}

		//Sets the params in the object
		codeBlock.SetParams(param1, param2, param3);

		return new CodeFunction(codeBlock.CodeBlockFunction);
	}

	public void setParam(int paramNumber, string paramData)
	{
		Variable tempParam;

		//Use of Try Catch for defensive programming
		try
		{
			tempParam = new Variable(int.Parse(paramData));
		}
		catch
		{
			//use of Lambda Expresion
			tempParam = PlaySessionManager.ins.variableList
				.Where(v => v.GetName() == paramData.ToLower()).FirstOrDefault();

			if(tempParam == null)
			{
				PlaySessionManager.ins.variableList
					.Add(new Variable(paramData, PlaySessionManager.ins.variableList.Count));
			}
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
