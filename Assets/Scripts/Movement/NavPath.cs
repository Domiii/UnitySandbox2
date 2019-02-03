using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NavPath : MonoBehaviour {
	public enum FollowDirection {
		Forward,
		Backward
	}

	public enum RepeatMode {
		/// <summary>
		/// Only follow the path once
		/// </summary>
		Once,

		/// <summary>
		/// When arriving at the end, start from the beginning
		/// </summary>
		Repeat,

		/// <summary>
		/// When arriving at the end, go reverse direction
		/// </summary>
		Mirror
	}

	public IEnumerator<Transform> GetPathEnumerator(FollowDirection direction) {
		return direction == FollowDirection.Forward ? GetPathEnumeratorForward () : GetPathEnumeratorBackward ();
	}
	
	/// <summary>
	/// Enumerates over all points
	/// </summary>
	public IEnumerator<Transform> GetPathEnumeratorForward() {
		for (var i = 0; i < transform.childCount; ++i) {
			yield return transform.GetChild(i);
		}
		yield return null;
	}
	
	/// <summary>
	/// Enumerates over all points
	/// </summary>
	public IEnumerator<Transform> GetPathEnumeratorBackward() {
		for (var i = transform.childCount-1; i >= 0; --i) {
			yield return transform.GetChild(i);
		}
		yield return null;
	}

	public Transform FirstPoint {
		get {
			if (transform.childCount == 0) {
				return null;
			}

			return transform.GetChild(0);
		}
	}

	public void OnDrawGizmos() {
		if (transform.childCount < 2) {
			return;
		}

		for (var i = 1; i < transform.childCount; ++i) {
			Gizmos.DrawLine (transform.GetChild(i-1).position, transform.GetChild(i).position);
		}
	}
}
