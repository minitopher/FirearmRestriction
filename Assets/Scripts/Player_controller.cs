using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Player_controller : MonoBehaviour {

	public LayerMask collisionMask;

	const float skinWidth = 0.15f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	float HRS;
	float VRS;

	BoxCollider2D collider;
	RaycastOrigins raycastorigins;
	public CollisionInfo collisions;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider2D> ();
		calculateRaySpacing ();
	}

	void HorizontalCollisions(ref Vector3 velocity){
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		for(int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastorigins.bottomleft : raycastorigins.bottomright;
			rayOrigin += Vector2.up * (HRS * i);

			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}

	}



	void VerticalCollisions(ref Vector3 velocity){
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;
		for(int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastorigins.bottomleft : raycastorigins.topleft;
			rayOrigin += Vector2.right * (VRS * i + velocity.x);

			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}

	}




	public void Move (Vector3 velocity){
		UpdateRaycastOrigins ();

		collisions.Reset ();

		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}
		transform.Translate (velocity);
	} 

	void UpdateRaycastOrigins(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastorigins.bottomleft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastorigins.bottomright = new Vector2 (bounds.max.x, bounds.min.y);
		raycastorigins.topleft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastorigins.topright = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void calculateRaySpacing(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		HRS = Mathf.Clamp (HRS, 2, int.MaxValue);
		VRS = Mathf.Clamp (VRS, 2, int.MaxValue);

		HRS = bounds.size.y / (horizontalRayCount - 1);
		VRS = bounds.size.x / (verticalRayCount - 1);
	}
	struct RaycastOrigins {
		public Vector2 topleft,topright;
		public Vector2 bottomleft,bottomright;

	}

	public struct CollisionInfo{
		public bool above, below;
		public bool left, right;

		public void Reset(){
			above = below = false;
			left = right = false;
		}
	}
}
