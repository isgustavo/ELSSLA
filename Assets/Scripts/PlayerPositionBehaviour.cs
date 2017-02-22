using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;

public class PlayerPositionBehaviour : NetworkBehaviour {

	private Rigidbody rigidbody;
	private float speed = 5f;
	private float movementSpeed = 0f;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		if (transform.position.z <= -6f) {
			Camera2DFollow camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow>();
			camera.setTarget (transform);
		}


		if (MoveButtonBehaviour.isMoveOn) {

			rigidbody.velocity = transform.up * speed;

			movementSpeed = speed;
		} else {

			if (movementSpeed > 0) {
				movementSpeed -= 0.05f;
				rigidbody.velocity = transform.up * movementSpeed;
			}
		}
	}

}
