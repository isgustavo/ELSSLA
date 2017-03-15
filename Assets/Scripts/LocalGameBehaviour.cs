using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalGameBehaviour : MonoBehaviour {

	public PlayerBehaviour player { get; set; }

	[SerializeField]
	private GameObject gameCanvas;

	[SerializeField]
	private Text scoreText;
	private int lastScore = -1;

	[SerializeField]
	private Text highscore;
	private int lastHighsocre = -1;

	[SerializeField]
	private GameObject deadCanvas;

	[SerializeField]
	private GameObject youDeadLetters;

	private bool isRespawnTriggerDown = false;
	private bool isBackTriggerDown = false;

	private float respawnTime = 5f;
	private float respawnTimer = 0f;

	void Update () {


		if (player == null) {
			return;
		}

		if (player.isDead) {

			gameCanvas.SetActive (false);
			deadCanvas.SetActive (true);

			Vector3 playerPosition = player.transform.position;
			youDeadLetters.transform.position = new Vector3 (playerPosition.x, playerPosition.y, -6);

			if (isRespawnTriggerDown || isBackTriggerDown) {
				Debug.Log (respawnTimer);
				if (respawnTimer > respawnTime) {
					if (isRespawnTriggerDown) {
						player.CmdRespawn ();
					} else { 
						//
					}

					respawnTimer = 0f;
				} else {

					respawnTimer += Time.deltaTime;
				}
			}



		} else {
			gameCanvas.SetActive (true);
			deadCanvas.SetActive (false);
			UpdateScore ();

		}

	}

	void UpdateScore () {

		if (player.score >= lastScore) {

			lastScore += Mathf.CeilToInt ((player.score - lastScore) * .1f);
			scoreText.text = lastScore.ToString ("000000000");
		}
	}


	public void MoveTrigger (bool value) {

		player.isMoving = value;
	}

	public void ShootTrigger (bool value) {

		player.isShooting = value;
	}

	public void RespawnTrigger (bool value) {
		respawnTimer = 0f;
		isRespawnTriggerDown = value;

	}


}
/*
public class GUIManagerBehaviour : MonoBehaviour {

	public PlayerBehaviour localPlayer { get; set;}

	[SerializeField]
	private Text score;
	private int lastScore = -1;

	[SerializeField]
	private Text highscore;
	private int lastHighscore = -1;

	[SerializeField]
	private GameObject ControllerCanvas;
	[SerializeField]
	private GameObject gameCanvas;
	[SerializeField]
	private GameObject deadCanvas;
	[SerializeField]
	private GameObject youDeadLetters;


	void Update () {


		if (localPlayer == null) {
			return;
		}

		if (!localPlayer.isDead) {
			UpdateScore ();
		} else {
			gameCanvas.SetActive (false);
			deadCanvas.SetActive (true);
			Vector3 playerPosition = localPlayer.transform.position;
			youDeadLetters.transform.position = new Vector3 (playerPosition.x, playerPosition.y, -6);
			ControllerCanvas.SetActive (false);
		}

	}

	void UpdateScore() {
		
		if (localPlayer.score >= lastScore) {

			lastScore += Mathf.CeilToInt((localPlayer.score - lastScore) * .1f);
			score.text = lastScore.ToString("000000000");
		}

		//if (localPlayer.data.highscore  >= lastHighscore) {

		//	lastHighscore += Mathf.CeilToInt((localPlayer.data.highscore - lastHighscore) * .1f);
		//	highscore.text = lastHighscore.ToString("000000000");
		//}
	}
}*/