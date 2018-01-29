using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySessionManager : MonoBehaviour {

	public static PlaySessionManager ins;

	void Awake()	// Runs when script is loaded, before game runs
	{
		// Singleton Pattern to have a single instance of the PlaySession manager
		if(ins == null)	//Checks whether there is already an Instance,
		{
			ins = this;	//If there is no instance sets itself to be the instance
			DontDestroyOnLoad(gameObject);	//Makes it so the object will persist through scenes
		}
		else if(ins != this)	//if the instance is set to something other than this object
		{
			Destroy(gameObject);	//destroy this object as there is already a different instance
			return;					// returns to prevent running any more code in this method
									// as it can take a short time for the object to be destroyed
		}
	}


}
