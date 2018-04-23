﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour {

	//In the Unity editor the User can enter the desired Inputs and expected outputs and enter instructions for the user
	public List<int> inputList = new List<int>();
	public List<int> expectedOutputs = new List<int>();
	public string instructions;

	//These Booleans decide whether to display these lists on the screen for the user
	public bool showInputs = true;
	public bool showExpectedOutput = false;
	public bool showOutputs = true;

	//References to UI elements
	public TMP_Text instructionText;
	public TMP_Text inputText;
	public TMP_Text expOutputText;
	public TMP_Text outputText;

	//Runs when level opens
	public void Start()
	{
		//Set up Lists, and access to the outputText in the PlaySessionManager
		PlaySessionManager.ins.ObjectivesSetup(inputList, expectedOutputs, this, outputText);

		//Setting Up the UI
		if (instructionText != null)
		{
			instructionText.SetText(instructions); 
		}

		if (inputText != null)
		{
			//if show Inputs is on it will set it as active then set up the text
			if (showInputs)
			{
				inputText.gameObject.SetActive(true);
				inputText.SetText("Inputs:");
				AddToTMP<int>(inputText, inputList);
			}
			//otherwise it will disable the UI element so it is out of the way
			else
				inputText.gameObject.SetActive(false);
		}

		if (expOutputText != null)
		{
			//if show Inputs is on it will set it as active then set up the text
			if (showExpectedOutput)
			{
				expOutputText.gameObject.SetActive(true);
				expOutputText.SetText("Expected Outputs:");
				AddToTMP<int>(expOutputText, expectedOutputs);
			}
			//otherwise it will disable the UI element so it is out of the way
			else
				expOutputText.gameObject.SetActive(false);
		}

		if (outputText != null)
		{
			//if show Inputs is on it will set it as active then set up the text
			if (showOutputs)
			{
				outputText.gameObject.SetActive(true);
				outputText.SetText("Outputs:");
			}
			//otherwise it will disable the UI element so it is out of the way
			else
				outputText.gameObject.SetActive(false);
		}



	}

	public void AddToTMP<T>(TMP_Text text, List<T> list)
	{
		foreach(T element in list)
		{
			AddToTMP<T>(text, element);
		}
	}

	public void AddToTMP<T>(TMP_Text text, T element)
	{
		text.SetText(text.text + " " + element.ToString() + ",");
	}

}