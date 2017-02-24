using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUIBehaviour : MonoBehaviour {

	public TextMesh score;
	private int _score;
	public TextMesh highscore;
	private int _highscore;

	public PlayerManagerBehaviour player;

	public void SetActiveGUI(bool respawn) {

		gameObject.SetActive (true);
		if (respawn) {

			_score = -1;
		}
	}

	public void SetDeactive() {

		gameObject.SetActive(false);
	}


	void Update () {

		if (player == null) {
			return;
		}

		if (player.score > _score) {

			_score += Mathf.CeilToInt((player.score - _score)*.1f);
			score.text = _score.ToString("000000000");

		}

		if (player.highscore  > _highscore) {

			_highscore += Mathf.CeilToInt((player.highscore - _highscore)*.1f);
			highscore.text = _highscore.ToString("000000000");

		}
			
	}
}
