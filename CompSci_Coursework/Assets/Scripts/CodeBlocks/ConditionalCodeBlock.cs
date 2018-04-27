using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalCodeBlock : BaseCodeBlock{

	protected CodeList codeList;
	protected string comparitor;

	protected delegate bool Comparison(Variable var1, Variable var2);

	public ConditionalCodeBlock(GameObject codeBlock, string comparitor)
	{
		//Gets reference to codeList
		codeList = codeBlock.GetComponent<CodeList>();
		this.comparitor = comparitor;
	}

	protected Comparison getComparison()
	{
		Comparison comparison = (x, y) => false;
		Debug.Log(comparitor);
		comparitor = comparitor.Trim();
		//Use of Anonymous functions and delegates
		switch (comparitor)
		{
			//Sets the delegate comparison to the corresponding logical operation
			case "==":
				comparison = (x, y) => (x == y);
				break;
			case "=":
				comparison = (x, y) => (x == y);
				break;
			case "!=":
				Debug.Log("!=");
				comparison = (x, y) => (x != y);
				break;
			case ">":
				comparison = (x, y) => (x > y);
				break;
			case "<":
				comparison = (x, y) => (x < y);
				break;
			case ">=":
				comparison = (x, y) => (x >= y);
				break;
			case "<=":
				comparison = (x, y) =>( x <= y);
				break;
		}

		return comparison;
	}
}
