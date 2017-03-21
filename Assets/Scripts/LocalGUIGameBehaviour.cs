using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalGUIGameBehaviour : MonoBehaviour {

	private const int INITIAL_SCORE = -1;
	private const int INITIAL_HIGHSCORE = -1;
	private const float TIME_HOLD_TRIGGER = 5f;
	private const float Z_POSITION = -6f;

	public PlayerBehaviour player { get; set; }

	[SerializeField]
	private GameObject gameCanvas;

	[SerializeField]
	private Text scoreText;
	private int lastScore = INITIAL_SCORE;

	[SerializeField]
	private Text highscore;
	private int lastHighscore = INITIAL_HIGHSCORE;

	[SerializeField]
	private GameObject deadCanvas;

	[SerializeField]
	private GameObject youDeadLetters;

	private bool isRespawnTriggerDown = false;
	private bool isBackTriggerDown = false;

	private float holdTimer = 0f;

	void Update () {

		if (player == null) 
			return;
		

		if (player.isDead) {

			gameCanvas.SetActive (false);
			//MoveTrigger (false);
			//ShootTrigger (false);
			deadCanvas.SetActive (true);

			lastScore = INITIAL_SCORE;
			lastHighscore = INITIAL_HIGHSCORE;

			UpdateDead ();

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

		if (player.highscore  >= lastHighscore) {

			lastHighscore += Mathf.CeilToInt((player.highscore - lastHighscore) * .1f);
			highscore.text = lastHighscore.ToString("000000000");
		}
	}

	void UpdateDead () {

		Vector3 playerPosition = player.transform.position;
		youDeadLetters.transform.position = new Vector3 (playerPosition.x, playerPosition.y, Z_POSITION);

		if (isRespawnTriggerDown || isBackTriggerDown) {
			if (holdTimer > TIME_HOLD_TRIGGER) {
				if (isRespawnTriggerDown) {
					player.CmdRespawn ();
				} else { 
					//
				}

				holdTimer = 0f;
			} else {

				holdTimer += Time.deltaTime;
			}
		}
	}


	public void MoveTrigger (bool value) {

		player.isMoving = value;
	}

	public void ShootTrigger (bool value) {

		player.isShooting = value;
	}

	public void RespawnTrigger (bool value) {
		holdTimer = 0f;
		isRespawnTriggerDown = value;

	}

}