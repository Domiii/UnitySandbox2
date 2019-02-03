using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public static class RotationUtil {
	public static Quaternion GetRotationToward(this Transform transform, Vector3 target) {
		Vector3 dir = target - transform.position;
		return transform.GetRotationFromDirection(dir);
	}

	public static Quaternion GetRotationFromDirection(this Transform transform, Vector3 dir) {
		var angle = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, transform.up);
	}

	public static void RotateTowardTarget(this Transform transform, Vector3 target, float turnSpeed = 180) {
		var rigidbody = transform.GetComponent<Rigidbody> ();
		if (rigidbody != null && (rigidbody.constraints & RigidbodyConstraints.FreezePositionY) != 0) {
			// don't rotate if rotation has been constrained
			return;
		}

		//transform.LookAt ();
		var agent = transform.GetComponent<NavMeshAgent> ();
		if (agent != null) {
			turnSpeed = agent.angularSpeed;
		}
		var targetRotation = transform.GetRotationToward(target);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
	}
}
