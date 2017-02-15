// Copyright 2017 ISGUSTAVO
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//          http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;

	[SerializeField]
	public NetworkDiscoveryBehaviour discovery;


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

		Debug.Log ("Client joined");
	}

	public override void OnStartHost () {

		discovery.StopBroadcast ();

		discovery.broadcastData = networkPort.ToString ();
		discovery.StartAsServer ();
		Debug.Log ("Host on");
	}

	public override void OnStartClient (NetworkClient client) {
		
		client.RegisterHandler(MsgType.Connect, OnConnected);

		base.OnStartClient (client);

	}


	public void OnConnected(UnityEngine.Networking.NetworkMessage netMsg) {
		NetworkMessage message = new NetworkMessage();
		//message.chosenClass = GameManagerBehaviour.TEST_SHIP; // mock choose

		ClientScene.AddPlayer (client.connection, 0, message);
		Debug.Log ("OnConnected");
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{

		//NetworkMessage message = extraMessageReader.ReadMessage< NetworkMessage>();
		//int selectedClass = message.chosenClass;
		//Debug.Log(selectedClass);
		//if (selectedClass == 1) {
			//Debug.Log ("number == 1");

		GameObject[] li = spawnPrefabs.ToArray();
		GameObject o = li [1];

		var player = (GameObject)GameObject.Instantiate (o, transform.position, Quaternion.identity);
			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
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
