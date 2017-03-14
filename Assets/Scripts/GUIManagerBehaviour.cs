using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerBehaviour : MonoBehaviour {

	public PlayerBehaviour localPlayer { get; set;}

	[SerializeField]
	private Text score;
	private int lastScore = -1;

	[SerializeField]
	private Text highscore;
	private int lastHighscore = -1;

	[SerializeField]
	private GameObject gameCanvas;
	[SerializeField]
	private GameObject deadCanvas;


	void Update () {


		if (localPlayer == null) {
			return;
		}

		if (!localPlayer.isDead) {
			UpdateScore ();
		} else {
			gameCanvas.SetActive (false);
			deadCanvas.SetActive (true);
		}

	}

	void UpdateScore() {
		
		if (localPlayer.score >= lastScore) {

			lastScore += Mathf.CeilToInt((localPlayer.score - lastScore) * .1f);
			score.text = lastScore.ToString("000000000");
		}

		if (localPlayer.data.highscore  >= lastHighscore) {

			lastHighscore += Mathf.CeilToInt((localPlayer.data.highscore - lastHighscore) * .1f);
			highscore.text = lastHighscore.ToString("000000000");
		}
	}
}