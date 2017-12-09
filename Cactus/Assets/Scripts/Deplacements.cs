using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacements : MonoBehaviour {

	public float speed = 5f;
	public float sprintSpeed = 10f;
	private float actualSpeed;
	public float jumpSpeed = 10f;
	public float jumpTime = .5f;
	public float jumpPreparation = 0.5f;
	private bool canJump= true;
	private bool canMove = true;
	private bool canSprint = true;
	private Vector2 dirToMouse;
	public Transform player;
	public Rigidbody2D body;




	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		float vertical = Input.GetAxisRaw ("Vertical");
		float horizontal = Input.GetAxisRaw("Horizontal");




		if(canMove)
		{
			if(horizontal > 0.5f || horizontal < -0.5f)
			{
				//			transform.Translate (new Vector3 ((horizontal * speed * Time.deltaTime), 0f, 0f));
				body.velocity = new Vector2(horizontal * actualSpeed, body.velocity.y);

			}
			if(vertical > 0.5f || vertical < -0.5f)
			{
				//			transform.Translate (new Vector3 (0f, vertical * speed * Time.deltaTime, 0f));
				body.velocity = new Vector2(body.velocity.x, vertical * actualSpeed);

			}
			if(horizontal < 0.5f && horizontal > -0.5f)
			{
				body.velocity = new Vector2(0f, body.velocity.y);
			}		
			if(vertical < 0.5f && vertical > -0.5f)
			{
				body.velocity = new Vector2(body.velocity.x, 0f);
			}

			if(Input.GetKey(KeyCode.LeftShift) && canSprint == true)
			{
				actualSpeed = sprintSpeed;
			}else{
				actualSpeed = speed;
			}
		}
		if (Input.GetButtonDown ("Jump") && canJump == true) 
		{
			dirToMouse = Camera.main.ScreenToWorldPoint (Input.mousePosition) - player.transform.position;
			StartCoroutine (dashingDelay ());
		}
	}

	void FixedUpdate()
	{
		
	}

	IEnumerator dashingDelay()
	{
		canSprint = false;
		canJump = false;
		canMove = false;
		body.velocity = Vector2.zero;
		yield return new WaitForSeconds (jumpPreparation);
		body.AddForce (dirToMouse.normalized * jumpSpeed, ForceMode2D.Impulse);
		yield return new WaitForSeconds (jumpTime);
		canMove = true;
		canJump = true;
		canSprint = true;
	}

}
