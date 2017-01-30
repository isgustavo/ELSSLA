using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;

	[SerializeField]
	private NetworkDiscoveryBehaviour _discovery;
	public NetworkDiscoveryBehaviour Discovery {
		get {
			return _discovery;
		}

	}


	void Start () {
		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		Debug.Log (networkPort);
		Debug.Log (networkAddress);
	}

	/// <summary>
	/// Raises the start host event. Called after StartHost ()
	/// </summary>
	public override void OnStartHost () {
		//To stop current search for a server
		_discovery.StopBroadcast ();

		//To start game being server
		_discovery.broadcastData = networkPort.ToString ();
		_discovery.StartAsServer ();

	}

	public void TapToJoin() {

		networkAddress = _discovery.Server.ServerIp;
		StartClient();
	}
		
}
