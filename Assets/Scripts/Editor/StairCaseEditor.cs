using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StairCase))]
public class StairCaseEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button ("Build Stairs")) {
			((StairCase)target).ClearStairs ();
			((StairCase)target).BuildStairs ();
		}

		if (GUILayout.Button ("Clear Stairs")) {
			((StairCase)target).ClearStairs ();
		}
	}
}
