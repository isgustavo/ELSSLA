using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel=0, sendInterval=0.0003f)]
public class PlayerSyncPositionBehaviour : NetworkBehaviour {

	[SyncVar (hook="OnPlayerPosSynced")]
	private Vector3 syncPlayerPosition;

	private float normalLerpRate = 20f;
	private float fasterLerpRate = 45f;
	private Vector3 lastPlayerPosition;
	private float threshold = .2f;

	private List<Vector3> syncPlayerPositionList = new List<Vector3>();
	private float closeEnough = 0.11f;

	void Update () {

		LerpRotation ();
	}

	void FixedUpdate () {

		TransmitRotation ();
	}

	void LerpRotation () {
		
		if (!isLocalPlayer) {

			if (syncPlayerPositionList.Count > 0) {

				float lerpRate = 0;

				if (syncPlayerPositionList.Count < 10) {
					lerpRate = normalLerpRate;
				} else {
					lerpRate = fasterLerpRate;
				}
					
				transform.position = Vector3.Lerp (transform.position, syncPlayerPositionList [0], Time.deltaTime * lerpRate);

				if (Vector3.Distance (transform.position, syncPlayerPositionList [0]) < closeEnough) {
					syncPlayerPositionList.RemoveAt (0);
				}
			}
		}

	}

	[Client]
	void TransmitRotation () {

		if (isLocalPlayer && Vector3.Distance(transform.position, lastPlayerPosition) > threshold) {
			lastPlayerPosition = transform.position;
			CmdSyncPosition (lastPlayerPosition);
		}	
	}
		
	[Command]
	void CmdSyncPosition(Vector3 position) {
	
		syncPlayerPosition = position;
	}
		
	[Client]
	void OnPlayerPosSynced (Vector3 position) {

		syncPlayerPosition = position;
		syncPlayerPositionList.Add (syncPlayerPosition);

	}
}
