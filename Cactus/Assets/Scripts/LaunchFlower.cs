using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFlower : MonoBehaviour {

	public GameObject player;
	public GameObject rangeFleur;
	public Transform emplacement;
	public Rigidbody2D bodyFleur;
	public LineRenderer lianeRend;
	public Vector2 lastMove;
	public float fleurSpeed = 100f;
	public float fleurSpeedBack = 600f;
	public float maxDistanceToFleur = 8f;
	public float hookTime = 800f;

	private GameObject hookedEnemy;
	private bool isLaunched = false;
	private bool isBacking = false;
	private bool isHooked = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		Vector2 orientStick = new Vector2 (horizontal, vertical);

		if(orientStick.magnitude > 0.5f)
		{
			lastMove = orientStick;
		}

		if(!isLaunched && !isBacking && !isHooked)
		{
			transform.position = emplacement.position;
		}
		if(isBacking)
		{
			Back ();
		}
		if(hookedEnemy != null && isHooked)
		{
			HookEnemy (hookedEnemy);
		}
		if (maxDistanceToFleur < Vector2.Distance (player.transform.position, transform.position) && isHooked) {
			isLaunched = false;
			isBacking = true;
		}

		Launch ();

		if(Input.GetAxisRaw("RightTrigger") != 0 && isHooked)
		{
			isBacking = true;
			isHooked = false;
		}
	}
		
	public void Launch()
	{
		lianeRend.SetPosition (0, transform.position);
		lianeRend.SetPosition (1, player.transform.position);
		if (Input.GetAxisRaw("RightTrigger") != 0 && !isLaunched && !isBacking && !isHooked) 
		{
			bodyFleur.velocity = lastMove.normalized * fleurSpeed * Time.fixedDeltaTime;
			isLaunched = true;
		}
		if (maxDistanceToFleur < Vector2.Distance (player.transform.position, transform.position)) {
			isLaunched = false;
			isBacking = true;
		}
	}
		
	public void Back()
	{
		Vector2 dirToPlayer = player.transform.position - transform.position;
		bodyFleur.velocity = dirToPlayer.normalized * fleurSpeedBack * Time.fixedDeltaTime; // On garde juste la direction du vecteur (grâce à Normalized), on multiplie la direction par la vitesse (fleurSpeedBack)
		if(Vector2.Distance(player.transform.position, transform.position) < 1f)
		{
			isLaunched = false;
			isBacking = false;
			isHooked = false;
		}
	}

	public void HookEnemy(GameObject hookEne)
	{
		transform.position = hookEne.transform.position;
	}

	IEnumerator hookDelay()
	{
		yield return new WaitForSeconds (hookTime);
		isHooked = false;
		isBacking = true;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Enemy" && isLaunched)
		{
			isHooked = true;
			hookedEnemy = coll.gameObject;
			StartCoroutine (hookDelay ());
		}
	}
		
}
