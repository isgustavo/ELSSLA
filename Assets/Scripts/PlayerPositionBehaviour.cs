using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;

public class PlayerPositionBehaviour : NetworkBehaviour {

	private Rigidbody _rigidbody;
	private float speed = 5f;
	private float movementSpeed = 0f;

	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		if (MoveButtonBehaviour.isMoveOn) {

			_rigidbody.velocity = transform.up * speed;

			movementSpeed = speed;
		} else {

			if (movementSpeed > 0) {
				movementSpeed -= 0.05f;
				_rigidbody.velocity = transform.up * movementSpeed;
			}
		}
	}

}
