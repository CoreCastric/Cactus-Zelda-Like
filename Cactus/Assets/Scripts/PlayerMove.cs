using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private Rigidbody2D body2d;
    public float speed = 5f;

	
	void Start ()
    {
        body2d = GetComponent<Rigidbody2D>();
    
	}

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        body2d.velocity = new Vector2(inputX * speed, inputY * speed);

        

    }

    void Update () {
		
	}
}
