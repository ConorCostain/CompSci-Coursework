using System;

public class Variable{

	protected int value;

	public void SetValue(int value)
	{
		this.value = value;
	}

	public int GetValue()
	{
		return value;
	}

	//Use of Operator Overloading
	public static Variable operator +(Variable var1, Variable var2)
	{
		Variable newVar = new Variable();
		newVar.SetValue(var1.GetValue() + var2.GetValue());
		return newVar;
	}

	public static Variable operator -(Variable var1, Variable var2)
	{
		Variable newVar = new Variable();
		newVar.SetValue(var1.GetValue() - var2.GetValue());
		return newVar;
	}

	public static Variable operator /(Variable var1, Variable var2)
	{
		Variable newVar = new Variable();
		newVar.SetValue(var1.GetValue() / var2.GetValue());
		return newVar;
	}

	public static Variable operator *(Variable var1, Variable var2)
	{
		Variable newVar = new Variable();
		newVar.SetValue(var1.GetValue() * var2.GetValue());
		return newVar;
	}

}
