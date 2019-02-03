
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentUtil {
	/**
	 * Source: http://answers.unity3d.com/questions/458207/copy-a-component-at-runtime.html
	 */
	public static T CopyComponent<T>(T original, GameObject destination) where T : Component
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy as T;
	}

	public static T GetComponentInHierarchy<T>(this GameObject target) 
		where T : Component
	{
		var comp = target.GetComponent<T>();
		if (comp == null && target.transform.parent != null) {
			// check if parent exists (and recurse through all ancestors)
			comp = GetComponentInHierarchy<T>(target.transform.parent.gameObject);
		}
		return comp;
	}

	public static T GetComponentInHierarchy<T, T2>(this T2 target) 
		where T : Component
		where T2 : Component
	{
		return GetComponentInHierarchy <T>(target.gameObject);
	}
}
