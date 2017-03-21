using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
			// Forces a different code path in the BinaryFormatter that doesn't rely on run-time code generation (which would break on iOS).
			// http://answers.unity3d.com/questions/30930/why-did-my-binaryserialzer-stop-working.html
			Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		} else if (instance != this) {

			Destroy (gameObject);
		}

		LoadLocalPlayerInfo ();

	}

	void LoadLocalPlayerInfo () {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {
			file = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			player = (PlayerData) bf.Deserialize (file);

			file.Close ();
		} else {
			file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");

			player = new PlayerData ();
			bf.Serialize (file, player);
			file.Close (); 
		}

	}

	public void SaveLocalPlayerIndo (int score) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {

			file = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);

			if (score > player.highscore) {
				player.highscore = score;
			}

			bf.Serialize (file, player);
			file.Close (); 
		} 

	}


	public string GetShipName () {
		return "NormalPlayer";
		//return player.shipName;
	}

	public int GetHighscore () {
		return player.highscore;
	}


}
