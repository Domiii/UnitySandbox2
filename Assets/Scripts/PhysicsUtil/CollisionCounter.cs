using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class CollisionCounter : MonoBehaviour
{
	public int collisionCount;

	public bool HasAnyCollisions {
		get { return collisionCount > 0; }
	}

	void OnCollisionEnter (Collision other)
	{
		++collisionCount;
	}

	void OnCollisionExit (Collision other)
	{
		--collisionCount;
	}
}