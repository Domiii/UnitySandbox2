using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StepState {
	Narrow,
	Wide
}

public class StepDetector : MonoBehaviour {
	public StepState targetState = StepState.Narrow;

	StairCase stairCase;
	StairStep step;

	void Start () {
		step = transform.parent.GetComponent <StairStep>();
		stairCase = step.transform.parent.GetComponent<StairCase> ();
	}

	void UpdateAnimState(float delta) {
		var scale = transform.localScale;
		var x = scale.x + delta;
		x = Mathf.Clamp (x, stairCase.narrowWidth, stairCase.wideWidth);
		transform.localScale = new Vector3 (x, scale.y, scale.z);
	}

	void FixedUpdate () {
		var delay = stairCase.animDelay;
		var delta = Time.fixedDeltaTime * delay;
		if (targetState == StepState.Narrow) {
			// narrow goes in the opposite direction
			delta = -delta;
		}
		UpdateAnimState (delta);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player> ()) {
			targetState = StepState.Wide;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.GetComponent<Player> ()) {
			targetState = StepState.Narrow;
		}
	}
}
