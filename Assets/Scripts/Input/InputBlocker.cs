using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Blocks default game input while active
/// </summary>
public class InputBlocker : MonoBehaviour {
	void Start() {
		if (gameObject.activeSelf) {
			PlayerInputManager.Instance.AddGameInputBlocker ();
		}
	}

	void OnEnable() {
		PlayerInputManager.Instance.AddGameInputBlocker ();
	}

	void OnDisable() {
		PlayerInputManager.Instance.RemoveGameInputBlocker ();
	}
}