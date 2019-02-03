using UnityEngine;
using System.Collections.Generic;

public class ClickPlayerControl : PlayerControlBase {
	bool isMoving;

	void Update()
	{
		CheckClick ();
	}

	void CheckClick() {
		if (Input.GetMouseButtonDown (0))
		{
			HandleMouse (true);
		}
		else if (Input.GetMouseButton(0))
		{
			HandleMouse (false);
		}
	}

	void HandleMouse(bool clicked) {
		Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(screenRay, out hit))
		{
			HuntOrMoveToDestination (hit, clicked);
		}
	}

	void HuntOrMoveToDestination (RaycastHit hit, bool clicked)
	{
		NextAction = null;
		isMoving = false;
		if (clicked) {
			// clicking -> attack or move
			var go = hit.collider.gameObject;
			var unit = go.GetComponent<Living> ();

			if (unit != null) {
				// clicked a unit -> attack if enemy
				// TODO: Move to Unit, then start attacking
				NextAction = new Strategies.HuntTargetAction {
					target = unit
				};
			} else {
				// did not click on a unit -> start moving
				NextAction = new Strategies.MoveToDestinationAction {
					destination = hit.point
				};
				isMoving = true;
			}
		}
		else {
			// dragging -> keep moving, if already moving
			if (isMoving) {
				NextAction = new Strategies.MoveToDestinationAction {
					destination = hit.point
				};
			}
		}
	}
}