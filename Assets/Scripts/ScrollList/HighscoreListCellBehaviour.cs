using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreListCellBehaviour : MonoBehaviour {

	//image
	[SerializeField]
	private Text position;
	[SerializeField]
	private Text name;
	[SerializeField]
	private Text highscore;

	public void SetPosition (int playerPosition) {

		position.text = playerPosition.ToString ();
	}

	public void SetName (string playerName) {

		name.text = playerName;
	}

	public void SetHighscore (int playerScore) {

		highscore.text = playerScore.ToString ();
	}

}