using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(HasSpeed))]
public class WASDPhysicalController : MonoBehaviour {
	public float rotationSpeed = 180;
	public float jumpStrength = 9;
	public CollisionCounter groundCollisionCounter;

	HasSpeed hasSpeed;
	Rigidbody body;

	float moveRotation, moveForward;
	bool jumping;

	void Start () {
		hasSpeed = GetComponent<HasSpeed> ();
		body = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (!PlayerInputManager.Instance.IsDefaultGameInputEnabled) {
			return;
		}
		// get input in Update()
		moveRotation = Input.GetAxisRaw ("Horizontal");
		moveForward = Input.GetAxis ("Vertical");

		if (CanJump) {
			jumping = jumping || Input.GetKey (KeyCode.Space);
		}
	}

	void FixedUpdate () {
		if (!PlayerInputManager.Instance.IsDefaultGameInputEnabled) {
			return;
		}
		// update physics in FixedUpdate()

		UpdateDirection ();
		UpdateVelocity ();
	}

	void UpdateDirection () {
		// rotate
		transform.Rotate (Vector3.up, rotationSpeed * moveRotation * Time.fixedDeltaTime, Space.World);
	}

	void UpdateVelocity () {
		// compute forward direction
		var forward = transform.forward;
		forward.y = 0;
		forward.Normalize ();

		// compute forward velocity
		var v = moveForward * hasSpeed.speed * forward;

		if (jumping) {
			// jump!
			v.y = jumpStrength;
			jumping = false;
		} else {
			// keep vertical speed
			v.y = body.velocity.y;
		}

		body.velocity = v;
		//transform.Translate(moveForward * Vector3.forward * hasSpeed.speed * Time.fixedDeltaTime);
	}

	public bool CanJump {
		get { return groundCollisionCounter == null || groundCollisionCounter.HasAnyCollisions || !body.useGravity; }
	}
}