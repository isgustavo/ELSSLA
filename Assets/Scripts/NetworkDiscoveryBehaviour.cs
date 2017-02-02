using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDiscoveryBehaviour : NetworkDiscovery {

	public class ServerGame {

		private string _serverName;
		public string ServerName {
			get { return _serverName; }
		}

		private string _serverIp;
		public string ServerIp {
			get { return _serverIp; }
		}

		public ServerGame (string name, string ip) {
			_serverName = name;
			_serverIp = ip;
		}
	}

	private ServerGame _server;
	public ServerGame Server {
		get { return _server; }
	}

	public ObserverBehaviour observer;

	void Start () {

		Initialize();
		StartAsClient();
	}

	public override void OnReceivedBroadcast (string fromAddress, string data) {

		//Network discovery component has found a server being broadcasted
		ServerFound (fromAddress, data);
		observer.OnNotify ();
	}

	private void ServerFound (string ip, string data) {

		if (_server == null) {
			_server = new ServerGame (data, ip);
		}

	}

}
