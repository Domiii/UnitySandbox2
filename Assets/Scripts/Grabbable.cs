using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Objects of this component can be grabbed by the player's gravitational pull ability.
 */
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {
	void OnMouseDown() {
		var player = Player.instance;
		var gravityController = player.GetComponent<GravityController> ();

		gravityController.PickUp (GetComponent<Rigidbody>());
	}
}
