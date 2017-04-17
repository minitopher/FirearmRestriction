using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Player_controller))]
public class Player : MonoBehaviour {

	private float moveSpeed = 0.3f;
    private float setMoveSpeed;
	private float gravity = -2;
	private Vector3 velocity;
    public bool facingRight = false;


	//float jumpVelocity = 0.6f;
	Player_controller controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Player_controller> ();
        setMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));


        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            moveSpeed = setMoveSpeed;
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            if (facingRight == false) {
                moveSpeed = moveSpeed * -1;
                facingRight = true;
            }
        }
        velocity.x = input.x * moveSpeed;

		velocity.y = velocity.y + (gravity * Time.deltaTime);
		controller.Move (velocity);
	}


}
