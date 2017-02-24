using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManagerBehaviour : MonoBehaviour {


	public GameObject gameGUI;
	public GameObject deadGUI;


	/*
	public enum EGameState {
			MainMenu,
			Game,
			Pause
		}
	

	private static GameManagerBehaviour _instance = null; 
	public static GameManagerBehaviour Instance {
		get { return _instance; }
	}

	[SerializeField]
	private NetworkManagerBehaviour networkManager;

	private EGameState _gameState;
	public EGameState GameState {
		get {
				return _gameState;
			}

		set {
			_gameState = value;

			switch (_gameState) {
			case EGameState.MainMenu: 
				mainMenu.SetActive (true);
				gameMenu.SetActive (false);
			break;
			case EGameState.Game:
				mainMenu.SetActive (false);
				gameMenu.SetActive (true);
			break;
			}
		}
	}

	public GameObject mainMenu;
	public GameObject gameMenu;
	public GameObject tapToPlay;
	public GameObject tapToJoin;
	public Transform testPrefab;

	public bool isMovement;
	public bool isShooting;

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

		//Debug.Log (networkManager.discovery.Server.ServerIp);

	}

	public void StartAsAHost () {
		GameState = EGameState.Game;
		networkManager.StartHost ();
	}

	public void StartAsAClient () {
		GameState = EGameState.Game;
		networkManager.StartClient ();
	}
		
	public void MovementTrigger (bool value) {

		isMovement = value;
	}

	public void ShootingTrigger (bool value) {

		isShooting = value;
	}
*/

		
}
