using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {

	public Button mmButton;
	// Use this for initialization
	void Start () {
		if(mmButton != null)
		{
			mmButton.onClick.AddListener(() => PlaySessionManager.ins.LoadScene("MainMenu"));
		}
	}
	
	
}
