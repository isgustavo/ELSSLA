using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel=0, sendInterval=0.0003f)]
public class PlayerSyncRotationBehaviour : NetworkBehaviour {

	[SyncVar (hook = "OnPlayerRotSynced")]
	private Quaternion syncPlayerRotation;

	private float normalLerpRate = 20f;
	private float fasterLerpRate = 45f;
	private Quaternion lastPlayerRotation;
	//private float threshoud = .2f;

	private List<Quaternion> syncPlayerRotationList = new List<Quaternion>();
	//private float closeEnough = 0.11f;

	void Update () {

		LerpRotation ();
	}

	void FixedUpdate () {

		TransmitRotation ();
	}

	void LerpRotation () {

		if (!isLocalPlayer) {

			float lerpRate = 0;

			if (syncPlayerRotationList.Count < 10) {
				lerpRate = normalLerpRate;
			} else {
				lerpRate = fasterLerpRate;
			}

			if (syncPlayerRotationList.Count > 0) {
				//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (rotation), Time.deltaTime * lerpRate);
				transform.rotation = Quaternion.Lerp (transform.rotation, syncPlayerRotationList [0], Time.deltaTime * lerpRate);

				//if () {
					syncPlayerRotationList.RemoveAt (0);
				//}
			}
		}
	}

	[Client]
	void TransmitRotation () {

		if (isLocalPlayer){
			lastPlayerRotation = transform.rotation;
			CmdSyncRotation (transform.rotation);
		}
	}

	[Command]
	void CmdSyncRotation(Quaternion rotation) {

		syncPlayerRotation = rotation;
	}

	[Client]
	void OnPlayerRotSynced (Quaternion latestPlayerRotation) {

		syncPlayerRotation = latestPlayerRotation;
		syncPlayerRotationList.Add (syncPlayerRotation);
		Debug.Log ("list Rotation:" + syncPlayerRotationList.Count);
	}
		
}
