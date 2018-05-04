using System.Collections;
using System.Collections.Generic;

//Use of an Abstract class
public abstract class BaseCodeBlock{

	//Declaring parameters
	protected Variable param1 { get; set; }

	protected Variable param2 { get; set; }

	protected Variable param3 { get; set; }

	//Abstract method
	public abstract void CodeBlockFunction();

	//still takes three parameters so all codeblocks can be sent all three and the uneeded ones will be ignored
	public void SetParams(Queue<Variable> parameters)
	{
		this.param1 = parameters.Dequeue();
		this.param2 = parameters.Dequeue();
		this.param3 = parameters.Dequeue();
	}


}
