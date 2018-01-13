using UnityEngine;
using System.Collections;
using System.Linq;

public static class FactionManager {
	#region Static Methods
	static Faction[] _factions;
	
	static FactionManager() {
		var maxFactionTypeValue = (int)System.Enum.GetValues(typeof(FactionType)).Cast<FactionType>().Max();
		
		_factions = new Faction[maxFactionTypeValue+1];
	}
	
	public static Faction GetFaction(FactionType factionType) {
		return _factions [(int)factionType];
	}
	
	internal static void RegisterFaction(FactionType factionType, Faction faction) {
		if (_factions [(int)factionType] != null) {
			//Debug.LogWarning(factionType + " Faction has been overwritten.");
		}
		
		_factions [(int)factionType] = faction;
	}

	public static bool AreHostile(GameObject obj1, GameObject obj2) {
		return GetFactionType(obj1) != GetFactionType(obj2);
	}

	public static bool AreAllied(GameObject obj1, GameObject obj2) {
		return GetFactionType(obj1) == GetFactionType(obj2);
	}
	
	public static Faction GetFaction(GameObject obj) {
		var factionType = GetFactionType (obj);
		return GetFaction (factionType);
	}
	
	public static FactionType GetFactionType(GameObject obj) {
		var factionMember = obj.GetComponent<FactionMember> ();
		if (factionMember == null) {
			factionMember = obj.GetComponentInParent<FactionMember> ();
		}
		return factionMember != null ? factionMember.FactionType : default(FactionType);
	}
	
	public static void SetFaction(GameObject dest, GameObject src) {
		SetFaction(dest, GetFactionType (src));
	}
	
	public static void SetFaction(GameObject dest, FactionType type) {
		// make sure, projectile has faction
		if (dest.GetComponent<FactionMember> () == null) {
			dest.AddComponent (typeof(FactionMember));
		}
		dest.GetComponent<FactionMember>().FactionType = type;
	}
	#endregion
}
