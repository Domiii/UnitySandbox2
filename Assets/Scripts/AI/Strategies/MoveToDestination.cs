using UnityEngine;
using System.Collections;

namespace Strategies {
	public class MoveToDestinationAction : AIAction {
		public Vector3 destination;
	}

	/// <summary>
	/// Move to destination, don't let anything distract you from that.
	/// </summary>
	[RequireComponent(typeof(NavMeshMover))]
	public class MoveToDestination : AIStrategy<MoveToDestinationAction> {
		NavMeshMover mover;

		void Awake () {
			mover = GetComponent<NavMeshMover> ();
		}

		#region Public
		public Vector3 CurrentDestination {
			get { 
				return mover.CurrentDestination;
			}
		}

		public override void StartBehavior(MoveToDestinationAction action) {
			mover.CurrentDestination = action.destination;
			mover.StopMovingAtDestination = true;
		}
		#endregion

		void Update () {
			if (mover.HasArrived) {
				StopStrategy ();
			}
		}

		/// <summary>
		/// Called when finished moving.
		/// </summary>
		protected override void OnStop() {
			mover.StopMove ();
		}
	}
}