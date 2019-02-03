/**
 * Source: http://answers.unity3d.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
 */

using System.Collections.Generic;
using UnityEngine;

public static class PrimitiveHelper
{
	private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();
	private static Dictionary<PrimitiveType, GameObject> primitives = new Dictionary<PrimitiveType, GameObject>();

	public static GameObject CreatePrimitive(PrimitiveType type, bool withCollider)
	{
		GameObject gameObject = new GameObject(type.ToString());

		DecoratePrimitive (gameObject, type);

		return gameObject;
	}


	public static void DecoratePrimitive(GameObject gameObject, PrimitiveType type, bool withCollider = true)
	{
		// add mesh
		var meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.sharedMesh = GetPrimitiveMesh(type);

		// add renderer
		//var renderer = gameObject.AddComponent<MeshRenderer>();

		// add material (if you want to...)
		//renderer.sharedMaterial = new Material(Shader.Find("Standard"));

		if (withCollider) {
			// add collider
			ComponentUtil.CopyComponent (GetPrimitiveCollider(type), gameObject);
		}

	}


	public static Mesh GetPrimitiveMesh(PrimitiveType type)
	{
		PrimitiveHelper.GetOrCreatePrimitive(type);

		return PrimitiveHelper.primitiveMeshes[type];
	}


	public static Collider GetPrimitiveCollider(PrimitiveType type)
	{
		return GetOrCreatePrimitive(type).GetComponent<Collider> ();
	}


	private static GameObject GetOrCreatePrimitive(PrimitiveType type)
	{
		GameObject go;
		if (!PrimitiveHelper.primitives.TryGetValue(type, out go) || go == null)
		{
			go = GameObject.CreatePrimitive(type);
			go.transform.parent = GameManager.Temp.transform;

			PrimitiveHelper.primitiveMeshes[type] = go.GetComponent<MeshFilter>().sharedMesh;
			PrimitiveHelper.primitives[type] = go;

			go.SetActive (false);
		}
		return go;
	}
}