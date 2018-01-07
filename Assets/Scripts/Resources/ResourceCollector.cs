using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		var resource = other.GetComponent<ResourceObject> ();
		if (resource) {
			resource.Consume ();
		}
	}
}
