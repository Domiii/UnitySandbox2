using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour {
	public int resourceValue = 1;

	public void Consume() {
		ResourceManager.instance.resourceCount += resourceValue;

		// TODO: consumption animation?

		Destroy (gameObject);
	}
}
