using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
	public static ResourceManager instance;

	public ResourceManager() {
		instance = this;
	}

	public string resourceName = "cubes";
	public int resourceCount = 0;
}
