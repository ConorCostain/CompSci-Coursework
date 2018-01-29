using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

	public void PlayButton()
	{
		Debug.Log("Play Button Pressed");
	}

	public void LevelsButton()
	{
		Debug.Log("Levels Button Pressed");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
