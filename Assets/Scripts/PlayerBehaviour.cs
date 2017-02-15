// Copyright 2017 ISGUSTAVO
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//          http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;

public class PlayerBehaviour : NetworkBehaviour {

	private const float ROTATE_AMOUNT = 2;
	private Rigidbody _rigidbody;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float timeBetweenFires = .3f;
	private float timeTilNextFire = 0.0f;

	private float movementSpeed = 0f;
	private float speed = 5f;

	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();

	}
	void Update () {

		if (!isLocalPlayer)
			return;

		//Vector3 newPos = transform.position;
		//if (transform.position.z > -5.9f) {
		//	newPos.z = Mathf.Lerp (transform.position.z, -6, Time.deltaTime * 5f);
		//} else {
		//	newPos.z = -6f;	
		//}
		//transform.position = newPos;

		Rotation ();



		Shooting ();
		timeTilNextFire -= Time.deltaTime;
	}


	void FixedUpdate () {

		if (!isLocalPlayer)
			return;

		Movement ();
	}

	private void Rotation () {

		float tiltValue = GetTiltValue();
		Vector3 oldAngles = this.transform.eulerAngles;
		this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (1 * ROTATE_AMOUNT));
		//this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (tiltValue * ROTATE_AMOUNT));
	}

	private void Movement () {
		//Debug.Log (isMovement);
		if (GameManagerBehaviour.Instance.isMovement) {

			_rigidbody.velocity = transform.up * speed;

			//transform.position += transform.up * Time.deltaTime * speed;
			movementSpeed = speed;
		} else {

			if (movementSpeed > 0) {
				movementSpeed -= 0.05f;
				_rigidbody.velocity = transform.up * movementSpeed;
				//transform.position += transform.up * movementSpeed;
			}

		}

	}

	private void Shooting() {

		if (GameManagerBehaviour.Instance.isShooting) {

			if (timeTilNextFire < 0) {
				this.timeTilNextFire = timeBetweenFires;
				CmdFire ();
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

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		Debug.Log ("OnStartLocalPlayer");

		Camera2DFollow camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow>();

		camera.setTarget (transform);
	}

	[Command]
	void CmdFire() {

		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);



		NetworkServer.Spawn(bullet);

	}
}
