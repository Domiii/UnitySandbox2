using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	//public GameObject owner;
	public bool destroyOnCollision = false;

	public float pushPower = 100;

	[HideInInspector]
	public float lifeTime = 10;
	[HideInInspector]
	public float damageMin;
	[HideInInspector]
	public float damageMax;

	bool isDestroyed = false;

	void Start () {
		Destroy (gameObject, lifeTime);		// destroy after at most lifeTime seconds
	}

	void OnTriggerEnter (Collider col) {
		if (isDestroyed) {
			// do nothing
			return;
		}

		var target = Living.GetLiving(col.gameObject);
		if (target != null) {
			// when colliding with Unit -> Check if we can attack the Unit
			if (target.CanBeAttacked && FactionManager.AreHostile (gameObject, target.gameObject)) {
				DamageTarget (target);
			}
		}
		//else if (col.gameObject != owner && col.GetComponent<Bullet>() == null && col.GetComponentInParent<Bullet>() == null && destroyOnCollision) {
		else if (!FactionManager.AreAllied (gameObject, col.gameObject)) {
			// hit something that is not an enemy unit -> Destroy anyway
			//			print (string.Format("{0} vs. {1} ({2}, {3})", 
			//				FactionManager.GetFactionType(gameObject), FactionManager.GetFactionType(col.gameObject),
			//				gameObject.name, col.gameObject.name));

			if (pushPower != 0) {
				// push the other object!
				var otherBody = col.gameObject.GetComponent<Rigidbody> ();
				if (otherBody) {
					var targetPos = col.ClosestPointOnBounds (transform.position);
					otherBody.AddForceAtPosition (transform.forward * pushPower, targetPos, ForceMode.Impulse);
				}
			}

			if (destroyOnCollision) {
				// destroy on impact!
				DestroyThis ();
			}
		}
	}

	void DamageTarget (Living target) {
		// damage the unit!
//		var damage = Random.Range (damageMin, damageMax);
//		target.Damage (damage, FactionManager.GetFactionType(gameObject));
//		DestroyThis ();

		// damage the unit!
		//var damageInfo = ObjectManager.Instance.Obtain<DamageInfo> ();
		var damageInfo = new DamageInfo ();
		damageInfo.Value = Random.Range (damageMin, damageMax);
		damageInfo.SourceFactionType = FactionManager.GetFactionType (gameObject);
		target.Damage (damageInfo);
		DestroyThis ();
	}

	void DestroyThis () {
		Destroy (gameObject);
		isDestroyed = true;
	}
}