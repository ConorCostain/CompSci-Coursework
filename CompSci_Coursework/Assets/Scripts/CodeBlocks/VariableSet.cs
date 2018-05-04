using UnityEngine;

//Use of Inheritance
public class VariableSet : BaseCodeBlock {

	//Use of Method Overriding
	public override void CodeBlockFunction()
	{
		SetVariable(param1, param2);
	}

	protected void SetVariable(Variable var1, Variable var2)
	{
		//Ensures values are not null to prevent throwing an exception
		if(var1 != null && var2 != null)
			var1.SetValue(var2.GetValue());
	}		

	

}
