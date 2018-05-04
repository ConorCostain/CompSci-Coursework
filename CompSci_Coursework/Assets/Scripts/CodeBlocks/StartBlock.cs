using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Use of inheritance
public class StartBlock : MonoBehaviour {


	public void StartInterpreter()
	{
		//Finds the codeList script
		CodeList codeListScript = transform.parent.gameObject.GetComponent<CodeList>();
		if (codeListScript != false)
		{
			PlaySessionManager.ins.CodeInterpreter(codeListScript.codeList);
		}
	}
}
