using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 用 Input 來控制 Player 的 行為
/// </summary>
public abstract class PlayerControlBase : BehaviorController {

	public AIAction NextAction {
		get;
		protected set;
	}

	/// <summary>
	/// Get player's intended action and resets NextAction, indicating that the action has been handled.
	/// </summary>
	public override AIAction PopAction() {
		if (NextAction != null) {
			var action = NextAction;
			NextAction = null;
			return action;
		}
		return null;
	}

}