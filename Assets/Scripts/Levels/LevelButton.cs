using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour {
	public string level;

	void Awake() {
		Reset();
	}

	void Reset() {
		var button = GetComponent<Button> ();
		if (button != null) {
			button.interactable = LevelManager.Instance.IsLevelUnlocked(level);
			button.onClick.AddListener(() => LevelManager.Instance.GotoLevel (level));

		}
	}
}
