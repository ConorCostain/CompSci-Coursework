using UnityEngine;
//Use of inheritance
public class IfBlock : ConditionalCodeBlock {

	//Required for the ConditionalCodeBlock Instructor to run
	public IfBlock(GameObject codeBlock, string comparitor): base(codeBlock, comparitor) { }

	//Use of Method Overriding
	public override void CodeBlockFunction()
	{
		// Gets the comparison function and invokes it using the params
		Comparison comparison = getComparison();
		
		if( comparison.Invoke(param1, param2) )
		{
			//if true trigger the codeBlock interpreter passing through the CodeList contained on the object
			PlaySessionManager.ins.CodeInterpreter(codeList.codeList, false);
		}
	}


}
