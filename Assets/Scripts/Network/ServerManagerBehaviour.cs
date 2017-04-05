using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ServerManagerBehaviour: NetworkBehaviour {

	public static ServerManagerBehaviour instance;

	[SerializeField]
	private GameObject asteroidSpawnManager;
	private Dictionary<string, PlayerBehaviour> players = new Dictionary<string, PlayerBehaviour> ();

	void Awake () {
		if (instance == null) {
			
			instance = this;
		} else if (instance != this) {

			Destroy (gameObject);    
		}
	}

	public override void OnStartServer () {
		base.OnStartServer ();

		NetworkServer.Spawn (Instantiate (asteroidSpawnManager));
	}

	public void AddPlayer (string playerId, PlayerBehaviour player) {
		players.Add (playerId, player);

	}

	public PlayerBehaviour GetPlayer (string playerId) {
		return players [playerId];

	}
}