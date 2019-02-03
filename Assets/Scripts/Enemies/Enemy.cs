using UnityEngine;
using System.Collections;

public class Enemy : FactionMember {
	Enemy() {
		Reset ();
	}

	void Awake() {
		Reset ();
	}

	void Reset() {
		FactionType = FactionType.Enemy;
	}

	void OnDeath(DamageInfo info) {
		EnemyManager.Instance.FinishedEnemy ();
	}
}
