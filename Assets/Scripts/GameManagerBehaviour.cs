﻿// Copyright 2017 ISGUSTAVO
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
using UnityEngine.UI;

public abstract class ObserverBehaviour : MonoBehaviour {
	public abstract void OnNotify ();
}

[RequireComponent (typeof (NetworkDiscoveryBehaviour))]
public class GameManagerBehaviour : ObserverBehaviour {

	private static GameManagerBehaviour _instance = null; 
	public static GameManagerBehaviour Instance {
		get { return _instance; }
	}

	[SerializeField]
	private NetworkManagerBehaviour networkManager;

	public GameObject tapToPlay;
	public GameObject tapToJoin;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
	}


	public override void OnNotify () {

		tapToPlay.gameObject.SetActive(false);
		tapToJoin.gameObject.SetActive(true);

		Debug.Log (networkManager.discovery.Server.ServerIp);

	}

	public void StartAsAHost () {

		networkManager.StartHost ();
	}

	public void StartAsAClient () {
		
		networkManager.StartClient ();
	}
		
}