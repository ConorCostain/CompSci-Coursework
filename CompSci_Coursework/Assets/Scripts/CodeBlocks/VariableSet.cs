using UnityEngine;

//Use of Inheriance
public class VariableSet : BaseCodeBlock {


	public override void CodeBlockFunction()
	{
		SetVariable(param1, param2);
	}


	protected void SetVariable(Variable var1, Variable var2)
	{
		if(var1 != null && var2 != null)
			var1.SetValue(var2.GetValue());
		else
		{
			Debug.Log("One of the values is null for SetVariable()");
		}
	}		

	

}
