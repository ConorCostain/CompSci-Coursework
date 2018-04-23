using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputBlock : BaseCodeBlock {

	public override void CodeBlockFunction()
	{
		PlaySessionManager.ins.Output(param1);
	}
}
