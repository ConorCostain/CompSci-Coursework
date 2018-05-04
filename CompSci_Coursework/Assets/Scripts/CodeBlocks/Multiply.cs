//Use of Inheritance
public class Multiply : OperatorCodeBlock
{
	//Use of Method Overriding
	public override void CodeBlockFunction()
	{
		//Use of Anonymous function and Delegates
		SetVariable(param1, param2, param3, new OperatorFunction((x, y) => x * y));
	}
}
