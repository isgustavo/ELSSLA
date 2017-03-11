using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;

[Serializable] 
public class PlayerData {

	public int highscore;
	public int deaths;
	public int programmerDeaths;

	public PlayerData () {

		highscore = 0;
		deaths = 0;
		programmerDeaths = 0;
	}

}

public class PlayerBehaviour : NetworkBehaviour {

	public int score;
	public PlayerData data { get; set; }

	void Start () {

		if (!isLocalPlayer) {
			return;
		}

		Load ();
		//Save (9876, false);
	}
		
	public override void OnStartLocalPlayer () {
		base.OnStartLocalPlayer ();

		CameraFollowBehaviour camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollowBehaviour> ();
		camera.target = gameObject;

		GameManagerBehaviour manage = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();
		manage.player = this;

	}

	void OnCollisionEnter(Collision collision) {

		if (!isLocalPlayer)
			return;

		GameObject hit = collision.gameObject;
		if (hit.GetComponent<AsteroidBehaviour> () != null || hit.GetComponent<FragmentBehaviour> () != null) {

			score += 100;
			Debug.Log ("OnCollision");
		}

	}


	void Save (int points, bool programmerDeath) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {
			
			file = new FileStream (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			data = (PlayerData) bf.Deserialize (file);

			if (points > data.highscore) {
				data.highscore = points;
			}

			data.deaths += 1;

			if (programmerDeath) {
				data.programmerDeaths += 1;
			}

			bf.Serialize (file, data);
			file.Close (); 
		} 

	}

	void Load () {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {

			file = new FileStream (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			data = (PlayerData) bf.Deserialize (file);
			file.Close ();
		} else {

			file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");
			data = new PlayerData ();
		}
	}

}


