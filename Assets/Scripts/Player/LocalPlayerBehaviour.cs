using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

[Serializable] 
public class PlayerData {

	public int highscore;
	public int deaths;
	public int programmerDeaths;
	public string shipName;

	public PlayerData () {

		highscore = 0;
		deaths = 0;
		programmerDeaths = 0;
		shipName = " "; 
	}

}

public class LocalPlayerBehaviour : MonoBehaviour {

	public static LocalPlayerBehaviour instance = null;

	private PlayerData player;

	void Awake () {
		
		if (instance == null) {
			
			instance = this;
		} else if (instance != this) {

			Destroy (gameObject);
		}

		//DontDestroyOnLoad (gameObject);
		LoadLocalPlayerInfo ();

	}

	void LoadLocalPlayerInfo () {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {

			file = new FileStream (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			player = (PlayerData) bf.Deserialize (file);
			file.Close ();
		} else {

			file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");
			player = new PlayerData ();
		}

	}

	void SaveLocalPlayerIndo () {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {

			file = new FileStream (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			player = (PlayerData) bf.Deserialize (file);

			//


			bf.Serialize (file, player);
			file.Close (); 
		} 


	}


	public string GetShipName () {
		return "NormalPlayer";
		//return player.shipName;
	}


}
