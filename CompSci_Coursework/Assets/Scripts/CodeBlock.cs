using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeBlock : MonoBehaviour {

	public Variable param1;
	public Variable param2;
	public Variable param3;

	private BaseCodeBlock codeBlock = null;

	private enum CodeBlockType {Add, Subtract, Multiply, Divide, VariableSet, If, While, Output}

	private CodeBlockType blockType;

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

	public BaseCodeBlock GetCodeBlock()
	{
		return codeBlock;
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
