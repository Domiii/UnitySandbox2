using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBuilder : MonoBehaviour {
	public float detailLevel = 10;

	public void BuildSphere() {
		var vectors = new List<Vector3>();
		var indices = new List<int>();

		GeometryProvider.Icosahedron(vectors, indices);

		for (var i = 0; i < detailLevel; i++) {
			GeometryProvider.Subdivide (vectors, indices, true);
		}

		/// normalize vectors to "inflate" the icosahedron into a sphere.
		for (var i = 0; i < vectors.Count; i++) {
			vectors [i] = Vector3.Normalize (vectors [i]);
		}

	}
}
