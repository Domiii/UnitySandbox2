using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
	public static EnemyManager Instance {
		get;
		private set;
	}

	EnemyManager() {
		Instance = this;
	}

	public Text text;

	public int FinishedEnemyCount {
		get;
		private set;
	}

	public int TotalEnemyCount {
		get;
		private set;
	}

	public void AddRemainingEnemies(int count) {
		TotalEnemyCount += count;
		UpdateCounts ();
	}

	public void FinishedEnemy() {
		++FinishedEnemyCount;
		UpdateCounts ();
	}

	void Start() {
		var enemies = GameObject.FindObjectsOfType<Enemy> ();
		AddRemainingEnemies (enemies.Length);
	}

	void UpdateCounts() {
		text.text = FinishedEnemyCount + " / " + TotalEnemyCount;
		if (FinishedEnemyCount >= TotalEnemyCount) {
			// we won! :D
			LevelManager.Instance.NotifyLevelWon ();
		}
	}
}
