using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCase : MonoBehaviour {
	public StairStep stepPrefab;
	public int nSteps = 10;
	public float depth = 1;
	public float height = 1;

	public float narrowWidth = 1;
	public float wideWidth = 2;
	public float animDelay = 1;

	void Start () {
		
	}

	public void ClearStairs() {
		for (var i = transform.childCount-1; i >= 0; --i) {
			var child = transform.GetChild (i);
			DestroyImmediate(child.gameObject);
		}
	}

	public void BuildStairs() {
		var pos = transform.position;
		for (var i = 0; i < nSteps; ++i) {
			var go = Instantiate (stepPrefab, pos, Quaternion.identity, transform);
			pos.y += height;
			pos.z += depth;
		}
	}
}
