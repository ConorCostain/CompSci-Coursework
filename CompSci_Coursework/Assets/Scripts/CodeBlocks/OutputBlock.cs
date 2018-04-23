using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputBlock : BaseCodeBlock {

	public override void CodeBlockFunction()
	{
		Debug.Log(param1.GetName() + " = " + param1.GetValue());
	}
}
