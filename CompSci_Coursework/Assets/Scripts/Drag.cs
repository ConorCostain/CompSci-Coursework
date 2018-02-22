using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drag : MonoBehaviour {

	private void OnMouseDrag()
	{
		transform.position = Vector3.right * Input.mousePosition.x + Vector3.up * Input.mousePosition.y + Vector3.back;
	}
}
