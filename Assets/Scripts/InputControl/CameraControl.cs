using UnityEngine;
using System.Collections;


/// <summary>
/// 用 Input 來控制 Camera
/// </summary>
public class CameraControl : MonoBehaviour {
	public float zoomSpeed = 30;		// meters/second
	public float tiltSpeed = 30;		// degrees/second
	public float maxZ = -10;
	public float minZ = -50;
	public float maxTilt = 60;
	public float minTilt = 10;

	Vector3 defaultCameraPos;
	Quaternion defaultCameraRotation;

	Transform CameraMount {
		get {
			Transform trans;
			if (Camera.main.transform.parent != null) {
				trans = Camera.main.transform.parent;
			} else {
				trans = Camera.main.transform;
			}
			return trans;
		}
	}

	Quaternion CameraRotation {
		get {
			return CameraMount.localRotation;
		}
		set {
			CameraMount.localRotation = value;
		}
	}

	void Start() {
		defaultCameraPos = Camera.main.transform.localPosition;
		defaultCameraRotation = CameraRotation;
	}

	void Update() {
		UpdateCameraZoom ();
		UpdateCameraTilt ();
		ResetCameraDefaults ();
	}

	void UpdateCameraZoom() {
		var dPos = Input.GetAxis ("Mouse ScrollWheel");
		if (dPos != 0) {
			var zoomDirection = dPos/Mathf.Abs(dPos);		// normalize

			dPos = zoomDirection * zoomSpeed * Time.deltaTime;
			var newPos = Camera.main.transform.localPosition;
			newPos.z = Mathf.Clamp (newPos.z + dPos, minZ, maxZ);
			Camera.main.transform.localPosition = newPos;
		}
	}

	void UpdateCameraTilt() {
		var dTilt = Input.GetAxis ("Vertical");
		if (dTilt != 0) {
			dTilt = dTilt * tiltSpeed * Time.deltaTime;

			var localRotation = CameraRotation;
			var newAngles = localRotation.eulerAngles;
			newAngles.x = Mathf.Clamp (newAngles.x + dTilt, minTilt, maxTilt);
			localRotation.eulerAngles = newAngles;
			CameraRotation = localRotation;
		}
	}

	void ResetCameraDefaults() {
		if (Input.GetKeyDown(KeyCode.V)) {
			Camera.main.transform.localPosition = defaultCameraPos;
			CameraRotation = defaultCameraRotation;
		}
	}
}
