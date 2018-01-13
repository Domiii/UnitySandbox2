using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WanderAction : AIAction {
	public static readonly WanderAction Default = new WanderAction();
}

/// <summary>
/// Just move around randomly
/// </summary>
[RequireComponent(typeof(NavMeshMover))]
[RequireComponent(typeof(NavMeshAgent))]
public class Wander : AIStrategy<WanderAction> {
	float smoothness = 1;
	NavMeshMover mover;
	NavMeshAgent agent;

	void Start() {
		mover = GetComponent<NavMeshMover> ();
		agent = GetComponent<NavMeshAgent> ();
		MoveIntoRandomDirection ();
	}

	protected override void UpdateStrategy() {
		if (mover.HasArrived) {
			MoveIntoRandomDirection ();
		}
	}

	public override void StartBehavior(WanderAction action) {
		mover.StopMovingAtDestination = true;
		MoveIntoRandomDirection ();
	}

	void MoveIntoRandomDirection() {
		// determine new random point in space
		var dir = Random.insideUnitSphere;
		dir.y = 0;
		var ray = dir * smoothness * agent.speed;
		var newPos = transform.position + ray;


		// project point onto NavMesh
		NavMeshHit hit;
		if (NavMesh.SamplePosition (newPos, out hit, 1000, NavMesh.AllAreas)) {
			// Go!
			mover.CurrentDestination = hit.position;
		}
	}

	protected override void OnStop() {
		mover.StopMove ();
	}
}
