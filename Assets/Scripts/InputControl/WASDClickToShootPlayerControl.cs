using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WASDClickToShootPlayerControl : PlayerControlBase {
	public float speed = 8;

	bool mouseDown;
	Plane testPlane;

	public bool IsOnNavMesh(out NavMeshHit hit) {
		return NavMesh.Raycast (transform.position, transform.position + Vector3.down * 100, out hit, NavMesh.AllAreas);
	}

	Transform GetTransform() {
		Transform t;
		var shooter = GetComponent<Shooter> ();
		if (shooter) {
			t = shooter.shootTransform;
		} else {
			t = transform;
		}
		return t;
	}

	void Awake() {
		// create a plane at 0,0,0 whose normal points to +Y
		testPlane = new Plane ();
	}

	Vector3 moveDelta;

	void Update() {
		CheckClick ();

		moveDelta.x = Input.GetAxis("Horizontal");
		moveDelta.y = 0;
		moveDelta.z = Input.GetAxis("Vertical");
		moveDelta.Normalize ();
	}

	void FixedUpdate() {
		Move ();
		TurnTowardCursor ();
	}

	void Move() {
		MoveByInput();
		//PlaceOnNavMesh ();
	}

	void MoveByInput() {
		//transform.position = transform.position + transform.TransformDirection(delta) * speed * Time.fixedDeltaTime;
		GetComponent<Rigidbody>().MovePosition(transform.position + transform.TransformDirection(moveDelta) * speed * Time.fixedDeltaTime);
	}

	void TurnTowardCursor() {
		float turnSpeed = 720;
		var shooter = GetComponent<Shooter> ();
		if (shooter) {
			turnSpeed = shooter.turnSpeed;
		} else {
		}

		Vector3 dest;
		if (GetCursorPos (out dest)) {
			//GetTransform().RotateTowardTarget (dest, turnSpeed);
		}
	}

	void PlaceOnNavMesh() {
		NavMeshHit hit;
		if (GetComponent<UnityEngine.AI.NavMeshAgent>() && !IsOnNavMesh(out hit)) {
			// place back on navmesh
			if (NavMesh.SamplePosition (transform.position, out hit, 100, NavMesh.AllAreas)) {
				// get bounds, and place bottom face of bounds at position
				var mesh = GetComponent<Collider>();
				var originHeight = -mesh.bounds.min.y;
				transform.position = hit.position + Vector3.up * originHeight;
			}
		}
	}

	void CheckClick() {
		if (Input.GetMouseButtonDown (0)) {
			// mouse click -> start something
			// 點滑鼠 -> 開始做事
			HandleMouse (true);
			mouseDown = true;
		} else if (Input.GetMouseButton (0)) {
			// mouse button is pressed all the time
			// 繼續點滑鼠
			HandleMouse (false);
		} else if (mouseDown) {
			// mouse released -> stop it
			// 滑鼠被放解
			mouseDown = false;
			NextAction = Strategies.IdleAction.Default;
		} 
	}

	bool GetCursorPos(out Vector3 pos) {
		// see: http://answers.unity3d.com/questions/269760/ray-finding-out-x-and-z-coordinates-where-it-inter.html
		// cast ray onto plane that goes through our current position and normal points upward
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		testPlane.SetNormalAndPosition (transform.up, transform.position);
		float distance;
		if (testPlane.Raycast (ray, out distance)) {
			pos = ray.GetPoint (distance);
			return true;
		}
//		RaycastHit hit;
//		if (Physics.Raycast(ray, out hit)) {
//			pos = hit.point;
//			return true;
//		}
		pos = Vector3.zero;
		return false;
	}

	void HandleMouse(bool clicked) {
		Vector3 dest;
		if (GetCursorPos(out dest)) {
			NextAction = new Strategies.ShootInDirectionAction {
				destination = dest
			};
		}
	}
}