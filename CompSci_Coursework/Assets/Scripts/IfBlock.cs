using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfBlock : ScriptableObject, ICodeBlock {

	public string blockType { get; set; } // Implementation of ICodeBlock blockType

	private void Awake()
	{
		blockType = "if";
	}
	
	public void blockFunction()	//Implementation of ICodeBlock blockFunction
	{

	}
}
