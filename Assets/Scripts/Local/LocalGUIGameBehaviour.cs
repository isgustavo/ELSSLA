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
	public RadialProgressBarBehaviour respawnProgressBar;
	public RadialProgressBarBehaviour backToMenuProgressBar;

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

	private bool isNewHighScore;

	private bool isNemezis;
	public Text nemezisplayerNameText;
	public Text nemezisPlayerHighscoreText;
	public Text nemezisKillCountText;
	public Text nemezisDeathCountText;

	private bool isRespawnTriggerDown = false;
	private bool isBackTriggerDown = false;

	private float holdTimer = 0f;

	void Start () {
		
		if (respawnProgressBar != null) {

			respawnProgressBar.SetTimeToHold(TIME_HOLD_TRIGGER);
		}

		if (backToMenuProgressBar != null) {

			backToMenuProgressBar.SetTimeToHold (TIME_HOLD_TRIGGER);
		}

	}

	void Update () {

		if (player.isDead) {

			gameCanvas.SetActive (false);
			deadCanvas.SetActive (true);

			SetUpDeadCanvas ();
			UpdateDead ();

		} else {

			lastScore = INITIAL_SCORE;
			lastHighscore = INITIAL_HIGHSCORE;

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

	void SetUpDeadCanvas () {

		if (this.isNewHighScore) {

		}
			
		if (!LocalPlayerBehaviour.instance.isLoggedIn () || !this.isNemezis)
			return;



	}

	void UpdateDead () {

		if (isRespawnTriggerDown || isBackTriggerDown) {
			if (holdTimer > TIME_HOLD_TRIGGER) {
				if (isRespawnTriggerDown) {
					respawnProgressBar.ResetProgressBar ();
					player.CmdRespawn ();
				} else { 
					backToMenuProgressBar.ResetProgressBar ();
					player.Unspawn ();
				}

				holdTimer = 0f;
			} else {

				holdTimer += Time.deltaTime;
			}
		}
	}

	public void GameActive () {


		gameCanvas.SetActive (true);
	}

	public void MoveTrigger (bool value) {
		if (player == null)
			return;
			
		player.isMoving = value;
	}

	public void ShootTrigger (bool value) {
		if (player == null)
			return;
		
		player.isShooting = value;
	}

	public void RespawnTrigger (bool value) {
		
		holdTimer = 0f;
		respawnProgressBar.IsHold (value);
		isRespawnTriggerDown = value;
	}

	public void BackToMenuTrigger (bool value) {

		holdTimer = 0f;
		backToMenuProgressBar.IsHold (value);
		isBackTriggerDown = value;
	}

}