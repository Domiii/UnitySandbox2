using UnityEngine;
using System.Collections;

public class PlayerBrain : Brain {
	void Awake () {
		AddStrategy<Strategies.Idle> ();
		//AddStrategy<Strategies.HuntTarget> ();

		// 
		AddStrategy<Strategies.ShootInDirection> ();

		// Idle is the default strategy
		SetDefaultStrategy<Strategies.Idle>();
	}

	void OnDeath(DamageInfo damageInfo) {
		LevelManager.Instance.NotifyLevelLost ();
	}
}
