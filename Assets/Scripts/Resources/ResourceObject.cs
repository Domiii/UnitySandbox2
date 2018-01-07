using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ResourceObject : MonoBehaviour {
	public int resourceValue = 1;

	public float colorRatio = 0.85f;

	[HideInInspector]
	public Material originalMaterial;

	public bool isGrabbed = false;

	new Rigidbody body;
	Renderer renderer;

	void Start() {
		renderer = GetComponent<Renderer>();
		body = GetComponent<Rigidbody> ();
		originalMaterial = new Material(renderer.sharedMaterial);
		renderer.material = originalMaterial;
	}

	public void Consume() {
		ResourceManager.instance.resourceCount += resourceValue;

		// TODO: consumption animation?

		Destroy (gameObject);
	}

	public void SetGrabbed(bool grabbed, MultiObjectGravityGrabber grabber) {
		if (grabbed != this.isGrabbed) {
			if (grabbed) {
				body.useGravity = false;
				var otherRenderer = grabber.GetComponent<Renderer> ();
				renderer.material.Lerp (originalMaterial, otherRenderer.material, colorRatio);
			} else {
				body.useGravity = true;
				renderer.material = originalMaterial;
			}
			this.isGrabbed = grabbed;
		}
	}
}
