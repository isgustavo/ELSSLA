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

	void Start () {
		
	}

	public override void OnNotify () {

		//TODO
		tapToPlay.gameObject.SetActive(false);
		tapToJoin.gameObject.SetActive(true);
		Debug.Log (networkManager.Discovery.Server.ServerIp);

	}

	public void TapToPlayOnClick () {
		GameState = EGameState.Game;
		networkManager.StartHost ();

	}

	public void TapToJoinOnClick () {
		GameState = EGameState.Game;
		networkManager.networkAddress = networkManager.Discovery.Server.ServerIp;
		networkManager.StartClient ();
	}
}
