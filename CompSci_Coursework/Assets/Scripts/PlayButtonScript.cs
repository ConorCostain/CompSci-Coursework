using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour {

	public static void OnClick()
	{
		GameObject canvas = GetCanvas();

	}

	public GameObject GetCanvas(Transform trans = transform)
	{
		if(trans.gameObject.tag == "Canvas")
		{
			return trans;
		}
		else
		{
			return GetCanvas(trans.parent);
		}
	}

}
