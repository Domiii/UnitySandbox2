using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class NavMeshPathFollower : NavMeshMover {
	public NavPath path;
	public NavPath.FollowDirection direction;
	public NavPath.RepeatMode mode;

	float maxDistanceToGoal;
	IEnumerator<Transform> pathIterator;

	#region Public
	public void SetPath(NavPath path, NavPath.FollowDirection pathDirection = NavPath.FollowDirection.Forward, NavPath.RepeatMode mode = NavPath.RepeatMode.Once) {
		Debug.Assert (path != null);

		this.path = path;
		this.direction = pathDirection;
		this.mode = mode;

		RestartPath ();
	}

	public void RestartPath() {
		if (path != null) {
			pathIterator = path.GetPathEnumerator (direction);
			pathIterator.MoveNext ();
		}
	}
	#endregion


	void Start () {
		RestartPath ();
	}

	void Update () {
		MoveAlongPath ();
	}

	void MoveAlongPath() {
		if (pathIterator == null || pathIterator.Current == null)
			return;
		
		// move towards current target
		CurrentDestination = pathIterator.Current.position;
	}

	protected override void OnStartMove ()
	{
	}

	protected override void OnStopMove ()
	{
		pathIterator.MoveNext();
		//print("Next Waypoint: " + pathIterator.Current);

		if (pathIterator.Current == null) {
			// finished path once
			switch (mode) {
			case NavPath.RepeatMode.Once:
				// done!
				break;
			case NavPath.RepeatMode.Mirror:
				// reverse direction and walk back
				direction = direction == NavPath.FollowDirection.Forward ? NavPath.FollowDirection.Backward : NavPath.FollowDirection.Forward;
				RestartPath ();
				MoveAlongPath ();
				break;
			case NavPath.RepeatMode.Repeat:
				// start from the beginning
				RestartPath ();
				MoveAlongPath ();
				break;
			}
		}
	}
}
