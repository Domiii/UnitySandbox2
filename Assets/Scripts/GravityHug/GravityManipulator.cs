using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityManipulator : MonoBehaviour {
	public Vector3 g;
	public float gForce;
	public float interpolationRate = 60;

	public Rigidbody body;
	new public Collider collider;

	GravityHug hug;

	void Reset() {
		g = Physics.gravity;
		gForce = g.magnitude;
	}

	void Awake() {
		if (body == null) {
			body = GetComponent<Rigidbody> ();
		}
		if (collider == null) {
			collider = GetComponent<Collider> ();
		}
	}

	public void StartHugging(GravityHug hug) {
		this.hug = hug;
		body.useGravity = false;
	}

	public void StopHugging(GravityHug hug) {
		if (hug == this.hug) {
			//this.hug = null;
			//body.useGravity = true;
		}
	}

	void FixedUpdate () {
		if (hug) {
			// get new up vector
			var surfaceUp = hug.FindSurfaceNormal (collider);
			Debug.DrawRay(body.transform.position, surfaceUp * 100, Color.white);

			// change up vector
			Quaternion targetRotation = Quaternion.FromToRotation(body.transform.up, surfaceUp) * body.rotation;
			//body.rotation = Quaternion.Slerp(body.rotation, targetRotation, interpolationRate * Time.fixedDeltaTime);
			body.rotation = targetRotation;

			// change gravity
			g = -gForce * surfaceUp;
		}

		body.AddForce (g, ForceMode.Force);
	}
}
