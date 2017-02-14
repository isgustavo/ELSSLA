﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class OffLinePlayerBehaviour : MonoBehaviour {

	private const float ROTATE_AMOUNT = 2;

	private float movementSpeed = 0f;
	private float speed = 5f;

	void Update () {

		Rotation ();

		Movement ();


	}

	void Start () {

		Camera2DFollow camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow>();

		camera.setTarget (transform);

	}

	private void Rotation () {

		float tiltValue = GetTiltValue();
		Vector3 oldAngles = this.transform.eulerAngles;
		//this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (1 * ROTATE_AMOUNT));
		this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (tiltValue * ROTATE_AMOUNT));
	}

	private void Movement () {
		//Debug.Log (isMovement);
		if (isMovementButtonPointDown) {

			transform.position += transform.up * Time.deltaTime * speed;
			movementSpeed = speed;
		} else {

			if (movementSpeed > 0) {
				movementSpeed -= 0.05f;
				transform.position += transform.up * Time.deltaTime * movementSpeed;
			}

		}

	}

	float GetTiltValue() {
		float TILT_MIN = 0.05f;
		float TILT_MAX = 0.2f;

		// Work out magnitude of tilt
		float tilt = Mathf.Abs(Input.acceleration.x);

		// If not really tilted don't change anything
		if (tilt < TILT_MIN) {
			return 0;
		}
		float tiltScale = (tilt - TILT_MIN) / (TILT_MAX - TILT_MIN);

		// Change scale to be negative if accel was negative
		if (Input.acceleration.x < 0) {
			return -tiltScale;
		} else {
			return tiltScale;
		}
	}

	bool isMovementButtonPointDown = false;

	public void TapToMovementEventTrigger (bool state) {

		isMovementButtonPointDown = state;
	}

}