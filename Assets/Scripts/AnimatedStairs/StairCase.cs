using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StairCase : MonoBehaviour {
	public StairStep stepPrefab;
	public int nSteps = 10;
	public float depth = 1;
	public float height = 1;

	public float narrowWidth = 1;
	public float wideWidth = 2;
	public float animSpeed = 1;

	void Start () {
		
	}

	public void ClearStairs() {
		for (var i = transform.childCount-1; i >= 0; --i) {
			var child = transform.GetChild (i);
			DestroyImmediate(child.gameObject);
		}

		NotifyEditorChanges ();
	}

	public void BuildStairs() {
		var pos = Vector3.zero;	// use local space
		for (var i = 0; i < nSteps; ++i) {
			// instantiate
			#if UNITY_EDITOR
			var go = (StairStep)PrefabUtility.InstantiatePrefab (stepPrefab);
			#else
			var go = Instantiate (stepPrefab);
			#endif

			// transform into world space
			var worldPos = transform.TransformPoint (pos); // position is in parent space
			var worldRot = transform.rotation;	// rotation is same as parent

			go.transform.SetParent (transform);
			go.transform.SetPositionAndRotation (worldPos, worldRot);


			pos.y += height;
			pos.z += depth;
		}
		NotifyEditorChanges ();
	}

	/// <summary>
	/// Call this after making changes to the scene, so the changes can be saved to the scene file.
	/// </summary>
	void NotifyEditorChanges() {
		#if UNITY_EDITOR
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
		#endif
	}
}
