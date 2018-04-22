﻿//Use of Inheriance
public class OperatorCodeBlock :VariableSet {

	protected delegate Variable OperatorFunction(Variable var1, Variable var2);

	protected Variable param3;

	public void SetParam3(Variable param)
	{
		param3 = param;
		
	}

	public Variable GetParam3()
	{
		return param3;
	}

	//Use of Method overloading to allow passing through of a Delegate
	protected void SetVariable(Variable var1, Variable var2, Variable var3, OperatorFunction opFunc)
	{
		var1 = opFunc(var2, var3);
	}
}