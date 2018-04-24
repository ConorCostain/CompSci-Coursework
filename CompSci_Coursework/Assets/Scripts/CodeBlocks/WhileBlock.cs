using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileBlock : ConditionalCodeBlock
{

	//Required for the ConditionalCodeBlock Instructor to run
	public WhileBlock(GameObject codeBlock, string comparitor) : base(codeBlock, comparitor) { }

	public override void CodeBlockFunction()
	{
		//Gets the comparison function and invokes it using the params
		Comparison comparison = getComparison();
		//Countdown prevents the user getting stuck in an infinite loop and exits after 100 repeats
		int countdown = 100;
		while (comparison(param1, param2) && countdown > 0)
		{
			Debug.Log("Condition true");
			//if true trigger the codeBlock interpreter passing through the CodeList contained on the object
			PlaySessionManager.ins.CodeInterpreter(codeList.codeList, false);
			countdown--;
		}

	}

}
