using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;

	[SerializeField]
	private NetworkDiscoveryBehaviour discovery;


	/// <summary>
	/// Taps to play.
	/// </summary>
	public void TapToPlay () {

		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		StartHost ();
	}

	/// <summary>
	/// Raises the start host event. Called after StartHost ()
	/// </summary>
	public override void OnStartHost () {
		//To stop current search for a server
		discovery.StopBroadcast ();

		//To start game being server
		discovery.broadcastData = networkPort.ToString ();
		discovery.StartAsServer ();

	}

	public void TapToJoin() {

		networkAddress = discovery.Server.ServerIp;
		StartClient();
	}
		
}
