using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(Brain))]
public class BrainEditor : Editor {
	public override void OnInspectorGUI () {
		base.OnInspectorGUI ();

		var brain = (Brain)target;

		var strategies = StrategyManager.StrategyTypes;
		HashSet<Component> toRemove = null;
		foreach (var strategy in strategies) {
			var comp = brain.GetComponent (strategy);
			var added = comp != null;
			var add = EditorGUILayout.Toggle(strategy.Name, added);
			var changed = add != added;

			if (changed && add) {
				// add component
				brain.gameObject.AddComponent (strategy);
			} else if (changed && !add) {
				// remove component
				toRemove = toRemove ?? new HashSet<Component> ();
				toRemove.Add (comp);
			}
		}

		if (toRemove != null) {
			// remove the selected strategy components and all its dependencies (if they are no longer needed by any other component)
			var remainingComponents = new HashSet<Component>(brain.gameObject.GetComponents<Component>());
			remainingComponents.ExceptWith(toRemove);

			var allRemainingComponents = GetAllDependentComponents (remainingComponents);
			var toRemoveComponents = GetAllDependentComponents (toRemove);

			toRemoveComponents.ExceptWith (allRemainingComponents);

			toRemoveComponents.ForEach(comp => DestroyImmediate(comp));
			Repaint ();
		}
	}

	bool HasStrategy(System.Type strategyType) {
		var brain = (Brain)target;
		return brain.GetComponent (strategyType) != null;
	}

	HashSet<Component> GetAllDependentComponents(IEnumerable<Component> roots) {
		var visited = new HashSet<Component> ();
		roots.ForEach(comp => GetAllDependentComponents(comp, visited));
		return visited;
	}

	void GetAllDependentComponents(System.Type type, HashSet<Component> visited) {
		var brain = (Brain)target;
		var comp = brain.GetComponent (type);
		GetAllDependentComponents (comp, visited);
	}
		

	void GetAllDependentComponents(Component comp, HashSet<Component> visited) {
		if (visited.Contains (comp)) {
			// don't follow circular dependencies
			return;
		}

		visited.Add (comp);
		var requiredComponents = (RequireComponent[])comp.GetType().GetCustomAttributes(typeof(RequireComponent), true);

		foreach (var rc in requiredComponents) {
			if (rc.m_Type0 != null) {
				GetAllDependentComponents(rc.m_Type0, visited);
			}
			if (rc.m_Type1 != null) {
				GetAllDependentComponents(rc.m_Type1, visited);
			}
			if (rc.m_Type2 != null) {
				GetAllDependentComponents(rc.m_Type2, visited);
			}
		}
	}
}
