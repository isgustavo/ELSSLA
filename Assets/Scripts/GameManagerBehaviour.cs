using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IObserver {

	void OnNotify ();
}

public abstract class ObserverBehaviour : MonoBehaviour {
	public abstract void OnNotify ();
}

public class GameManagerBehaviour : ObserverBehaviour {

	private static GameManagerBehaviour _instance = null; 
	public static GameManagerBehaviour Instance {
		get { return _instance; }
	}

	public enum EGameState {
		MainMenu,
		Game,
		Pause
	}

	[SerializeField]
	private Canvas mainMenuCanvas;
	[SerializeField]
	private Canvas gameMenuCanvas;
	[SerializeField]
	private Canvas pauseMenuCanvas;

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
				mainMenuCanvas.enabled = true;
				gameMenuCanvas.enabled = false;
				break;
			case EGameState.Game:
				mainMenuCanvas.enabled = false;
				gameMenuCanvas.enabled = true;
				break;
			}
		}
	}

	public Button tapToPlay;
	public Button tapToJoin;

	public bool isMovementButtonPointDown = false;


	void Awake() {
		if (_instance == null) {
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
	}


	public override void OnNotify () {

		//TODO
		tapToPlay.gameObject.SetActive(false);
		tapToJoin.gameObject.SetActive(true);

		Debug.Log (networkManager.discovery.Server.ServerIp);

	}

	//TODO remove 
	public static int TEST_SHIP = 0;

	public void TapToPlayOnClick () {
		GameState = EGameState.Game;
		networkManager.StartHost ();
		Debug.Log (" return start server = ");

	}

	public void TapToJoinOnClick () {
		TEST_SHIP = 1;
		GameState = EGameState.Game;
		networkManager.StartClient ();
	}


	public void TapToMovementEventTrigger (bool state) {

		isMovementButtonPointDown = state;
	}
		
}
