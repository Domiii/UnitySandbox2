using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor {
	LevelManagerEditor() {
	}

	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		//var levelManager = (LevelManager)target;

		if (GUILayout.Button ("Reset Level List")) {
			ResetLevelList ();
		}
	}

	/// <summary>
	/// Can currently only list all scenes in editor mode
	/// http://answers.unity3d.com/questions/1115796/scenemanagergetallscenes-only-returns-the-current.html
	/// </summary>
	public static void ResetLevelList() {
		var levelManager = (LevelManager)LevelManager.Instance;

		var levelList = new List<string> ();
		for (var i = 0; i < EditorBuildSettings.scenes.Length; ++i) {
			var scene = EditorBuildSettings.scenes[i];
			var name = System.IO.Path.GetFileNameWithoutExtension(scene.path);
			if (name.StartsWith (levelManager.levelPrefix)) {
				levelList.Add(name);
			}
		}
		levelManager.levels = levelList.ToArray ();
		EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
	}
}
