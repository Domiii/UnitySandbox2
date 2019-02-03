using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	bool isPaused;

	public static GameManager Instance {
		get;
		private set;
	}

	GameManager() {
		Instance = this;
	}

	void Awake() {
		Reset ();
	}

	void Reset() {
		IsPaused = false;
		Time.timeScale = 1;
	}

	public bool IsPaused {
		get {
			return isPaused;
		}
		set {
			if (isPaused != value) {
				if (value) {
					// pause
					Time.timeScale = 0;
				} else {
					// unpause!
					Time.timeScale = 1;
				}
				isPaused = value;
			}
		}
	}

	#region Temp objects
	public GameObject tempObject;

	static string tempName = "__temp";

	public static GameObject Temp {
		get {
			if (Instance.tempObject == null) {
				Instance.tempObject = GameObject.Find (tempName);
				if (Instance.tempObject == null) {
					Instance.tempObject = CreateTemp ();
				}
			}
			return Instance.tempObject;
		}
	}

	static GameObject CreateTemp() {
		var go = new GameObject (tempName);
		//go.SetActive (false);
		return go;
	}
	#endregion
}
