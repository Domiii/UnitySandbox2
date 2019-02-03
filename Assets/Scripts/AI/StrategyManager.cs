using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;



/// <summary>
/// Provides Action <-> Strategy Mapping
/// </summary>
public class StrategyManager : UnityEngine.MonoBehaviour {
	//[UnityEngine.Multiline]
	//public string strategies = "hi\nworld";

	static Type[] strategyTypes;
	static Type[] actionStrategyTypes;
	static Dictionary<Type, Type> strategyTypesByActionType;

//	static StrategyManager instance;

	static StrategyManager() {
		RegisterAllStrategies();
	}

	static void RegisterAllStrategies() {
		RegisterAllStrategies (Assembly.GetExecutingAssembly());
	}

	static Type GetActionType(Type t) {
		var baseType = t.BaseType;
		if (baseType.GetGenericArguments().Length != 1 || !baseType.GetGenericArguments()[0].IsSubclassOf(typeof(AIAction))) {
			//UnityEngine.Debug.LogError("Invalid Strategy definition does not inherit from AIStrategy<AIAction>: " + t.FullName);
			return null;
		}
		return baseType.GetGenericArguments()[0];
		
	}

	static void RegisterAllStrategies(Assembly assembly) {
		strategyTypes = assembly.GetTypes ().Where(t => t.IsSubclassOf(typeof(AIStrategy)) && !t.IsAbstract).ToArray();
		actionStrategyTypes = strategyTypes.Where(t => GetActionType(t) != null).ToArray();

		strategyTypesByActionType = actionStrategyTypes.ToDictionary<Type, Type>(GetActionType);
	}

	public static Type[] StrategyTypes {
		get { return strategyTypes; }
	}

	public static Type GetStrategyTypeForAction(AIAction action) {
		Type t;
		strategyTypesByActionType.TryGetValue (action.GetType (), out t);
		return t;
	}

	StrategyManager() {
//		instance = this;
//		UpdateInstance ();
	}

//	static void UpdateInstance() {
//		if (instance != null) {
//			instance.strategies = string.Join ("\n", strategyTypesByActionType.Values.Where (v => v != null).Select (t => t.Name).ToArray ());
//		}
//	}
}