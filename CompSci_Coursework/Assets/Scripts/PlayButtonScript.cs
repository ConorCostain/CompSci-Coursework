using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour {

	public static void OnClick()
	{
		Debug.Log("OnClick called");
		GameObject.FindGameObjectsWithTag("CodeBlock").Where(b => b.GetComponent<StartBlock>() != null)
			.ToList().ForEach(b => b.GetComponent<StartBlock>().StartInterpreter());
	}

	

}
