using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * There should be one object in the world with this component,
 * representing the Player-controlled character.
 */
public class Player : MonoBehaviour {
	/// <summary>
	/// Through this static global variable, we can always identify the unique player character object.
	/// </summary>
	public static Player instance;

	void Start () {
		instance = this;
	}
}
