﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerBehaviour : NetworkManager {

	private const int NETWORK_PORT = 7777;
	public NetworkDiscoveryBehaviour discovery;

	public void OnTapAction () {

		if (this.discovery.IsServerFound ()) {
			OnJoinAction ();
		} else {
			OnPlayAction ();
		}
	}

	void OnPlayAction () {
		networkAddress = Network.player.ipAddress;
		networkPort = NETWORK_PORT;

		StartHost ();
	}

	void OnJoinAction () {
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
