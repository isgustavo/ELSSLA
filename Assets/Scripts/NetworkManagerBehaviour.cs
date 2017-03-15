using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;

	[SerializeField]
	private NetworkDiscoveryBehaviour discovery;



	public void StartAsAHost () {
		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		StartHost ();

	}

	public override void OnStartHost () {
		discovery.StopBroadcast ();

		discovery.broadcastData = networkPort.ToString ();
		discovery.StartAsServer ();

	}

	public void StartAsAClient() {
		networkAddress = discovery.Server.ServerIp;
		StartClient ();

	}
		
	public override void OnStartClient (NetworkClient client) {
		base.OnStartClient (client);

	}

	public override void OnClientSceneChanged(NetworkConnection conn) {
		ClientScene.AddPlayer(conn, 0);
	}

	public override void OnClientConnect(NetworkConnection conn) { }
		
}
