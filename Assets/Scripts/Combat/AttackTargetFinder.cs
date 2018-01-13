using UnityEngine;

public class AttackTargetFinder : MonoBehaviour {
	Collider[] collidersInRange;


	public bool IsValidTarget (Living target) {
		//Debug.Log(gameObject+ " vs. " + target.gameObject + ": " + FactionManager.AreHostile (gameObject, target.gameObject));
		return target.CanBeAttacked && FactionManager.AreHostile (gameObject, target.gameObject);
	}

	public Living FindTarget (float radius) {
		if (collidersInRange == null) {
			collidersInRange = new Collider[128];
		}
		var nResults = Physics.OverlapSphereNonAlloc (transform.position, radius, collidersInRange);
		for (var i = 0; i < nResults; ++i) {
			var collider = collidersInRange [i];
			var unit = Living.GetLiving (collider);
			if (unit && IsValidTarget (unit)) {
				return unit;
			}
		}

		// no valid target found
		return null;
	}
}
