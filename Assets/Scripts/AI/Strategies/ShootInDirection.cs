using UnityEngine;
using System.Collections;

namespace Strategies {
	public class ShootInDirectionAction : AIAction {
		public Vector3 destination;
	}

	[RequireComponent(typeof(Shooter))]
	public class ShootInDirection : AIStrategy<ShootInDirectionAction> {
		Shooter shooter;

		void Awake() {
			shooter = GetComponent<Shooter> ();
		}

		public override void StartBehavior (ShootInDirectionAction action)
		{
			shooter.StartShootingAt (action.destination);
		}

		protected override void OnStop ()
		{
			shooter.StopShooting ();
		}
	}

}