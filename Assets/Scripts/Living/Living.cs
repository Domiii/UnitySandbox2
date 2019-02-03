using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour {
	#region Life, Health + Death
	/// <summary>
	/// 頂多多少生命值？
	/// </summary>
	public float MaxHealth = 100;

	/// <summary>
	/// 目前多少生命值？
	/// </summary>
	public float Health = 100;

	/// <summary>
	/// 無敵
	/// </summary>
	public bool isInvulnerable = false;

	/// <summary>
	/// 還活著嗎？
	/// </summary>
	public bool IsAlive {
		get { return Health > 0; }
	}

	/// <summary>
	/// 在哪一些狀況之下才可以攻擊這個單位？
	/// </summary>
	public bool CanBeAttacked {
		get {
			return IsAlive && !isInvulnerable;
		}
	}

	/// <summary>
	/// 死了才呼叫這個函數
	/// </summary>
	void Die(DamageInfo damageInfo) {
		Health = 0;

		// Send message, let everyone know that damage has occurred.  廣播，讓大家知道 Unit 死了
		SendMessage ("OnDeath", damageInfo, SendMessageOptions.DontRequireReceiver);

		// Destroy/Delete/刪除 this Unit GO
		Destroy (gameObject);
	}
	#endregion

	#region Attack
	/// <summary>
	/// 我們被傷害的時候呼叫這個函數
	/// </summary>
	public void Damage(DamageInfo damageInfo) {
		if (!CanBeAttacked) {
			// already dead or invul 早就死 或是 無敵
			return;
		}

		// reduce health 扣血
		Health -= damageInfo.Value;
		
		if (!IsAlive) {
			// died from damage 這次才死
			Die(damageInfo);
		}
	}
   	#endregion

	public static Living GetLiving<C>(C component) 
		where C : Component
	{
		return GetLiving (component.gameObject);
	}

	/// <summary>
	/// Helper method to get the Living component of the given GO or its ancestors.
	/// </summary>
	public static Living GetLiving(GameObject go) {
		return go.GetComponentInHierarchy <Living>();
	}
}