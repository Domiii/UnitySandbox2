using UnityEngine;
using System.Collections;

namespace Strategies {
	public class HuntTargetAction : AIAction {
		public Living target;
	}

	/// <summary>
	/// Attack given target when close enough; else move and catch up
	/// </summary>
	[RequireComponent(typeof(UnitAttacker))]
	[RequireComponent(typeof(NavMeshMover))]
	public class HuntTarget : AIStrategy<HuntTargetAction> {
		UnitAttacker attacker;
		NavMeshMover mover;
		bool hadValidTarget = false;

		#region Public
		public Living CurrentTarget {
			get {
				return attacker.CurrentTarget;
			}
		}

		public override void StartBehavior(HuntTargetAction action) {
			attacker.StartAttack(action.target);
			mover.StopMovingAtDestination = false;
		}
		#endregion

		void Awake () {
			attacker = GetComponent<UnitAttacker> ();
			mover = GetComponent<NavMeshMover> ();
		}

		protected override void UpdateStrategy() {
			// current target out of range -> move to catch up
			if (attacker.IsCurrentValid) {
				if (!hadValidTarget) {
					// new target found! stop everything else.
					hadValidTarget = true;
					StopOtherStrategies ();
				}
				if (attacker.IsCurrentInRange) {
					// keep attacking; also: make sure, we are not moving
					mover.StopMove();
				}
				else {
					// target out of range -> move toward target
					mover.CurrentDestination = attacker.CurrentTarget.transform.position;
					attacker.StopAttack ();
				}
			}
			else if (hadValidTarget) {
				// we have no more valid target anymore (target might have died, disappeared, turned etc) -> done!
				StartOtherStrategies();
			}
		}

		/// <summary>
		/// Called when finished hunting.
		/// </summary>
		protected override void OnStop() {
			attacker.StopAttack();
			mover.StopMove ();
		}
	}
}