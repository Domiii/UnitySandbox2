using UnityEngine;
using System.Collections;

public class MoveWithTarget : MonoBehaviour {
	public Transform target;
	Vector3 offset;

	void Start() {
		if (target == null)
			return;

		offset = transform.position - target.position;
	}


	void Update () {
		if (target == null)
			return;
		
		transform.position = target.position + offset;
	}
}
