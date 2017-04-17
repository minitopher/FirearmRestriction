using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {


	public GameObject projectile;

	private static float chargeShot = 0.0f;



	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			chargeShot += 0.1f;
			chargeShot = Mathf.Clamp (chargeShot, 0.0f, 10.0f);
			Debug.Log (chargeShot);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {

			GameObject shot = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
			Shoot_ball (ref rb);
			chargeShot = 0;
		}

	}

	public static void Shoot_ball(ref Rigidbody2D rb){
		rb.AddForce (new Vector2(-1,1) * chargeShot * 50);
		//rb.AddForce (Transform.up * chargeShot);
	}
}
