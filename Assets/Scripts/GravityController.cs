using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {
	/// <summary>
	/// The transform to pull the grabbed object toward.
	/// </summary>
	public Transform gravityPoint;

	/// <summary>
	/// The power of the pull on grabbed objects
	/// </summary>
	public float pullPower = 200;

	/// <summary>
	/// The extra amount of power added to grabbed objects through swinging mouse motion
	/// </summary>
	public float swingPower = 1;

	public int swingSmoothFactor = 2;

	Vector3 previousGravityPointPosition;

	/// <summary>
	/// The currently grabbed object.
	/// </summary>
	public Rigidbody grabbedObject;

	void FixedUpdate() {
		if (grabbedObject != null) {
			if (!Input.GetMouseButton (0)) {
				// drop object when mouse is released
				DropObject ();
			} else {
				PullObject (grabbedObject);
			}
		}
	}

	/// <summary>
	/// Exert a damped gravitational force to pull the object toward a location in front of the player.
	/// </summary>
	void PullObject(Rigidbody obj) {
		// compute force direction
		var dir = gravityPoint.transform.position - obj.transform.position;
		var dist = dir.magnitude;
		if (dir.magnitude < 0.1f) {
			// stop pulling when very close
			return;
		}

		dir.Normalize ();

		// the further, the stronger the pull!
		//var delta = dir * (pullPower * Time.fixedDeltaTime * (dist * dist));

		// pull strength is independent of distance
		var speed = pullPower * Time.fixedDeltaTime;
		var delta = dir * speed;

		if (dist > speed * 0.1f) {
			delta *= dist;
		} else {
			// if is very close, stop moving, prevent oscillation
			delta = Vector3.zero;
		}

		// add mouseMomentum
		delta += CalcSwingMomentum();

		obj.velocity = delta;
	}

	/// <summary>
	/// Calculate swing momentum: given by how far the gravityPoint moved in a frame.
	/// NOTE: The gravityPoint is locked with mouse/controller.
	/// </summary>
	Vector3 CalcSwingMomentum() {
		var p1 = previousGravityPointPosition;
		var p2 = gravityPoint.transform.position;

		var swingDelta = p2 - p1;

		var swingMomentum = swingDelta * swingPower;

		// update previous position (smoothed over time)
		previousGravityPointPosition = (previousGravityPointPosition * (swingSmoothFactor - 1) + p2) / swingSmoothFactor;

		return swingMomentum;
	}

	public void PickUp(Rigidbody obj) {
		grabbedObject = obj;
		grabbedObject.useGravity = false;

		previousGravityPointPosition = gravityPoint.transform.position;
	}

	public void DropObject() {
		if (grabbedObject != null) {
			grabbedObject.useGravity = true;
			grabbedObject = null;
		}
	}
}
