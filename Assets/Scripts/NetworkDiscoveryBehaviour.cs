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
