using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Collider))]
public class GravityHug : MonoBehaviour {
	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	new Collider collider;
	int layerMask;

	void Awake() {
		meshRenderer = GetComponent<MeshRenderer> ();
		meshFilter = GetComponent<MeshFilter> ();
		collider = GetComponent<Collider> ();
		layerMask = 1 << LayerMask.NameToLayer ("GravityHug");
	}

	Vector3 AverageNormal(Collision coll) {
		var normal = Vector3.zero;
		for (var i = 0; i < coll.contacts.Length; ++i) {
			var contact = coll.contacts [i];
			normal += contact.normal;
		}
		normal /= coll.contacts.Length;
		return normal;
	}

	public Vector3 FindSurfaceNormal(Collider other) {
		var otherPos = other.transform.position;
		RaycastHit hit;
		if (Physics.Raycast (otherPos, transform.position - otherPos, out hit, 1000, layerMask)) {
			//print (hit.normal + ", " + hit.distance + ", " + hit.point + ", " + hit.collider.name);
			return hit.normal;
		}

		// problem!
		print("Could not find surface normal from object to " + other.name);
		return Vector3.up;
	}

	void OnCollisionEnter(Collision other) {
		var gravityManipulator = other.collider.gameObject.GetComponentInHierarchy <GravityManipulator>();
		if (!gravityManipulator) {
			return;
		}
		gravityManipulator.StartHugging (this);
	}

	void OnCollisionExit(Collision other) {
		var gravityManipulator = other.collider.gameObject.GetComponentInHierarchy <GravityManipulator>();
		if (!gravityManipulator) {
			return;
		}

		gravityManipulator.StopHugging (this);
	}

//	void OnCollisionStay(Collision other) {
//		//print(other.collider);
//
//		var gravityManipulator = other.collider.gameObject.GetComponentInHierarchy <GravityManipulator>();
//		if (!gravityManipulator) {
//			return;
//		}
//
//		foreach (ContactPoint contact in other.contacts) {
//			//print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
//			Debug.DrawRay(contact.point, -contact.normal * 10, Color.white);
//		}
//
//		// get average normal of collision surface
//		var surfaceNormal = AverageNormal(other);
//
//		// change up direction of object
//		//var newUp = -Vector3.Lerp (gravityManipulator.transform.up, surfaceNormal, interpolationRate);
//		var newUp = -surfaceNormal;
//		gravityManipulator.transform.up = newUp;
//		gravityManipulator.g = surfaceNormal * gravityManipulator.gForce;
//	}
}
