using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;

	[SerializeField]
	public NetworkDiscoveryBehaviour discovery;

	// TODO variables test for registerPrefab to spawn player - remove
	public GameObject space;
	public GameObject space2;


	/// <summary>
	/// This starts a network "host" - a server and client in the same application.
	/// </summary>
	/// <returns>The client object created - this is a "local client".</returns>
	public override NetworkClient StartHost () {
		base.networkAddress = Network.player.ipAddress;
		base.networkPort = NETWORK_PORT;

		return base.StartHost ();
	}

	public void StartClient() {
		
		base.networkAddress = discovery.Server.ServerIp;
		base.StartClient ();
	}

	public override void OnStartHost () {

		discovery.StopBroadcast ();

		discovery.broadcastData = networkPort.ToString ();
		discovery.StartAsServer ();
	}

	public override void OnStartClient (NetworkClient client) {



		ClientScene.RegisterPrefab(space);
		ClientScene.RegisterPrefab(space2);


		client.RegisterHandler(MsgType.Connect, OnConnected);

		base.OnStartClient (client);
		Debug.Log ("onStartClient");

	}


	public void OnConnected(UnityEngine.Networking.NetworkMessage netMsg) {
		NetworkMessage message = new NetworkMessage();
		message.chosenClass = GameManagerBehaviour.TEST_SHIP; // mock choose

		ClientScene.AddPlayer (client.connection, 0, message);
		Debug.Log ("OnConnected");
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{

		NetworkMessage message = extraMessageReader.ReadMessage< NetworkMessage>();
		int selectedClass = message.chosenClass;
		Debug.Log(selectedClass);
		if (selectedClass == 1) {
			Debug.Log ("number == 1");
			var player = (GameObject)GameObject.Instantiate (space, transform.position, Quaternion.identity);
			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
		} else {
			Debug.Log ("number == 0");
			var player = (GameObject)GameObject.Instantiate (space2, transform.position, Quaternion.identity);
			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
		}

		Debug.Log ("OnServerAddPlayer 3");
	}

	//TODO Message class - Implementation incomplete
	public class NetworkMessage : MessageBase {
		public int chosenClass;
	}

}
