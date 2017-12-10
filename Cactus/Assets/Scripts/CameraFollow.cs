using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	public GameObject followTarget;
	public float moveSpeed;
	public Vector3 targetPos;
	public BoxCollider2D boundBox;
	private Vector3 minBounds;
	private Vector3 maxBounds;
	private Camera theCamera;
	private float halfHeight;
	private float halfWidth;

	// Use this for initialization
	void Start () 
	{
		minBounds = boundBox.bounds.min;
		maxBounds = boundBox.bounds.max;
		halfHeight = theCamera.orthographicSize;
		halfWidth = halfHeight * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () 
	{
		targetPos = new Vector3 (0f, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);

		float clampedX = Mathf.Clamp (transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
		float clampedY = Mathf.Clamp (transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
		transform.position = new Vector3 (clampedX, clampedY, transform.position.z);
	}
}