using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;

public class PlayerPositionBehaviour : NetworkBehaviour {

	private const float POSITION_SPEED = 10f;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();

	}
		
	void FixedUpdate() {

		if (!isLocalPlayer)
			return;

		//Debug.Log ("Position"+transform.position);

		if (MoveButtonBehaviour.isMoveOn) {

			rb.AddForce (transform.up * POSITION_SPEED, ForceMode.Acceleration);
		}
	}

}
