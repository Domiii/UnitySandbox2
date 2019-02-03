using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Camera rig is controlled with basic navigation keys
/// </summary>
public class BirdEyeCameraRig : MonoBehaviour {
	public KeyCode[] zoomKeys = new KeyCode[] {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3
	};

	float zoomStep;

	public float sensitivity = 6;

	void Start() {
		zoomStep = transform.position.y;
		SetZoomLevel (1);
	}

	void Update() {
		if (Input.anyKeyDown) {
			for (var i = 0; i < zoomKeys.Length; ++i) {
				var key = zoomKeys [i];
				if (Input.GetKeyDown (key)) {
					SetZoomLevel (i+1);
				}
			}
		}
	}

	void SetZoomLevel(int level) {
		var pos = transform.position;
		transform.position = new Vector3 (pos.x, level * zoomStep, pos.z);
	}

	void FixedUpdate() {
		var moveX = Input.GetAxisRaw("Horizontal");
		var moveZ = Input.GetAxisRaw("Vertical");
		var move = new Vector3 (moveX, 0, moveZ) *  (Time.fixedDeltaTime * sensitivity);
		transform.Translate (move);
	}
}
