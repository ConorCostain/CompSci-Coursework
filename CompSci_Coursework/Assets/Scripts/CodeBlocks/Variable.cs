﻿using System;
using UnityEngine;

public class Variable{

	protected int value;

	private string name;

	private int index = -1;

	public Variable(int value)
	{
		this.value = value;
	}

	//Use of Default Values
	public Variable(string name, int index, int value = 0)
	{
		this.value = value;
		this.name = name;
		if (index >= -1 && index < PlaySessionManager.ins.variableList.Count)
		{
			this.index = index;
		}
		else
		{
			Debug.Log("invalid constructor index value");
		}
	}

	public void SetValue(int value)
	{

		this.value = value;
		if(index != -1 )
		{
			if (index < PlaySessionManager.ins.variableList.Count)
			{
				PlaySessionManager.ins.variableList[index].SetValue(value);
			}
			else
			{
				Debug.Log("Index Value Out of Variable list range");
			}
		}
	}

	public int GetValue()
	{
		if(index != -1)
		{
			value = PlaySessionManager.ins.variableList[index].GetValue();
		}
		return value;
	}

	public void SetName(string name)
	{
		this.name = name;
	}

	public string GetName()
	{
		return name;
	}

	public void SetIndex(int index)
	{
		if (index >= -1 || index < PlaySessionManager.ins.variableList.Count)
		{
			this.index = index;
		}
		else
		{
			Debug.Log("Invalid set index value");
		}
	}

	//Use of Operator Overloading
	public static Variable operator +(Variable var1, Variable var2)
	{
		return new Variable(var1.GetValue() + var2.GetValue());
	}

	public static Variable operator -(Variable var1, Variable var2)
	{
		return new Variable(var1.GetValue() - var2.GetValue());
	}

	public static Variable operator /(Variable var1, Variable var2)
	{
		return new Variable(var1.GetValue() / var2.GetValue());
	}

	public static Variable operator *(Variable var1, Variable var2)
	{
		return new Variable(var1.GetValue() * var2.GetValue());
	}

}
