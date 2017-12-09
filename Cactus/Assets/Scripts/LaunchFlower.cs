using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFlower : MonoBehaviour {

	public GameObject player;
	public Transform emplacement;
	public Rigidbody2D bodyFleur;
	public LineRenderer lianeRend;
	public float fleurSpeed = 100f;
	public float fleurSpeedBack = 600f;
	public float maxDistanceToFleur = 8f;
	public float hookTime = 800f;
	private GameObject hookedEnemy;
	private Vector2 dirToMouse;
	private bool isLaunched = false;
	private bool isBacking = false;
	private bool isHooked = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
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
		Launch ();
		if(Input.GetMouseButton(1) && isHooked)
		{
			DetachFlower();
		}
	}

	public void DetachFlower(){
		isBacking = true;
		isHooked = false;
	}

	public void Launch()
	{
		float fDistance = Vector2.Distance (player.transform.position, transform.position);
		lianeRend.SetPosition (0, transform.position);
		lianeRend.SetPosition (1, player.transform.position);
		if (Input.GetMouseButtonDown (0) && !isLaunched && !isBacking && !isHooked) {
			dirToMouse = Camera.main.ScreenToWorldPoint (Input.mousePosition) - player.transform.position;
			bodyFleur.velocity = dirToMouse.normalized * fleurSpeed * Time.fixedDeltaTime;
			isLaunched = true;
		}
		if (maxDistanceToFleur < fDistance) {
			isLaunched = false;
			isBacking = true;
		}
		if (isHooked && fDistance > maxDistanceToFleur) {
			DetachFlower();
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
		

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Enemy" && isLaunched)
		{
			isHooked = true;
			hookedEnemy = coll.gameObject;
//			StartCoroutine (hookDelay ());
		}
	}
		
}
