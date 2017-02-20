using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdendityBehaviour : NetworkBehaviour {

	[SyncVar] public string playerUniqueName;
	private NetworkInstanceId playerNetId;
	//private Transform _transform;

	public override void OnStartClient ()
	{
		GetNetIdentity ();
		SetIdentity ();
	}

	[Client]
	void GetNetIdentity () {

		playerNetId = GetComponent<NetworkIdentity> ().netId;
		CmdRegisterNetId (MakeUniqueName());
	}

	void SetIdentity () {

		if (isLocalPlayer) {

			transform.name = MakeUniqueName ();
		} else {

			transform.name = playerUniqueName;
		}

	}

	string MakeUniqueName () {

		string uniqueName = "Player " + playerNetId.ToString ();
		return uniqueName;
	}

	[Command]
	void CmdRegisterNetId(string netId) {

		playerUniqueName = netId;

	}
}
