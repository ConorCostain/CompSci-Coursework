using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {

	public Button mmButton;
	// Use this for initialization
	void Start () {
		if(mmButton != null)
		{
			//Adds an event listener
			mmButton.onClick.AddListener(() => PlaySessionManager.ins.LoadScene("MainMenu"));
		}
	}
	
	
}
