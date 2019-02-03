using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
	public static PlayerInputManager Instance {
		get;
		private set;
	}

	public int nInputBlockers = 0;

	public PlayerInputManager() {
		Instance = this;
	}

	public bool IsDefaultGameInputEnabled {
		get {
			return nInputBlockers == 0;
		}
	}

	public void AddGameInputBlocker() {
		++nInputBlockers;
	}

	public void RemoveGameInputBlocker() {
		--nInputBlockers;
	}
}