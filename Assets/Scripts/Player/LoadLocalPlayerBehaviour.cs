using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadLocalPlayerBehaviour : NetworkBehaviour {

	[SerializeField]
	private List<GameObject> playerPrefabList;

	public override void OnStartClient () {
		base.OnStartClient ();

		Debug.Log ("OnStartClient");
	}


	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

		Debug.Log ("OnStartLocalPlayer");
	}


	void Update () {
		Debug.Log ("Update");
		string playerShipName = "NormalPlayer"; 

		foreach (GameObject prefab in playerPrefabList) {

			if (prefab.transform.name.Equals (playerShipName)) {
				NetworkConnection conn = base.connectionToClient;
				Debug.Log ("Conn" + conn);
				var player = Instantiate<GameObject> (prefab);
				Destroy (gameObject);

				NetworkServer.ReplacePlayerForConnection (conn, player, 0);
			}
		}

	}

}
