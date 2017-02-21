using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncRotationBehaviour : NetworkBehaviour {

	[SyncVar (hook = "OnPlayerRotSynced")]
	private Quaternion syncPlayerRotation;

	[SerializeField]
	private float lerpRate = 10f;
	private Quaternion lastPlayerRotation;
	private float ignoreZRotation = 5f;

	private List<Quaternion> syncPlayerRotationList = new List<Quaternion>();

	void Update () {

		LerpRotation ();
	}

	void FixedUpdate () {

		TransmitRotation ();
	}

	void LerpRotation () {

		if (!isLocalPlayer) {

			if (syncPlayerRotationList.Count > 0) {
				
				//Vector3 rotation = new Vector3 (0, 0, syncPlayerZRotationList [0]);
				//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (rotation), Time.deltaTime * lerpRate);
				transform.rotation = Quaternion.Lerp (transform.rotation, syncPlayerRotationList [0], Time.deltaTime * lerpRate);
				syncPlayerRotationList.RemoveAt(0);
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
	}
		
}
