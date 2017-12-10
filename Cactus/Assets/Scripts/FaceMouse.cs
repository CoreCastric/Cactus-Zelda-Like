using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour 
{

	private Vector2 mousePos;
	private float angleAnim;
	public Animator anim;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		faceMouse ();
		anim.SetFloat ("Rot_Z", angleAnim);
	}

	private void faceMouse ()
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		float rot_z = (Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg) - 90;
//		transform.rotation = Quaternion.Euler (0f, 0f, rot_z);
		angleAnim = rot_z +270;
		Debug.Log (rot_z+ 270);
	}


}
