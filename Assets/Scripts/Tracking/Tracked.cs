using UnityEngine;
using System.Collections;

/// <summary>
/// Registers this object with the ObjectTracker and 
/// provides arrow image for tracking.
/// </summary>
public class Tracked : MonoBehaviour {

	public bool IsOnCamera {
		get;
		private set;
	}

	public UnityEngine.UI.Image ArrowImage {
		get;
		set;
	}

	public Vector3 ViewportPoint {
		get;
		private set;
	}

	void Start() {
		UpdateTrackStatus (true);
	}

	void Update() {
		UpdateTrackStatus ();
	}

	void UpdateTrackStatus(bool force = false) {
		ViewportPoint = Camera.main.WorldToViewportPoint (transform.position);
		var wasOncamera = IsOnCamera;
		IsOnCamera = Mathf.Clamp (ViewportPoint.x, 0, 1) == ViewportPoint.x && Mathf.Clamp (ViewportPoint.y, 0, 1) == ViewportPoint.y && ViewportPoint.z >= 0;
		if (force || IsOnCamera != wasOncamera) {
			if (ObjectTracker.Instance != null) {
				ObjectTracker.Instance.UpdateTrackStatus (this);
			}
		}
	}

	void OnDeath(DamageInfo damageInfo) {
		if (ObjectTracker.Instance != null) {
			ObjectTracker.Instance.StopTracking (this);
		}
	}

}
