using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3D : MonoBehaviour {
	Plane targetPlane;

	void FixedUpdate () {
		var cam = Camera.main;

		// have a plane on the height of the 3DCursor object
		targetPlane.SetNormalAndPosition (Vector3.up, transform.position);

		// find ray intersection shooting from mouse to plane
		var mousePos = Input.mousePosition;
		var ray = cam.ScreenPointToRay (new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
		float dist;
		if (targetPlane.Raycast (ray, out dist)) {
			// 
			transform.position = ray.GetPoint(dist);
		}
	}
}
