using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;
	[SerializeField]
	private NetworkDiscoveryBehaviour discovery;


	void Start () {

		//discovery = GameObject.FindGameObjectWithTag ("NetworkDiscovery").GetComponent<NetworkDiscoveryBehaviour> ();
	}

	public void StartAsAHost () {
		Debug.Log ("StartAsAHost");
		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		//StopHost ();
		StartHost ();


	}

	public override void OnStartHost () {
		Debug.Log ("OnStartHost");
		discovery.StopBroadcast ();

		discovery.broadcastData = networkPort.ToString ();
		discovery.StartAsServer ();


	}

	public void StartAsAClient() {
		Debug.Log ("StartAsAClient");
		networkAddress = discovery.Server.ServerIp;
		StartClient ();


	}



	public override void OnStartClient (NetworkClient client) {
		Debug.Log ("OnStartClient");
		//client.RegisterHandler(MsgType.Connect, OnConnected);
		base.OnStartClient (client);


	}




	//public void OnConnected(UnityEngine.Networking.NetworkMessage netMsg) {
	//	Debug.Log ("OnConnected");
	//	NetworkMessage message = new NetworkMessage();
		//message.chosenClass = GameManagerBehaviour.TEST_SHIP; // mock choose

	//	ClientScene.AddPlayer (client.connection, 0, message);


	//

	//public override void OnClientConnect (NetworkConnection conn)
	//{
		//base.OnClientConnect (conn);
	//	Debug.Log ("OnClientConnect");
	//	NetworkMessage message = new NetworkMessage();
		//if (string.IsNullOrEmpty (this.onlineScene) || this.onlineScene == this.offlineScene){
			//ClientScene.Ready (conn);
			//if (this.autoCreatePlayer)
			//{
	//			ClientScene.AddPlayer(conn, 0, message);
			//}
		//}

	//}

	//public override void OnClientSceneChanged (NetworkConnection conn)
	//
		//base.OnClientSceneChanged (conn);
	//}

	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		NetworkMessage message = new NetworkMessage ();
		message.chosenClass = TapToJoinBehaviour.ConnectionTesterStatus;
		ClientScene.AddPlayer(conn, 0, message);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		//base.OnClientConnect(conn);
	}


	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{

		NetworkMessage message = extraMessageReader.ReadMessage< NetworkMessage>();
		int selectedClass = message.chosenClass;

		GameObject[] li = spawnPrefabs.ToArray ();
		GameObject o = li [0];

		//Debug.Log(selectedClass);
		if (selectedClass == 1) {
			//Debug.Log ("number == 1");

			//GameObject[] li = spawnPrefabs.ToArray ();
			//GameObject o = li [0];


			var player = (GameObject)GameObject.Instantiate (o, new Vector3 (-1, 0, -6), Quaternion.identity);
			player.name = "Client";

			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);

		} else {
			var player = (GameObject)GameObject.Instantiate (o, new Vector3 (1, 0, -6), Quaternion.identity);
			player.name = "Host";

			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
		}
			
		//} else {
		//	Debug.Log ("number == 0");
		//	var player = (GameObject)GameObject.Instantiate (Resources.Load("spaceship_1"), transform.position, Quaternion.identity);
		//	NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
		//}

		Debug.Log ("OnServerAddPlayer 3");
	}

	//TODO Message class - Implementation incomplete
	public class NetworkMessage : MessageBase {
		public int chosenClass;
	}



}
