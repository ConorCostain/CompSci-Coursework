//Use of inheritance
public class OutputBlock : BaseCodeBlock {

	//Use of Method Overriding
	public override void CodeBlockFunction()
	{
		//Calls the output method in PlaySessionManager to handle the output
		PlaySessionManager.ins.Output(param1);
	}
}
