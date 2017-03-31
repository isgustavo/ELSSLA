using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDiscoveryBehaviour : NetworkDiscovery {

	private bool serverFound = false;
	//private string serverData;
	private string serverAddress;

	//public ObserverBehaviour observer;

	void Start () {
		Initialize ();
		StartAsClient ();
	}


	public override void OnReceivedBroadcast (string fromAddress, string data) {

		//Network discovery component has found a server being broadcasted
		this.serverAddress = fromAddress;
		//this.serverData = data;

		this.serverFound = true;

		//observer.OnNotify ();
	}

	public string GetAddress () {

		return this.serverAddress;
	}

	public bool IsServerFound () {

		return this.serverFound;
	}
}
