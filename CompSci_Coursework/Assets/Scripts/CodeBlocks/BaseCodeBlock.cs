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


}
