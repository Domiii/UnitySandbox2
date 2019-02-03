using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnitAttackerGizmoDrawer {
	[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
	static void DrawGizmoForMyScript(UnitAttacker t, GizmoType gizmoType)
	{
//		if (Vector3.Distance(position, Camera.current.transform.position) > 10f)
//			Gizmos.DrawIcon(position, "MyScript Gizmo.tiff");

		var mesh = t.GetComponent<MeshRenderer>();

		var pos = t.transform.position;

		if (mesh != null) {
			pos.y = mesh.bounds.min.y;
		} else {
			pos.y = t.transform.position.y;
		}
		pos.y += 0.01f; 	// add a small epsilon to reduce chances of interferance between surfaces


		Handles.color = Color.red;
		Handles.DrawWireDisc(pos, Vector3.up, t.attackRadius);
	}
}
