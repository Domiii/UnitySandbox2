using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndFollow : MonoBehaviour {
	public Transform target;

	//Vector3 offset;
	Quaternion localRot;
	
	void Start () {
		//offset = target.position - transform.position;
		localRot = transform.rotation;
	}

	void Update () {
		transform.position = target.position;
		transform.rotation = localRot * target.rotation;

		//var angles = target.rotation.eulerAngles;
		//transform.rotation = Quaternion.Euler(angles.x, 0, angles.z);
	}
}
