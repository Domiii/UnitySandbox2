using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {
	public Vector3 axis = Vector3.right;
	public float angle = 90;

	void Reset() {
		Update ();
	}

	// Update is called once per frame
	void Update () {
		// fix rotation relative to world
		transform.rotation = Quaternion.AngleAxis(angle, axis);
	}
}
