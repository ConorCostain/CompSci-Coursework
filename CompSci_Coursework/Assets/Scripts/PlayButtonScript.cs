using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour {

	public static void OnClick()
	{
		GameObject canvas = GetCanvas();
		canvas.GetComponentsInChildren<StartBlock>().ForEach(b => b.StartInterpreter());
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
