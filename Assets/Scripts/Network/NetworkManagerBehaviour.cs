using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerBehaviour : NetworkManager {

	public static string name;

	private const int NETWORK_PORT = 7777;
	public NetworkDiscoveryBehaviour discovery;


	public void OnPlayAction (string t) {
		name = t;
		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		StartHost ();
	}

	public void OnJoinAction (string t) {
		name = t;
		networkAddress = this.discovery.GetAddress ();
		StartClient ();


	}

	public override void OnStartHost () {
		this.discovery.StopBroadcast ();

		this.discovery.broadcastData = networkPort.ToString ();
		this.discovery.StartAsServer ();
	}

	public override void OnClientSceneChanged(NetworkConnection conn) {
		ClientScene.AddPlayer(conn, 0);
	}

	public override void OnClientConnect(NetworkConnection conn) {
		//base.OnClientConnect(conn);
	}
		
	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId) {

		foreach (GameObject prefab in spawnPrefabs) {
			
			if (LocalPlayerBehaviour.instance.GetShipName () == prefab.transform.name) {

				GameObject playerShip = (GameObject)GameObject.Instantiate (prefab);
				NetworkServer.AddPlayerForConnection (conn, playerShip, playerControllerId);
				break;

			}
		}
			
	}

}
