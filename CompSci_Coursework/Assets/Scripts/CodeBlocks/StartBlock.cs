﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour {


	public void StartInterpreter()
	{
		Debug.Log("Start Interpreter Called");
		CodeList codeListScript = transform.parent.gameObject.GetComponent<CodeList>();
		if (codeListScript != false)
		{
			PlaySessionManager.ins.CodeInterpreter(codeListScript.codeList);
		}
	}
}
