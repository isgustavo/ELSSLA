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


public class HighscorePlayer {

	//image
	private string name;
	private int highscore;

	public HighscorePlayer (string name, int highscore) {

		this.name = name;
		this.highscore = highscore;
	}


	public string GetName() {

		return name;
	}

	public int GetHighscore () {

		return highscore;
	}

}


public class HeadToHeadPlayer {

	private int id;
	private string name;
	private int deaths;
	private int kills;
	//image


	public HeadToHeadPlayer (int id, string name, int deaths, int kills) {

		this.id = id;
		this.name = name;
		this.deaths = deaths;
		this.kills = kills;

	}

	public string GetName () {
		return name;
	}

	public int GetDeaths () {
		return deaths;
	}

	public int GetKills () {
		return kills;
	}
}


public class LocalPlayerBehaviour : MonoBehaviour {

	public static LocalPlayerBehaviour instance = null;

	private PlayerData player;

	private List<HighscorePlayer> highscorePlayers = new List<HighscorePlayer> ();
	private bool highscoreValuesAvailable = false;

	private List<HeadToHeadPlayer> headToHeadListPlayers = new List<HeadToHeadPlayer> ();
	private bool headToHeadValuesAvailable = false;

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
		LoadHighscoreFriends ();
		LoadHeadToHeadStats ();

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


	void LoadHighscoreFriends () {


		highscorePlayers = MockHighscoreFriends ();

		highscoreValuesAvailable = true;
	}

	List<HighscorePlayer> MockHighscoreFriends () {


		List<HighscorePlayer> hp = new List<HighscorePlayer> (); 

		HighscorePlayer player1 = new HighscorePlayer ("Gustavo", 89999);
		HighscorePlayer player2 = new HighscorePlayer ("Mariana", 87643);
		HighscorePlayer player3 = new HighscorePlayer ("Pedro", 300);
		HighscorePlayer player4 = new HighscorePlayer ("NomeMaiorQtodos", 8789);
		HighscorePlayer player5 = new HighscorePlayer ("NomeRealmenteGrande", 500);


		hp.Add (player1);
		hp.Add (player2);
		hp.Add (player3);
		hp.Add (player4);
		hp.Add (player5);


		return hp;
	}

	void LoadHeadToHeadStats () {

		headToHeadListPlayers = MockHeadToHeadList ();
		headToHeadValuesAvailable = true;
	}


	List<HeadToHeadPlayer> MockHeadToHeadList () {

		List<HeadToHeadPlayer> hh = new List<HeadToHeadPlayer> ();

		HeadToHeadPlayer player1 = new HeadToHeadPlayer (1, "Gustavo", 3, 6);
		HeadToHeadPlayer player2 = new HeadToHeadPlayer (2, "Mariana", 6, 2);
		HeadToHeadPlayer player3 = new HeadToHeadPlayer (3, "Pedro", 1, 1);
		HeadToHeadPlayer player4 = new HeadToHeadPlayer (4, "NomeMaiorQtodos", 5, 9);
		HeadToHeadPlayer player5 = new HeadToHeadPlayer (5, "NomeRealmenteGrande",2, 0);
		HeadToHeadPlayer player6 = new HeadToHeadPlayer (6, "Programador", 2, 3);

		hh.Add (player1);
		hh.Add (player2);
		hh.Add (player3);
		hh.Add (player4);
		hh.Add (player5);
		hh.Add (player6);

		return hh;
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

	public List<HighscorePlayer> GetHighscorePlayersList () {

		return highscorePlayers;
	}

	public List<HeadToHeadPlayer> GetHeadToHeadListPlayers () {

		return headToHeadListPlayers;
	}

	public string GetLocalPlayerName () {
		return "Gustavo";
	}

	public bool GetHighscoreValuesAvailable () {
		return highscoreValuesAvailable;
	}

	public bool GetHeadToHeadValuesAvailable () {
		return headToHeadValuesAvailable;
	}

}
