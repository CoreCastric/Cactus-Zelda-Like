using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacements : MonoBehaviour 
{

	public float speed = 5f;
	public float sprintSpeed = 10f;
	public float jumpSpeed = 10f;
	public float jumpTime = .5f;
	public float jumpPreparation = 0.5f;
	public float jumpCoolDown = 1f;

	private float actualSpeed;
	private float distArrowAim;
	private bool canJump= true;
	private bool canMove = true;
	private bool canSprint = true;
	private bool onFight = false;
	private bool isTargetted = false;

	public Transform player;
	public GameObject rangeFleur;
	public GameObject aimFleur;
	public LaunchFlower Fleur;
	public Rigidbody2D body;
	private GameObject targetEnemy;
	private Animator anim;
	private Vector2 lastMove;
	private Vector2 currentMove;
	private Vector2 dirToEnemy;



	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		Debug.Log (" CanJump" + canJump);

		float vertical = Input.GetAxisRaw ("Vertical");
		float horizontal = Input.GetAxisRaw("Horizontal");


		Vector2 orientStick = new Vector2 (horizontal, vertical);



		if(orientStick.magnitude > 0.5f)
		{
			lastMove = orientStick;
		}
		if(canMove)
		{
			currentMove = lastMove;

			if(horizontal > 0.5f || horizontal < -0.5f)
			{
				body.velocity = orientStick.normalized * actualSpeed;
			}
			if(vertical > 0.5f || vertical < -0.5f)
			{
				body.velocity = orientStick.normalized * actualSpeed;
			}
				

			if(horizontal < 0.5f && horizontal > -0.5f)
			{
				body.velocity = new Vector2(0f, body.velocity.y);
			}		
			if(vertical < 0.5f && vertical > -0.5f)
			{
				body.velocity = new Vector2(body.velocity.x, 0f);
			}
		}



		if (Input.GetButton ("XButton") && canSprint == true) 
		{
			actualSpeed = sprintSpeed;
		} 
		else 
		{
			actualSpeed = speed;
		}



		if (Input.GetButtonDown ("BButton") && canJump == true)
		{
			currentMove = orientStick;
			StartCoroutine (dashingDelay ());
		}



		if(onFight && Input.GetAxisRaw("LeftTrigger") != 0)
		{
			Fleur.lastMove = dirToEnemy;
			canJump = true;
			LockEnemy (targetEnemy);
		}

		if(Input.GetAxisRaw("LeftTrigger") != 0 && onFight == false)
		{
			canJump = false;
			player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			rangeFleur.transform.localScale = new Vector2 (Fleur.maxDistanceToFleur * 2, Fleur.maxDistanceToFleur * 2);
			aimFleur.SetActive (true);
			aimFleur.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (lastMove.y, lastMove.x) * Mathf.Rad2Deg);
			rangeFleur.SetActive (true);
		}
		else
		{
			rangeFleur.SetActive (false);
			aimFleur.SetActive (false);
			canJump = true;
		}


		anim.SetFloat ("Rot_Z", (Mathf.Atan2(lastMove.y, lastMove.x) * Mathf.Rad2Deg) + 180);

	}




	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "EnemyDetectionZone")
		{
			onFight = true;
			isTargetted = true;
			targetEnemy = coll.gameObject;
		}
		else
		{
			onFight = false;
			isTargetted = false;
		}
	}

	public void LockEnemy(GameObject targetEnemy)
	{
		dirToEnemy = targetEnemy.transform.position - transform.position;
		float rot_z = Mathf.Atan2(dirToEnemy.y, dirToEnemy.x) * Mathf.Rad2Deg;
		lastMove.x = dirToEnemy.x;
		lastMove.y = dirToEnemy.y;
	}
		
	public void LockPlayer()
	{
		
		canJump = false;
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		rangeFleur.transform.localScale = new Vector2 (Fleur.maxDistanceToFleur * 2, Fleur.maxDistanceToFleur * 2);
		aimFleur.SetActive (true);
		aimFleur.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (lastMove.y, lastMove.x) * Mathf.Rad2Deg);
		rangeFleur.SetActive (true);
	}

	IEnumerator dashingDelay()
	{
		canSprint = false;
		canJump = false;
		canMove = false;
		body.velocity = Vector2.zero;
		yield return new WaitForSeconds (jumpPreparation);
		body.AddForce (currentMove.normalized * jumpSpeed, ForceMode2D.Impulse);
		yield return new WaitForSeconds (jumpTime);
		canMove = true;
		canSprint = true;
		yield return new WaitForSeconds (jumpCoolDown);
		canJump = true;
	}

	
}