using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Player_controller))]
public class Player : MonoBehaviour {

	private float moveSpeed = 0.3f;
	private float gravity = -2;
	private Vector3 velocity;


	//float jumpVelocity = 0.6f;
	Player_controller controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Player_controller> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")); 

		velocity.x = input.x * moveSpeed;

		velocity.y = velocity.y + (gravity * Time.deltaTime);
		controller.Move (velocity);
	}


}
