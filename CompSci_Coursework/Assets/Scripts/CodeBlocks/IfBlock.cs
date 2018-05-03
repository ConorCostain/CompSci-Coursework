using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Use of inheritance
public class IfBlock : ConditionalCodeBlock {

	//Required for the ConditionalCodeBlock Instructor to run
	public IfBlock(GameObject codeBlock, string comparitor): base(codeBlock, comparitor) { }

	public override void CodeBlockFunction()
	{
		// Gets the comparison function and invokes it using the params
		Comparison comparison = getComparison();
		
		if( comparison.Invoke(param1, param2) )
		{
			Debug.Log("Condition true");
			//if true trigger the codeBlock interpreter passing through the CodeList contained on the object
			PlaySessionManager.ins.CodeInterpreter(codeList.codeList, false);
		}
		else
		{
			Debug.Log("Condition false");
			Debug.Log(param1.GetValue() + "   " + param2.GetValue());
		}
	}


}
