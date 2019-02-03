using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public string levelPrefix = "Level";
	public string mainMenuScene = "MainMenu";
	public string[] levels;
	public Canvas wonDisplay;
	public Canvas lostDisplay;

	public static LevelManager Instance {
		get;
		private set;
	}

	LevelManager() {
		Instance = this;
	}

	public string CurrentSceneName {
		get { return SceneManager.GetActiveScene ().name; }
	}

	void Start() {
		OnLevelStart ();
	}

	void Reset() {
		//		// we gonna keep this guy!
		//		DontDestroyOnLoad (gameObject);
		//		if (transform.parent != null) {
		//			DontDestroyOnLoad (transform.parent.gameObject);
       		//		}
	}

	string GetLevelCompletedKey(string level) {
		return "level__" + level;
	}

	public int GetLevelIndex(string name) {
		return System.Array.IndexOf(levels, name);
	}

	bool HasAlreadyCompletedLevel(string name) {
		return PlayerPrefs.GetInt (GetLevelCompletedKey(name), 0) > 0;
	}

	void SetLevelCompleted(string name, bool completed) {
		PlayerPrefs.SetInt (GetLevelCompletedKey(name), completed ? 1 : 0);
		PlayerPrefs.Save ();
	}

	void OnLevelStart() {
		if (wonDisplay != null) {
			wonDisplay.gameObject.SetActive (false);
			lostDisplay.gameObject.SetActive (false);
		}
		//GameManager.Instance.IsPaused = false;
	}

	public bool IsLevelUnlocked(string name) {
		// level is unlocked if the previous level has already been completed
		var previousLevel = GetPreviousLevel (name);
		return previousLevel == null || HasAlreadyCompletedLevel (previousLevel);
	}

	public void ResetPlayerData() {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();

		RestartCurrentScene ();
	}

	public void NotifyLevelWon() {
		SetLevelCompleted (CurrentSceneName, true);
		wonDisplay.gameObject.SetActive (true);
		GameManager.Instance.IsPaused = true;
	}

	public void NotifyLevelLost() {
		GameManager.Instance.IsPaused = true;
		lostDisplay.gameObject.SetActive (true);
	}

	public void RestartCurrentScene() {
		GotoLevel (CurrentSceneName);
	}

	public void GotoNextLevel() {
		var level = GetNextLevel ();
		if (level != null) {
			GotoLevel (level);
		}
	}

	public void GotoMainMenu() {
		GotoLevel (mainMenuScene);
	}

	public string GetPreviousLevel(string name) {
		var index = GetLevelIndex (name) - 1;
		if (index >= 0 && index < levels.Length) {
			// go to next level!
			return levels [index];
		} else {
			// there are no more levels!
			return null;
		}
	}

	public string GetNextLevel() {
		var index = GetLevelIndex (CurrentSceneName) + 1;
		if (index > 0 && index < levels.Length) {
			// go to next level!
			return levels [index];
		} else {
			// there are no more levels!
			return null;
		}
	}

	public void GotoLevel(string level) {
		//OnLevelStart ();
		SceneManager.LoadScene(level);
	}
}
