using System.Collections;
using System.Collections.Generic;

//Use of an Abstract class
public abstract class BaseCodeBlock{

	protected Variable param1;

	public void SetParam1(Variable param)
	{
		param1 = param;
		
	}

	public Variable GetParam1()
	{
		return param1;

	}

	//Abstract method
	public abstract void CodeBlockFunction();

	//still takes three parameters so all codeblocks can be sent all three and the uneeded ones will be ignored
	public void SetParams(Variable param1, Variable param2, Variable param3)
	{
		this.param1 = param1;
	}


}
