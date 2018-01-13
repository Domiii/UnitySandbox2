using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public float padding = 5;
	public float buttonWidth = 160;
	public float buttonHeight = 60;
	public LevelButton levelButtonPrefab;
	public Transform levelButtonParent;

	public Transform RealLevelButtonParent {
		get {
			return levelButtonParent != null ? levelButtonParent : transform;
		}
	}

	public void ClearMenu() {
		var children = RealLevelButtonParent.GetComponentsInChildren<LevelButton> ();
		foreach (var child in children) {
			DestroyImmediate (child.gameObject);
		}

		#if UNITY_EDITOR
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		#endif
	}

	public void BuildMenu() {
		ClearMenu ();

		var levels = LevelManager.Instance.levels;

		var parent = RealLevelButtonParent;
		var parentRect = parent.GetComponent<RectTransform>().rect;
		//var buttonRT = levelButtonPrefab.GetComponent<RectTransform> ();
		//var buttonRect = buttonRT.rect;

		var parentW = parentRect.width;
		var parentH = parentRect.height;
		var nCols = (int)((parentW - padding) / (buttonWidth + padding));
		var nRows = (int)Mathf.Ceil(levels.Length / (float)nCols);


		var fullW = nCols * (buttonWidth + padding) + padding;
		var margin = (parentW - fullW) / 2;

		var iButton = 0;
		var y = parentH - padding;
		for (var j = 0; j < nRows; ++j) {
			var x = margin;

			for (var i = 0; i < nCols && iButton < levels.Length; ++i) {
				var lvl = levels [iButton++];
				var btn = (LevelButton)Instantiate(levelButtonPrefab, parent, true);
				var newRectTransform = btn.GetComponent<RectTransform> ();
				newRectTransform.localScale = Vector3.one;
				newRectTransform.sizeDelta = Vector2.one;
				newRectTransform.offsetMax = newRectTransform.offsetMin = Vector2.zero;
				newRectTransform.anchorMin = new Vector2 (x/parentW, (y-buttonHeight)/parentH);
				newRectTransform.anchorMax = new Vector2 ((x+buttonWidth)/parentW, (y)/parentH);

				var text = btn.GetComponentInChildren<Text> ();
				text.text = lvl;
				btn.level = lvl;

				x += buttonWidth + padding;
			}
			y -= buttonHeight + padding;
		}

		#if UNITY_EDITOR
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		#endif
	}
}
