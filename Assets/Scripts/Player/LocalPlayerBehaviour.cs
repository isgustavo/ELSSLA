using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using Facebook.Unity;

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

	public void AddDeath () {

		this.deaths += 1;
	}

	public int GetKills () {
		return kills;
	}

	public void AddKill () {

		this.kills += 1;
	}
}


public class LocalPlayerBehaviour : MonoBehaviour {

	public static LocalPlayerBehaviour instance = null;

	private PlayerData player;

	private List<HighscorePlayer> highscorePlayers = new List<HighscorePlayer> ();
	private bool highscoreValuesAvailable = false;

	private Dictionary<int, HeadToHeadPlayer> headToHeadValuesPlayers = new Dictionary<int, HeadToHeadPlayer> ();
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

		FB.Init (SetInit, OnHideUnity);
		LoadLocalPlayerInfo ();
		LoadHighscoreFriends ();
		LoadHeadToHeadStats ();

	}

	void SetInit() {

		if (FB.IsLoggedIn) {
			//fbLoginButton.SetActive (false);
			//fbLoggedContainer.SetActive (true);

			//FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			//FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		} else {
			Debug.Log ("FB is not logged in");
		}

	}

	void OnHideUnity(bool isGameShown) {

		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

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


	public void LoadHighscoreFriends () {


		//highscorePlayers = MockHighscoreFriends ();

		//highscoreValuesAvailable = true;

		if (FB.IsLoggedIn) {

			FB.API ("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, HighScoreCallBack);
		}
	}

	public void SetScore () {

		var scoreData = new Dictionary<string, string> ();
		scoreData ["score"] = "69";

		FB.API ("/me/scores", HttpMethod.POST, delegate (IGraphResult result) {
			Debug.Log("Set score: "+ result.RawResult);
		}, scoreData);

		LoadHighscoreFriends ();
	}

	private void HighScoreCallBack (IResult result) {
		Debug.Log ("HighScore Callback:" + result.ResultDictionary);


		IDictionary<String, object> data = result.ResultDictionary;
		List<object> listObj = (List<object>)data ["data"];

		foreach (object obj in listObj) {

			var entry = (Dictionary<string, object>)obj;
			var user = (Dictionary<string, object>)entry ["user"];


			Debug.Log (user ["name"].ToString () + "score: " + entry ["score"].ToString ());
		}



		//foreach(Object obj in result.ResultDictionary.to
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

		//headToHeadListPlayers = MockHeadToHeadList ();
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

		return null;//headToHeadListPlayers;
	}

	public string GetLocalPlayerName () {
		return "Gustavo";
	}

	public bool isLoggedIn () {
		return true;
	}

	public bool GetHighscoreValuesAvailable () {
		return highscoreValuesAvailable;
	}

	public bool GetHeadToHeadValuesAvailable () {
		return headToHeadValuesAvailable;
	}
		
	public void SaveNewHeadToHead (bool death, int playerId) {

		HeadToHeadPlayer hh = headToHeadValuesPlayers [playerId];

		if (hh != null) {

			if (death) {

				hh.AddDeath ();
			} else {
				
				hh.AddKill ();
			}
		} else {

			//hh = new HeadToHeadPlayer(playerId, 
		}
		//save()
	}
}
