using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRotationBehaviour : NetworkBehaviour {

	private const float ROTATE_AMOUNT = 2;

	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		float tiltValue = GetTiltValue();
		Vector3 oldAngles = this.transform.eulerAngles;
		//this.transform.eulerAngles = new Vector3(0, 0, oldAngles.z + (1 * ROTATE_AMOUNT));
		this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (tiltValue * ROTATE_AMOUNT));

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

}
