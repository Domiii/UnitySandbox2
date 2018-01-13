using UnityEngine;
using System.Collections;
using System.Linq;


/// <summary>
/// 3D adaption of the famous DontGoThroughThings component (http://wiki.unity3d.com/index.php?title=DontGoThroughThings).
/// Uses raycasting to trigger OnTriggerEnter events when hitting something to prevent temporal aliasing (tunneling).
/// </summary>
/// <see cref="http://stackoverflow.com/a/29564394/2228771"/>
public class ProjectileCollisionTrigger : MonoBehaviour {
	public enum TriggerTarget {
		None = 0,
		Self = 1,
		Other = 2,
		Both = 3
	}

	/// <summary>
	/// The layers that can be hit by this object.
	/// Defaults to "Everything" (-1).
	/// </summary>
	public LayerMask hitLayers = -1;

	/// <summary>
	/// The name of the message to be sent on hit.
	/// You generally want to change this, especially if you want to let the projectile apply a force (`momentumTransferFraction` greater 0).
	/// If you do not change this, the physics engine (when it happens to pick up the collision) 
	/// will send an extra message, prior to this component being able to. This might cause errors or unexpected behavior.
	/// </summary>
	public string MessageName = "OnProjectileHit";

	/// <summary>
	/// Where to send the hit event message to.
	/// </summary>
	public TriggerTarget triggerTarget = TriggerTarget.Self;

	/// <summary>
	/// How much of momentum is transfered upon impact.
	/// If set to 0, no force is applied.
	/// If set to 1, the entire momentum of this object is transfered upon the first collider and this object stops dead.
	/// If set to anything in between, this object will lose some velocity and transfer the corresponding momentum onto every collided object.
	/// </summary>
	public float momentumTransferFraction = 0;

	private Vector3 previousPosition;
	private Rigidbody myRigidbody;
	private Collider myCollider;

	
	//initialize values 
	void Awake()
	{
		myRigidbody = GetComponent<Rigidbody>();
		myCollider = GetComponents<Collider> ().FirstOrDefault();
		myCollider = myCollider ?? transform.GetComponentInChildren<Collider> ();
		if (myCollider == null || myRigidbody == null) {
			Debug.LogError(GetType().Name + " is missing Collider or Rigidbody component", this);
			enabled = false;
			return;
		}

		previousPosition = myRigidbody.transform.position;
	}
	
	void FixedUpdate()
	{
		//have we moved more than our minimum extent? 
		var origPosition = transform.position;
		var movementThisStep = transform.position - previousPosition;
		float movementSqrMagnitude = movementThisStep.sqrMagnitude;
		
		if (movementSqrMagnitude > float.Epsilon) {
			float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
			
			//check for obstructions we might have missed 
			RaycastHit[] hitsInfo = Physics.RaycastAll(previousPosition, movementThisStep, movementMagnitude, hitLayers.value);

			for (int i = 0; i < hitsInfo.Length; ++i) {
				var hitInfo = hitsInfo[i];
				if (hitInfo.collider != null && hitInfo.collider != myCollider) {
					// apply force
					if (hitInfo.rigidbody != null && momentumTransferFraction != 0) {
						// When using impulse mode, the force argument is actually the amount of instantaneous momentum transfered.
						// Quick physics refresher: F = dp / dt = m * dv / dt
						// Note: dt is the amount of time traveled (which is the time of the current frame and is taken care of internally, when using impulse mode)
						// For more info, go here: http://forum.unity3d.com/threads/rigidbody2d-forcemode-impulse.213397/
						var dv = myRigidbody.velocity;
						var m = myRigidbody.mass;
						var dp = dv * m;
						var impulse = momentumTransferFraction * dp;
						hitInfo.rigidbody.AddForceAtPosition(impulse, hitInfo.point, ForceMode.Impulse);

						if (momentumTransferFraction < 1) {
							// also apply force to self (in opposite direction)
							var impulse2 = (1-momentumTransferFraction) * dp;
							hitInfo.rigidbody.AddForceAtPosition(-impulse2, hitInfo.point, ForceMode.Impulse);
						}
					}

					// move this object to point of collision
					transform.position = hitInfo.point;

					SendMessages (hitInfo.collider);
				}
			}
		}

		previousPosition = transform.position = origPosition;
	}

	void OnTriggerEnter(Collider collider) {
		SendMessages (collider);
	}

	void SendMessages(Collider otherCollider) {
		// send hit messages
		if (((int)triggerTarget & (int)TriggerTarget.Other) != 0) {
			otherCollider.SendMessage(MessageName, myCollider, SendMessageOptions.DontRequireReceiver);
		}
		if (((int)triggerTarget & (int)TriggerTarget.Self) != 0) {
			SendMessage(MessageName, otherCollider, SendMessageOptions.DontRequireReceiver);
		}
	}
}
