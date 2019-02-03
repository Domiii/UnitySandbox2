using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MainMenu))]
public class MainMenuEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		var menu = (MainMenu)target;

		if (GUILayout.Button ("Clear Menu")) {
			menu.ClearMenu ();
		}

		if (GUILayout.Button ("Build Menu")) {
			LevelManagerEditor.ResetLevelList ();
			menu.BuildMenu ();
		}
	}
}
