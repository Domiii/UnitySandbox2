using UnityEngine;
using System.Collections;

/// <summary>
/// AI strategies are high-level components used to control actuators.
/// Brains decide which strategy to activate at any given time.
/// </summary>
public abstract class AIStrategy<A> : AIStrategy 
	where A : AIAction
{
	public virtual void StartBehavior (A action) {
	}

	public override void StartStrategy(AIAction action) {
		StartBehavior ((A)action);
	}
}


public class AIStrategy : MonoBehaviour {
	public System.Action<AIStrategy> FinishedHandler;
	public bool isEnabled = true;


	void Update() {
		if (isEnabled) {
			UpdateStrategy ();
		}
	}

	protected virtual void UpdateStrategy() {
	}

	public virtual void StartStrategy(AIAction action) {
		isEnabled = true;
	}

	public void StopStrategy() {
		NotifyFinished ();
		OnStop ();
		isEnabled = false;
	}

	protected void NotifyFinished() {
		if (FinishedHandler != null) {
			FinishedHandler (this);
		}
	}

	protected virtual void OnStop() {
	}

	protected void StartOtherStrategies() {
		SendMessage ("OnStartOtherStrategies", this, SendMessageOptions.DontRequireReceiver);
	}

	protected void StopOtherStrategies() {
		SendMessage ("OnStopOtherStrategies", this, SendMessageOptions.DontRequireReceiver);
	}

	void OnResumeOtherStrategies(AIStrategy sender) {
		if (sender != this) {
			// resume
			isEnabled = true;
		}
	}

	void OnStopOtherStrategies(AIStrategy sender) {
		if (sender != this) {
			StopStrategy ();
		}
	}
}