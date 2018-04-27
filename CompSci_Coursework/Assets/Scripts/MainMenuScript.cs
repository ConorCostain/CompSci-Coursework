using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public Button playButton;
	public Button quitButton;

	public void Awake()
	{
		//Setups up referenced buttons on click methods
		if(playButton != null)
		{
			playButton.onClick.AddListener(() => PlaySessionManager.ins.LoadScene("SceneSelect") );
		}
		if (quitButton != null)
		{
			quitButton.onClick.AddListener(() => Application.Quit() );
		}

	}

	
}
