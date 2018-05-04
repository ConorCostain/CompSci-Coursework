//Use of Inheritance
public abstract class OperatorCodeBlock :VariableSet {

	protected delegate Variable OperatorFunction(Variable var1, Variable var2);

	//Use of Method overloading to allow passing through of a Delegate
	protected void SetVariable(Variable var1, Variable var2, Variable var3, OperatorFunction opFunc)
	{
		var1.SetValue(opFunc(var2, var3).GetValue());
	}


}
