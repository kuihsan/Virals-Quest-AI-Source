using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerarotate : MonoBehaviour {
	
	void Update () {
		
		//compensate character rotation
		if (Input.GetKey (KeyCode.A))
			transform.Rotate (new Vector3 (0f, 90f, 0f) * Time.deltaTime);
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (new Vector3 (0f, -90f, 0f) * Time.deltaTime);

		//ROTATE
		if (Input.GetAxis ("Mouse X")>0)
			transform.Rotate (new Vector3 (0f, 90f, 0f) * Time.deltaTime);
		if (Input.GetAxis ("Mouse X")<0)
			transform.Rotate (new Vector3 (0f, -90f, 0f) * Time.deltaTime);
	}
}
