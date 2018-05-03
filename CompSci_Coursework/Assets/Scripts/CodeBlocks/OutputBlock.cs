using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Use of inheritance
public class OutputBlock : BaseCodeBlock {

	public override void CodeBlockFunction()
	{
		//Calls the output method in PlaySessionManager to handle the output
		PlaySessionManager.ins.Output(param1);
	}
}
