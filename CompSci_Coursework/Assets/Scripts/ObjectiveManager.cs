using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class ObjectiveManager : MonoBehaviour {

	//In the Unity editor the User can enter the desired Inputs and expected outputs and enter instructions for the user
	public List<int> inputList = new List<int>();
	public List<int> expectedOutputs = new List<int>();
	public int minBlocks;
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
	public GameObject winPopup;
	public TMP_Text scoreText;
	public TMP_Text highScoreText;
	public TMP_Text blocksUsedText;

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
				inputText.text = "Inputs:";
				AddToTMP(inputText, inputList);
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
				expOutputText.text = "Expected Outputs:";
				AddToTMP(expOutputText, expectedOutputs);
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
				outputText.text = "Outputs:";
			}
			//otherwise it will disable the UI element so it is out of the way
			else
				outputText.gameObject.SetActive(false);
		}



	}

	public void SetInstruction(string instruction = "")
	{
		if(instruction == "")
		{
			instruction = instructions;
		}

		if (instructionText != null)
		{
			instructionText.text = instruction; 
		}
	}

	public static void AddToTMP(TMP_Text text, List<int> list)
	{
		foreach(int element in list)
		{
			text.SetText(text.text + " " + element.ToString() + ",");
		}
	}

	public static void AddToTMP(TMP_Text text, int element)
	{
		text.SetText(text.text + " " + element.ToString() + ",");
	}

	public void WinFunc(int blocksUsed)
	{
		//Shows the Win Popup
		winPopup.SetActive(true);
		try
		{
			//ensures it is the last sibling so that it is on top
			//If already last sibling will throw an exception so this catches that
			winPopup.transform.SetAsLastSibling();
		}
		catch {}

		blocksUsedText.text = "Blocks Used : " + blocksUsed.ToString();
		//Cast into floats and back into integers as otherwise the decimal values when divded were set to 0 and lost
		int score = (int)(((float)minBlocks / (float)blocksUsed) * 100);
		scoreText.text = "Score :" + score.ToString();
		int highScore = GetHighScore(SceneManager.GetActiveScene().name, score);
		highScoreText.text = "High Score : " + highScore.ToString();
	}

	private int GetHighScore(string levelName, int score = 0)
	{
		string highScoreFile;
		int highScore = 0;
		//checks to prevent trying to read a file that does not exist
		if (System.IO.File.Exists(levelName + ".txt"))
		{
			//use of file reading
			using (StreamReader reader = new StreamReader(levelName + ".txt"))
			{
				highScoreFile = reader.ReadLine();
			}
			

			//Use of Try Catch in case the 
			try
			{
				highScore = int.Parse(highScoreFile);
			}
			catch { } 
		}

		if(score > highScore)
		{
			highScore = score;
			SetHighScore(highScore, levelName);
		}

		return highScore;
	}

	private void SetHighScore(int highScore, string levelName)
	{
		//use of writing to files
		using (StreamWriter writer = new StreamWriter(levelName + ".txt"))
		{
			writer.Write(highScore);
		}
	}
}

