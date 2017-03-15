using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;
	[SerializeField]
	private NetworkDiscoveryBehaviour discovery;
		
	public void OnPlayAction () {
		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		StartHost ();
	}

	public void OnJoinAction () {
		networkAddress = discovery.Server.ServerIp;

		StartClient ();
	}

	public override void OnStartHost () {
		discovery.StopBroadcast ();

		discovery.broadcastData = networkPort.ToString ();
		discovery.StartAsServer ();
	}

	public override void OnClientSceneChanged(NetworkConnection conn) {
		ClientScene.AddPlayer(conn, 0);
	}

	public override void OnClientConnect(NetworkConnection conn) {
		//base.OnClientConnect(conn);
	}


	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId) {

		foreach (GameObject ship in spawnPrefabs) {
			
			if (LocalPlayerBehaviour.instance.GetShipName () == ship.transform.name) {

				var player = (GameObject)GameObject.Instantiate (ship);
				NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
				break;
			}
		}
			
	}

}
