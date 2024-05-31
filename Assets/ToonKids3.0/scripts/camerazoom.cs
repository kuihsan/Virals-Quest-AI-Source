using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerazoom : MonoBehaviour {
	
	void Update () {

		//ZOOM
		if (Input.GetAxis ("Mouse ScrollWheel")>0)
			transform.Translate (new Vector3 (0f, 0f, 0.25f));
		if (Input.GetAxis ("Mouse ScrollWheel")<0)
			transform.Translate (new Vector3 (0f, 0f, -0.25f));
		
	}
}
