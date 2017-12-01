using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private Rigidbody2D body2d;
    public float speed;

	
	void Start ()
    {
        body2d = GetComponent<Rigidbody2D>();
    
	}

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        body2d.velocity = new Vector2(horizontal * speed, vertical * speed);

    }

    void Update () {
		
	}
}
