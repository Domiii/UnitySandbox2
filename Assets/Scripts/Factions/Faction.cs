using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class Faction : MonoBehaviour {
	FactionType _factionType;
	
	[SerializeField]
	private int _credits = 100;
	
	public int Credits {
		get { return _credits; }
		set {
			_credits = value;
			UpdateText();
		}
	}

	// Use this for initialization
	void Awake () {
		FactionManager.SetFaction(gameObject, _factionType);

		UpdateText ();
	}
	
	protected Faction(FactionType factionType) {
		_factionType = factionType;
		
		FactionManager.RegisterFaction (factionType, this);
	}
	
	#region UI
	public Text creditText;
	public void UpdateText() {
		if (creditText != null) {
			creditText.text = Credits.ToString ();
		}
	}
	#endregion

	void OnDeath(DamageInfo damageInfo) {
		if (_factionType == FactionType.Player) {
			// player lost
			//GameManager.Instance.LoseGame();
		} else {
			// player won
			//GameManager.Instance.WinGame();
		}
	}
}
