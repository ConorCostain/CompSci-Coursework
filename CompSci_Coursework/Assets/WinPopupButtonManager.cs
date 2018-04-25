using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPopupButtonManager : MonoBehaviour {

	public Button tryAgainButton;
	public Button mainMenuButton;

	public void Start()
	{
		
		if (tryAgainButton != null)
		{
			//Adding an event listener using an Anymous method to create the method inline
			tryAgainButton.onClick.AddListener( () =>
				PlaySessionManager.ins.LoadScene(SceneManager.GetActiveScene().name));
		}
		if (mainMenuButton != null)
		{
			//Adding an event listener using an Anymous method to create the method inline
			tryAgainButton.onClick.AddListener(() =>
			   PlaySessionManager.ins.LoadScene("MainMenu"));
		}
	}
}
