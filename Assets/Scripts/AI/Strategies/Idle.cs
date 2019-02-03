using UnityEngine;
using System.Collections;

namespace Strategies {
	public class IdleAction : AIAction {
		public static readonly IdleAction Default = new IdleAction();
	}

	public class Idle : AIStrategy<IdleAction> {
		void Update () {
			// do nothing...
		}
	}
}