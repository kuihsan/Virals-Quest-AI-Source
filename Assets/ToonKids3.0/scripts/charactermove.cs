using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactermove : MonoBehaviour {

	Animator anim;
	Rigidbody rigid;
	Transform trans;
	float jumpforce;
	bool gr;

	void Start () 
	{
		
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
		trans = GetComponent<Transform>();
		jumpforce = 100f;


	}
	
	void Update () 
	{	
		//IDLES
		anim.SetInteger ("idle", Random.Range(0,1200));

		//CHECK GROUNDED
		if   (Physics.Raycast(trans.position+new Vector3(0.1f,0.05f,0.1f    ), Vector3.down, 0.1f)
			||Physics.Raycast(trans.position+new Vector3(0.075f,0.05f,-0.02f), Vector3.down, 0.1f)
			||Physics.Raycast(trans.position+new Vector3(-0.1f,0.05f,0.08f  ), Vector3.down, 0.1f)
			||Physics.Raycast(trans.position+new Vector3(-0.1f,0.05f,-0.05f ), Vector3.down, 0.1f))
		{
				anim.SetBool("grounded", true);
				gr = true;

		}
		else
		{
			anim.SetBool("grounded", false);
			gr = false;
		}

		//TRANSLATE
			anim.SetFloat ("walk", Input.GetAxisRaw ("Vertical"));
		if (Input.GetKey (KeyCode.W) && gr == true) {
			rigid.velocity = trans.forward * 0.625f;
				if (anim.GetBool ("run") == true)
				rigid.velocity = trans.forward * 2f;
			}
		if (Input.GetKey (KeyCode.S)&& gr == true) {
			rigid.velocity = trans.forward * -0.625f;
				if (anim.GetBool ("run") == true)
				rigid.velocity = trans.forward * -1.5f;
			}

		//ROTATE
			anim.SetFloat ("turn", Input.GetAxisRaw ("Horizontal"));
		if (Input.GetKey (KeyCode.A))
				transform.Rotate (new Vector3 (0f, -90f, 0f) * Time.deltaTime);
		if (Input.GetKey (KeyCode.D))
				transform.Rotate (new Vector3 (0f, 90f, 0f) * Time.deltaTime);

		//RUN
			if (Input.GetKey (KeyCode.LeftShift))
				anim.SetBool ("run", true);
			else
				anim.SetBool ("run", false);

		//STRAFE
		if (Input.GetKey (KeyCode.E) || Input.GetKey (KeyCode.Q)) {
			if (Input.GetKey (KeyCode.E) && gr == true && anim.GetFloat ("walk") == 0f) {
				anim.SetFloat ("strafe", 1f);
				rigid.velocity = trans.right  * 0.625f;
				if (anim.GetBool ("run") == true) {
					anim.SetFloat ("strafe", 2f);
					rigid.velocity = trans.right  * 1.2f;
				}
			} 
			if (Input.GetKey (KeyCode.Q) && gr == true && anim.GetFloat ("walk") == 0f) {
				anim.SetFloat ("strafe", -1f);
				rigid.velocity = trans.right  * -0.625f;
				if (anim.GetBool ("run") == true) {
					anim.SetFloat ("strafe", -2f);
					rigid.velocity = trans.right  * -1.2f;
				}
			}
		}
		else anim.SetFloat ("strafe", 0f);
		
		//JUMP
		if (Input.GetKeyDown (KeyCode.Space) && gr == true) {
				if (anim.GetFloat ("walk") <= 0f) {
					anim.Play ("TK_IP_jump");
				rigid.AddForce ((Vector3.up) * jumpforce*1.1f , ForceMode.Acceleration);
				}
				if (anim.GetFloat ("walk") > 0f) {
					anim.Play ("TK_IP_runjump");
					if (anim.GetBool ("run") == true)
						rigid.AddForce ((trans.forward + new Vector3 (0f, 2f, 0f)) * jumpforce, ForceMode.Acceleration);
					else
					rigid.AddForce ((trans.forward + new Vector3 (0f, 3f, 0f)) * jumpforce*0.45f, ForceMode.Acceleration);
				}
			}				
	}
}
