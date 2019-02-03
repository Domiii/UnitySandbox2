using UnityEngine;
using System.Collections;

public class FactionMember : MonoBehaviour {
	#if UNITY_EDITOR
	[HideInSubClass]
	#endif
	public FactionType FactionType;
}
