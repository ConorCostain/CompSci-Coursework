
//Use of Inheriance
public class VariableSet : BaseCodeBlock {

	protected Variable param2;

	public void SetParam2(Variable param)
	{
		param2 = param;

	}

	public Variable GetParam2()
	{
		return param2;

	}

	public override void CodeBlockFunction()
	{
		SetVariable(param1, param2);
	}

	//replaces the old SetParam Function to setup the second parameter
	public new void SetParams(Variable param1, Variable param2, Variable param3)
	{
		this.param1 = param1;
		this.param2 = param2;
	}

	protected void SetVariable(Variable var1, Variable var2)
	{
		var1.SetValue(var2.GetValue());
	}

	

}
